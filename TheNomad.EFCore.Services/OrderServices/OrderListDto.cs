using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Services.CheckoutServices;

namespace TheNomad.EFCore.Services.OrderServices
{
    public class OrderListDto
    {
        public int OrderId { get; set; }

        public DateTime DateOrderedUtc { get; set; }

        public string OrderNumber => $"SO{OrderId:D6}";

        public IEnumerable<CheckoutItemDto> LineItems { get; set; }
    }
}
