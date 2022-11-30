using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace TheNomad.EFCore.Utils.Enums
{
    public enum OrderByOptions
    {
        [Display(Name = "sort by...")]
        SimpleOrder = 0,
        [Display(Name = "Votes ↑")]
        ByVotes,
        [Display(Name = "Publication Date ↑")]
        ByPublicationDate,
        [Display(Name = "Price ↓")]
        ByPriceLowestFirst,
        [Display(Name = "Price ↑")]
        ByPriceHigestFirst
    }
}
