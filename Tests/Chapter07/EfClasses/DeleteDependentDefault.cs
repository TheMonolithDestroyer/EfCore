using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Chapter07.EfClasses
{
    public class DeleteDependentDefault
    {
        public int Id { get; set; }
        public int? DeletePrincipalId { get; set; }
    }
}
