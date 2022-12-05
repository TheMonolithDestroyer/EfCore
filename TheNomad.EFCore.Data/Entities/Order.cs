using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheNomad.EFCore.Data.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime DateOrderedUtc { get; set; }
        /// <summary>
        /// In this simple example the cookie holds a GUID for everyone that 
        /// </summary>
        public Guid CustomerName { get; set; }
        public ICollection<LineItem> LineItems { get; set; }

        // Extra columns not used by EF

        public string OrderNumber => $"SO{OrderId:D6}";

        public Order()
        {
            DateOrderedUtc = DateTime.UtcNow;
        }
    }
}
