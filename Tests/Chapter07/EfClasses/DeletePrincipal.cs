using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Chapter07.EfClasses
{
    public class DeletePrincipal
    {
        public int DeletePrincipalId { get; set; }

        public DeleteDependentDefault DependentDefault { get; set; }

        public DeleteDependentSetNull DependentSetNull { get; set; }

        public DeleteDependentRestrict DependentRestrict { get; set; }

        public DeleteDependentCascade DependentCascade { get; set; }
    }
}
