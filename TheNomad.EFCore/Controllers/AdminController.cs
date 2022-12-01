using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheNomad.EFCore.Api.Helpers;
using TheNomad.EFCore.Data;
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

        [HttpGet]
        public IActionResult ResetDatabase()
        {
            Request.ThrowErrorIfNotLocal();

            _context.DevelopmentEnsureCreated();

            var numerOfBooks = _context.SeedDatabase();

            return Ok($"BookUpdated, Successfully reset the database and added { numerOfBooks } books.");
        }
    }
}
