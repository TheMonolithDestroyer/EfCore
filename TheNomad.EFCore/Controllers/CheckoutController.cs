using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Services.CheckoutServices.Concrete;
using TheNomad.EFCore.Services.OrderServices.Concrete;

namespace TheNomad.EFCore.Api.Controllers
{
    //[ApiController]
    //[Route("api/[controller]/[action]")]
    public class CheckoutController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CheckoutController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listService = new CheckoutListService(_context, HttpContext.Request.Cookies);
            return Ok(listService.GetCheckoutList());
        }

        //[HttpPost]
        public IActionResult PlaceOrder(bool acceptTAndCs)
        {
            var service = new PlaceOrderService(HttpContext.Request.Cookies, HttpContext.Response.Cookies, _context);
            var orderId = service.PlaceOrder(acceptTAndCs);

            if (!service.Errors.Any())
                return Ok(new { orderId });

            var listService = new CheckoutListService(_context, HttpContext.Request.Cookies);
            return BadRequest(listService.GetCheckoutList());
        }
    }
}
