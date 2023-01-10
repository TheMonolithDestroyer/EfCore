using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheNomad.BizDbAccess.Orders;
using TheNomad.BizLogic.Orders;
using TheNomad.BizLogic.Orders.Concrete;
using TheNomad.EFCore.Data.EfCode;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Services.BizRunners;
using TheNomad.EFCore.Services.CheckoutServices.Concrete;

namespace TheNomad.EFCore.Services.OrderServices.Concrete
{
    public class PlaceOrderServiceTransact
    {
        private readonly CheckoutCookie _checkoutCookie;
        private readonly RunnerTransactWriteDb <PlaceOrderInDto, Part1ToPart2Dto, Order> _runner;

        public IImmutableList<ValidationResult> Errors => _runner.Errors;

        public PlaceOrderServiceTransact(
            IRequestCookieCollection cookiesIn,
            IResponseCookies cookiesOut,
            AppDbContext context)
        {
            _checkoutCookie = new CheckoutCookie(cookiesIn, cookiesOut);

            var action1 = new PlaceOrderPart1(new PlaceOrderDbAccess(context));
            var action2 = new PlaceOrderPart2(new PlaceOrderDbAccess(context));
            _runner = new RunnerTransactWriteDb<PlaceOrderInDto, Part1ToPart2Dto, Order>(context, action1, action2); //#F
        }

        public int PlaceOrder(bool tsAndCsAccepted)
        {
            var checkoutService = new CheckoutCookieService(_checkoutCookie.GetValue());

            var order = _runner.RunAction(new PlaceOrderInDto(tsAndCsAccepted, checkoutService.UserId, checkoutService.LineItems));

            if (_runner.HasErrors) 
                return 0;

            checkoutService.ClearAllLineItems();
            
            _checkoutCookie.AddOrUpdateCookie(checkoutService.EncodeForCookie());

            return order.OrderId;
        }
    }
}
