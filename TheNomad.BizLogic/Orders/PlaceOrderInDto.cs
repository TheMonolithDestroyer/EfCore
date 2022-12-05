using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace TheNomad.BizLogic.Orders
{
    public class PlaceOrderInDto
    {
        public bool AcceptTAndCs { get; private set; }
        public Guid UserId { get; private set; }
        public IImmutableList<OrderLineItem> LineItems { get; private set; }

        public PlaceOrderInDto(bool acceptTAndCs, Guid userId, IImmutableList<OrderLineItem> lineItems)
        {
            AcceptTAndCs = acceptTAndCs;
            UserId = userId;
            LineItems = lineItems;
        }
    }
}
