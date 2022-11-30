using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Dto.BookDtos;
using TheNomad.EFCore.Services;
using TheNomad.EFCore.Services.Concrete;
using TheNomad.EFCore.Services.QueryObjects;
using TheNomad.EFCore.Utils;

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
            var service = new BookService(_context);
            return service.GetFirst();
        }

        [HttpGet]
        public Book EagerGetFirstWithRelations()
        {
            var service = new BookService(_context);
            return service.EagerGetFirstWithRelations();
        }

        [HttpGet]
        public object ExplicitGetFirst()
        {
            var service = new BookService(_context);
            return service.ExplicitGetFirst();
        }

        [HttpGet]
        public object SelectiveGetFirst()
        {
            var service = new BookService(_context);
            return service.SelectiveGetFirst();
        }

        [HttpGet]
        public object ClientServerEvaluation()
        {
            var service = new BookService(_context);
            return service.ClientServerEvaluation();
        }

        [HttpPost]
        public List<BookListDto> GetBookListDto([FromBody] SortFilterPageOptions options)
        {
            var service = new ListBooksService(_context);
            return service.SortFilterPage(options).ToList();
        }
    }
}
