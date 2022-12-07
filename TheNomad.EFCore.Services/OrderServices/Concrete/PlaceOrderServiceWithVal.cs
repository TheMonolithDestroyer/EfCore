using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheNomad.BizDbAccess.Orders;
using TheNomad.BizLogic.Orders.Concrete;
using TheNomad.BizLogic.Orders;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Services.BizRunners;
using TheNomad.EFCore.Services.CheckoutServices.Concrete;
using TheNomad.EFCore.Data;

namespace TheNomad.EFCore.Services.OrderServices.Concrete
{
    public class PlaceOrderServiceWithVal
    {
        private readonly CheckoutCookie _checkoutCookie;
        private readonly RunnerWriteDbWithValidation<PlaceOrderInDto, Order> _runner;

        public IImmutableList<ValidationResult> Errors => _runner.Errors;

        public PlaceOrderServiceWithVal(
            IRequestCookieCollection cookiesIn,
            IResponseCookies cookiesOut,
            AppDbContext context)
        {
            _checkoutCookie = new CheckoutCookie(cookiesIn, cookiesOut);

            var action = new PlaceOrderAction(new PlaceOrderDbAccess(context));
            _runner = new RunnerWriteDbWithValidation<PlaceOrderInDto, Order>(action,context);
        }

        public int PlaceOrder(bool acceptTAndCs)
        {
            var checkoutService = new CheckoutCookieService(_checkoutCookie.GetValue());

            var dto = new PlaceOrderInDto(acceptTAndCs, checkoutService.UserId, checkoutService.LineItems);
            var order = _runner.RunAction(dto);

            if (_runner.HasErrors) 
                return 0;

            //successful so clear the cookie line items
            checkoutService.ClearAllLineItems();

            _checkoutCookie.AddOrUpdateCookie(checkoutService.EncodeForCookie());

            return order.OrderId;
        }
    }
}
