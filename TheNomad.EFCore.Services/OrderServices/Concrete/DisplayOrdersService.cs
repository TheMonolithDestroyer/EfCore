using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Services.CheckoutServices;
using TheNomad.EFCore.Services.CheckoutServices.Concrete;

namespace TheNomad.EFCore.Services.OrderServices.Concrete
{
    public class DisplayOrdersService
    {
        private readonly AppDbContext _context;

        public DisplayOrdersService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This lists existing orders
        /// </summary>
        /// <returns></returns>
        public List<OrderListDto> GetUsersOrders(IRequestCookieCollection cookiesIn)
        {
            var cookie = new CheckoutCookie(cookiesIn);
            var service = new CheckoutCookieService(cookie.GetValue());

            return SelectQuery(_context.Orders
                        .OrderByDescending(x => x.DateOrderedUtc)
                        .Where(x => x.CustomerName == service.UserId))
                    .ToList();
        }


        public OrderListDto GetOrderDetail(int orderId)
        {
            var order = SelectQuery(_context.Orders).SingleOrDefault(x => x.OrderId == orderId);

            if (order == null)
                throw new NullReferenceException($"Could not find the order with id of {orderId}.");

            return order;
        }

        //---------------------------------------------
        //private methods

        private IQueryable<OrderListDto> SelectQuery(IQueryable<Order> orders)
        {
            return orders.Select(x => new OrderListDto
            {
                OrderId = x.OrderId,
                DateOrderedUtc = x.DateOrderedUtc,
                LineItems = x.LineItems.Select(lineItem => new CheckoutItemDto
                {
                    BookId = lineItem.BookId,
                    Title = lineItem.ChosenBook.Title,
                    AuthorsName = string.Join(", ",
                     lineItem.ChosenBook.AuthorsLink
                         .OrderBy(q => q.Order)
                         .Select(q => q.Author.Name)),
                    BookPrice = lineItem.BookPrice,
                    NumBooks = lineItem.NumBooks,
                })
            });
        }
    }
}
