using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.BizLogic.GenericInterfaces;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.BizLogic.Orders
{
    public interface IPlaceOrderPart2 : IBizAction<Part1ToPart2Dto, Order> { }
}
