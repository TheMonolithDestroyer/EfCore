using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Chapter07.EfClasses
{
    public class PaymentCard : Payment
    {
        public string ReceiptCode { get; set; }
    }
}
