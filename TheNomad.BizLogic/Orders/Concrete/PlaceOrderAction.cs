﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.BizDbAccess.Orders;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.BizLogic.Orders.Concrete
{
    public class PlaceOrderAction : 
        BizActionErrors, //#A
        IPlaceOrderAction //#B
    {
        private readonly IPlaceOrderDbAccess _dbAccess;

        public PlaceOrderAction(IPlaceOrderDbAccess dbAccess)//#C
        {
            _dbAccess = dbAccess;
        }

        /// <summary>
        /// This validates the input and if OK creates 
        /// an order and calls the _dbAccess to add to orders
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>returns an Order. Will be null if there are errors</returns>
        public Order Action(PlaceOrderInDto dto) //#D
        {
            if (!dto.AcceptTAndCs)                    //#E
            {                                         //#E
                AddError("You must accept the T&Cs to place an order.");   //#E
                return null;                          //#E
            }                                         //#E
            if (!dto.LineItems.Any())                 //#E
            {                                         //#E
                AddError("No items in your basket."); //#E
                return null;                          //#E
            }                                         //#E

            var booksDict = _dbAccess.FindBooksByIdsWithPriceOffers (dto.LineItems.Select(x => x.BookId));//#F
            var order = new Order                          //#G
            {                                              //#G
                CustomerName = dto.UserId,                 //#G
                LineItems = FormLineItemsWithErrorChecking (dto.LineItems, booksDict)        //#G
            };                                             //#G

            if (!HasErrors)           //#H
                _dbAccess.Add(order); //#H

            return HasErrors ? null : order; //#I
        }

        private List<LineItem> FormLineItemsWithErrorChecking(IEnumerable<OrderLineItem> lineItems, IDictionary<int, Book> booksDict) //#J
        {
            var result = new List<LineItem>();
            var i = 1;

            foreach (var lineItem in lineItems)  //#K
            {
                if (!booksDict.ContainsKey(lineItem.BookId)) //#L
                    throw new InvalidOperationException ($"An order failed because book, id = {lineItem.BookId} was missing."); //#L

                var book = booksDict[lineItem.BookId];
                var bookPrice = book.Promotion?.NewPrice ?? book.Price; //#M
                if (bookPrice <= 0)
                {
                    AddError($"Sorry, the book '{book.Title}' is not for sale.");    //#N
                }
                else
                {
                    result.Add(new LineItem         //#O
                    {                               //#O
                        BookPrice = bookPrice,      //#O
                        ChosenBook = book,          //#O
                        LineNum = (byte)(i++),      //#O
                        NumBooks = lineItem.NumBooks//#O
                    });
                }
            }
            return result; //#p
        }
        
        /**************************************************************
        #A The BizActionErrors class provides all the error handling that is required for the business logic
        #B The IBizAction interface makes the business logic conform to a standard interface for business logic that has an input and an output
        #C The PlaceOrderAction needs the companion PlaceOrderDbAccess class to handle all the database accesses
        #D This is the method that is called by the BizRunner to execute this business logic
        #E I start with some basic validation
        #F Now I ask the PlaceOrderDbAccess class to find all the books I need, with any optional PriceOffers
        #G I create the Order entity class. Note the call to the private method FormLineItemsWithErrorChecking which creates the LineItems
        #H I only add the order to the database if there are no errors 
        #I If there are errors I return null, otherwise I return the order
        #J This private method handles the creation of each LineItem entity class for each book ordered
        #K This goes through each book type that the person has ordered
        #L I treat a book being missing as a system error, and throw an exception
        #M I calculate the price at the time of the order
        #N More validation where I check the book can be sold
        #O All is OK, so now I can create the LineItem entity class with the details
        #P I return all the LineItems for this order
         * ***********************************************************/
    }
}
