using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;
using TheNomad.EFCore.Api.Helpers;
using TheNomad.EFCore.Data.EfCode;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Services.AdminServices;
using TheNomad.EFCore.Services.AdminServices.Concrete;
using TheNomad.EFCore.Services.DatabaseServices.Concrete;

namespace TheNomad.EFCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBook(int id)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangePubDateService(_context);
            var book = service.GetBook(id);

            return Ok(book);
        }

        [HttpPut]
        public IActionResult ChangePubDate([FromBody]ChangePubDateDto dto)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangePubDateService(_context);
            var book = service.UpdateBook(dto);
            
            return Ok(book);
        }

        [HttpGet]
        public IActionResult GetAuthor(int id)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangeAuthorService(_context);
            var book = service.GetAuthor(id);

            return Ok(book);
        }

        [HttpPut]
        public IActionResult ChangeDisconectedAuthor(ChangeAuthorNameDto dto)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangeAuthorService(_context);
            var author = service.UpdateDisconectedAuthor(dto);

            return Ok(author);
        }

        [HttpPut]
        public IActionResult ChangeDisconectedAuthorV2()
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangeAuthorService(_context);
            var author = service.UpdateDisconectedAuthorV2();

            return Ok(author);
        }

        [HttpPost]
        public IActionResult AddPromotionToBook(PriceOffer priceOffer)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangePriceOfferService(_context);
            var book = service.ChangePriceOffer(priceOffer);

            return Ok(book);
        }

        [HttpGet]
        public IActionResult GetPromotion(int id)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangePriceOfferService(_context);
            var priceOffer = service.GetOriginal(id);
            
            return Ok(priceOffer);
        }

        [HttpPost]
        public IActionResult ChangePromotion(PriceOffer dto)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangePriceOfferService(_context);
            var book = service.UpdateBook(dto);
            
            return Ok(book);
        }

        [HttpGet]
        public IActionResult GetBookReview(int id)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new AddReviewService(_context);
            var review = service.GetBlankReview(id);
            
            return Ok(review);
        }

        [HttpPost]
        public IActionResult AddBookReview(Review dto)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new AddReviewService(_context);
            var book = service.AddReviewToBook(dto);

            return Ok(book);
        }

        [HttpGet]
        public IActionResult ResetDatabase()
        {
            Request.ThrowErrorIfNotLocal();

            _context.DevelopmentEnsureCreated();

            var numerOfBooks = _context.SeedDatabase();

            return Ok($"BookUpdated, Successfully reset the database and added { numerOfBooks } books.");
        }

        [HttpGet]
        public IActionResult ChangePublicationDate(int id, [FromServices] IChangePubDateService service)
        {
            var dto = service.GetBook(id);
            return Ok(dto);
        }
    }
}
