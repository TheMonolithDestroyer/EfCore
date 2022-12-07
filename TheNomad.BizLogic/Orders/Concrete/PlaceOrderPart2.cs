using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.BizDbAccess.Orders;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.BizLogic.Orders.Concrete
{
    public class PlaceOrderPart2 : BizActionErrors, IPlaceOrderPart2
    {
        private readonly IPlaceOrderDbAccess _dbAccess;

        public PlaceOrderPart2(IPlaceOrderDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Order Action(Part1ToPart2Dto dto)
        {
            var booksDict = _dbAccess.FindBooksByIdsWithPriceOffers (dto.LineItems.Select(x => x.BookId));
            
            dto.Order.LineItems = FormLineItemsWithErrorChecking(dto.LineItems, booksDict);

            return HasErrors ? null : dto.Order;
        }

        private List<LineItem> FormLineItemsWithErrorChecking (IEnumerable<OrderLineItem> lineItems, IDictionary<int, Book> booksDict)  //#I
        {
            var result = new List<LineItem>();
            var i = 1;

            foreach (var lineItem in lineItems)
            {
                if (!booksDict.ContainsKey(lineItem.BookId))
                {
                    throw new InvalidOperationException($"Could not find the {i} book you wanted to order. Please remove that book and try again.");
                }

                var book = booksDict[lineItem.BookId];
                var bookPrice = book.Promotion?.NewPrice ?? book.Price;
                
                if (book.PublishedOn > DateTime.UtcNow)
                {
                    AddError($"Sorry, the book '{book.Title}' is not yet in print.");
                }
                else if (bookPrice <= 0)
                {
                    AddError($"Sorry, the book '{book.Title}' is not for sale.");
                }
                else
                {
                    result.Add(new LineItem
                    {
                        BookPrice = bookPrice,
                        ChosenBook = book,
                        LineNum = (byte)(i++),
                        NumBooks = lineItem.NumBooks
                    });
                }
            }
            return result;
        }
    }
}
