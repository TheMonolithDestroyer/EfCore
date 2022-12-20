using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Services.BookService;
using TheNomad.EFCore.Services.BookService.Concrete;

namespace TheNomad.EFCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Book GetFirst()
        {
            var service = new BookService2(_context);
            return service.GetFirst();
        }

        [HttpGet]
        public Book EagerGetFirstWithRelations()
        {
            var service = new BookService2(_context);
            return service.EagerGetFirstWithRelations();
        }

        [HttpGet]
        public object ExplicitGetFirst()
        {
            var service = new BookService2(_context);
            return service.ExplicitGetFirst();
        }

        [HttpGet]
        public object SelectiveGetFirst()
        {
            var service = new BookService2(_context);
            return service.SelectiveGetFirst();
        }

        [HttpGet]
        public object ClientServerEvaluation()
        {
            var service = new BookService2(_context);
            return service.ClientServerEvaluation();
        }

        [HttpPost]
        public List<BookListDto> GetBookListDto([FromBody] SortFilterPageOptions options)
        {
            var service = new ListBookService(_context);
            return service.SortFilterPage(options).ToList();
        }

        [HttpPost]
        public IActionResult CreateBookOneAuthor()
        {
            try
            {
                var service = new BookService2(_context);
                service.CreateBookOneAuthor();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
