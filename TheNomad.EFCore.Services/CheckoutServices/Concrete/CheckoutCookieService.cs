using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using TheNomad.BizLogic.Orders;

namespace TheNomad.EFCore.Services.CheckoutServices.Concrete
{
    public class CheckoutCookieService
    {
        private List<OrderLineItem> _lineItems;

        public Guid UserId { get; private set; }
        public ImmutableList<OrderLineItem> LineItems => _lineItems.ToImmutableList();

        public CheckoutCookieService(string cookieContent)
        {
            DecodeCookieString(cookieContent);
        }

        public CheckoutCookieService(IRequestCookieCollection cookiesIn)
        {
            var cookieHandler = new CheckoutCookie(cookiesIn);
            DecodeCookieString(cookieHandler.GetValue());
        }

        public void AddLineItem(OrderLineItem newItem)
        {
            _lineItems.Add(newItem);
        }

        public void UpdateLineItem(int itemIndex, OrderLineItem replacement)
        {
            if (itemIndex < 0 || itemIndex > _lineItems.Count)
                throw new InvalidOperationException($"System error. Attempt to remove line item index {itemIndex}. _lineItems.Count = {_lineItems.Count}");

            _lineItems[itemIndex] = replacement;
        }

        public void DeleteLineItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex > _lineItems.Count)
                throw new InvalidOperationException($"System error. Attempt to remove line item index {itemIndex}. _lineItems.Count = {_lineItems.Count}");

            _lineItems.RemoveAt(itemIndex);
        }

        public void ClearAllLineItems()
        {
            _lineItems.Clear();
        }

        public string EncodeForCookie()
        {
            var sb = new StringBuilder();
            sb.Append(UserId.ToString("N"));
            foreach (var lineItem in _lineItems)
            {
                sb.AppendFormat(",{0},{1}", lineItem.BookId, lineItem.NumBooks);
            }
            return sb.ToString();
        }

        //---------------------------------------------------
        //private methods

        private void DecodeCookieString(string cookieContent)
        {
            _lineItems = new List<OrderLineItem>();

            if (cookieContent == null)
            {
                //No cookie exists, so create new user and no orders
                UserId = Guid.NewGuid();
                return;
            }

            //Has cookie, so decode it
            var parts = cookieContent.Split('=');
            UserId = Guid.Parse(parts[0]);
            for (int i = 0; i < (parts.Length - 1) / 2; i++)
            {
                _lineItems.Add(new OrderLineItem
                {
                    BookId = int.Parse(parts[i * 2 + 1]),
                    NumBooks = short.Parse(parts[i * 2 + 2])
                });
            }
        }
    }
}
