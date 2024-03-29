﻿using Microsoft.AspNetCore.Http;
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
    public class PlaceOrderService
    {
        private readonly CheckoutCookie _checkoutCookie;  //#A
        private readonly RunnerWriteDb<PlaceOrderInDto, Order> _runner;//#B

        public IImmutableList<ValidationResult> Errors => _runner.Errors; //#C

        public PlaceOrderService(              //#D
            IRequestCookieCollection cookiesIn,//#D 
            IResponseCookies cookiesOut,       //#D
            AppDbContext context)             //#D
        {
            _checkoutCookie = new CheckoutCookie(cookiesIn, cookiesOut); //#E

            var action = new PlaceOrderAction(new PlaceOrderDbAccess(context));
            _runner = new RunnerWriteDb<PlaceOrderInDto, Order>(action, context); //#F
        }

        public int PlaceOrder(bool acceptTAndCs) //#G
        {
            var checkoutService = new CheckoutCookieService(_checkoutCookie.GetValue()); //#H

            var order = _runner.RunAction(new PlaceOrderInDto(acceptTAndCs, checkoutService.UserId, checkoutService.LineItems)); //#I

            if (_runner.HasErrors) 
                return 0; //#J

            //successful so clear the cookie line items
            checkoutService.ClearAllLineItems();   //#K
            
            _checkoutCookie.AddOrUpdateCookie(checkoutService.EncodeForCookie());//#K

            return order.OrderId;//#L
        }
    }
    /***********************************************************
    #A This is a class that handles the checkout cookie. This is a cookie, but with a specfic name and expiry time
    #B This is the BizRunner that I am going to use to execute the business logic. It is of Type RunnerWriteDb<TIn, TOut>
    #C This holds any errors sent back from the business logic. The caller can use these to redisplay the page and show the errors that need fixing
    #D The constructor needs access to the cookies, both in and out, and the application's DbContext
    #E I create a CheckoutCookie using the cookie in/out access parts from ASP.NET Core
    #F I create the BizRunner, with the business logic, PlaceOrderAction, that I want to run. PlaceOrderAction needs PlaceOrderDbAccess when it is created
    #G This is the method I call from the ASP.NET action that is called when the user presses the Purchase button
    #H The CheckoutCookieService is a class that encodes/decodes the checkout data into a string that goes inside the checkout cookie
    #I I am now ready to run the business logic, handing it the checkout information in the format that it needs
    #J If the business logic has any errors then I return immediately. The checkout cookie has not been cleared so the user can try again
    #K If I get here then the order was placed successfully. I therefore clear the checkout cookie of the order parts
    #L I return the OrderId, that is, the primary key of the order, which ASP.NET uses to show a confirmation page which includes the order details
     * *********************************************************/
}
