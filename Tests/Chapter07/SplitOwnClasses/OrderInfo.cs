using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Chapter07.SplitOwnClasses
{
    public class OrderInfo //#A
    {
        public int OrderInfoId { get; set; }
        public string OrderNumber { get; set; }

        public Address BillingAddress { get; set; } //#B
        public Address DeliveryAddress { get; set; } //#B
    }

    /**********************************************************
    #A The entity class OrderInfo, with a primary key and two addresses
    #B There are two, distinct Address classes. The data for each address class will be included in the table that the OrderInfo is mapped to
     * *********************************************************/
}
