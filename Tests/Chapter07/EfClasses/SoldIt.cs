using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tests.Chapter07.EfClasses
{
    public class SoldIt
    {
        public int SoldItId { get; set; }

        [Required]
        public string WhatSold { get; set; }

        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
    }
}
