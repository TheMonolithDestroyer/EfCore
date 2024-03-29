﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheNomad.EFCore.Services.CheckoutServices
{
    public class CheckoutItemDto
    {
        public int BookId { get; internal set; }

        public string Title { get; internal set; }

        public string AuthorsName { get; internal set; }

        public decimal BookPrice { get; internal set; }

        public string ImageUrl { get; set; }

        public short NumBooks { get; internal set; }
    }
}
