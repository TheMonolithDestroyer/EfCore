using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheNomad.EFCore.Data.Entities
{
    public class PriceOffer                        //#A
    {
        [Key]
        public int PriceOfferId { get; set; }
        public decimal NewPrice { get; set; }
        public string PromotionalText { get; set; }

        //-----------------------------------------------
        //Relationships

        public int BookId { get; set; }            //#b
    }
    /***************************************************
    #N The PriceOffer is designed to override the normal price. It is a One-to-ZeroOrOne relationhsip
    #O This foreign key links back to the book it should be applied to
     * *************************************************/
}
