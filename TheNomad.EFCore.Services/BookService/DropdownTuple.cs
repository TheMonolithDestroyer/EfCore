﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheNomad.EFCore.Services.BookService
{
    public class DropdownTuple
    {
        public string Value { get; set; }

        public string Text { get; set; }

        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}, {nameof(Text)}: {Text}";
        }
    }
}
