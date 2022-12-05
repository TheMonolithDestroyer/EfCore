using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Utils.CheckoutCookies;

namespace TheNomad.EFCore.Services.CheckoutServices.Concrete
{
    public class CheckoutCookie : CookieTemplate
    {
        public const string CheckoutCookieName = "TheNomad.EFCore-Checkout";

        public CheckoutCookie(IRequestCookieCollection cookiesIn, IResponseCookies cookiesOut = null)
            : base(CheckoutCookieName, cookiesIn, cookiesOut)
        {
        }

        protected override int ExpiresInThisManyDays => 200;    //Make this last, as it holds the user id for checking orders
    }
}
