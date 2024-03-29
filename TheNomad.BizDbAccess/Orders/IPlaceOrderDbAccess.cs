﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data.EfCode;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.BizDbAccess.Orders
{
    public interface IPlaceOrderDbAccess
    {
        IDictionary<int, Book> FindBooksByIdsWithPriceOffers(IEnumerable<int> bookIds);
        void Add(Order newOrder);
    }

    public class PlaceOrderDbAccess : IPlaceOrderDbAccess
    {
        private readonly AppDbContext _context;
        public PlaceOrderDbAccess(AppDbContext context) //#A
        {
            _context = context;
        }

        /// <summary>
        /// This finds any books that fits the BookIds given to it, with any optional promotion
        /// </summary>
        /// <param name="bookIds"></param>
        /// <returns>A dictionary with the BookId as the key, and the Book as the value</returns>
        public IDictionary<int, Book>
            FindBooksByIdsWithPriceOffers               //#B
               (IEnumerable<int> bookIds)               //#C
        {
            return _context.Books                       //#D
                .Where(x => bookIds.Contains(x.BookId)) //#D
                .Include(r => r.Promotion)              //#E
                .ToDictionary(key => key.BookId);       //#F
        }

        public void Add(Order newOrder)                 //#G 
        {                                               //#G
            _context.Orders.Add(newOrder);              //#G
        }

        /************************************************************
        #A All the BizDbAccess need the application's DbContext to access the database
        #B This method finds all the books that the user wants to buy
        #C The BizLogic hands it a collection of BookIds, which the checkout has provided
        #D This finds a book, if present, for each Id. 
        #E I include any optional promotion, as the BizLogic needs that for working out the price
        #F I return the result as a dictionary to make it easier for the BizLogic to look them up
        #G This method simply adds the new order that the BizlOgic built into the DbContext's Orders DbSet collection
         * **********************************************************/
    }
}
