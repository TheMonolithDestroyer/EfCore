using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.BizLogic.Orders
{
    public class Part1ToPart2Dto
    {
        public IImmutableList<OrderLineItem> LineItems { get; private set; }

        public Order Order { get; private set; }

        public Part1ToPart2Dto(IImmutableList<OrderLineItem> lineItems, Order order)
        {
            LineItems = lineItems;
            Order = order;
        }
    }
}
