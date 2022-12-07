using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.BizDbAccess.Orders;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.BizLogic.Orders.Concrete
{
    public class PlaceOrderPart1 : BizActionErrors, IPlaceOrderPart1
    {
        private readonly IPlaceOrderDbAccess _dbAccess;

        public PlaceOrderPart1(IPlaceOrderDbAccess dbAccess)//#C
        {
            _dbAccess = dbAccess;
        }

        public Part1ToPart2Dto Action(PlaceOrderInDto dto)
        {
            if (!dto.AcceptTAndCs)
                AddError("You must accept the T&Cs to place an order.");

            if (!dto.LineItems.Any())
                AddError("No items in your basket.");

            var order = new Order
            {
                CustomerName = dto.UserId
            };

            if (!HasErrors)
                _dbAccess.Add(order);

            return new Part1ToPart2Dto(dto.LineItems, order);
        }
    }
}
