using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Services.AdminServices
{
    public interface IChangePriceOfferService
    {
        Book ChangePriceOffer(PriceOffer priceOffer);
        PriceOffer GetOriginal(int id);
        Book UpdateBook(PriceOffer promotion);
    }
}
