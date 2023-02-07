using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Chapter07.SplitOwnClasses
{
    public class Address //#C
    {
        public string NumberAndStreet { get; set; }
        public string City { get; set; }
        public string ZipPostCode { get; set; }
        public string CountryCodeIso2 { get; set; }
    }
    /****************************************************
    #C A owned type has no primary key - this type of class is refered to as a 'Value Object' in Domain-Driven Design terms
     * **************************************************/
}
