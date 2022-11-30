using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace TheNomad.EFCore.Utils.Enums
{
    public enum BooksFilterBy
    {
        [Display(Name = "All")]
        NoFilter = 0,
        [Display(Name = "By Votes...")]
        ByVotes,
        [Display(Name = "By Year published...")]
        ByPublicationYear
    }
}
