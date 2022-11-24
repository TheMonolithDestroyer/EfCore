using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Entity;
using TheNomad.EFCore.Services.BookServices;

namespace TheNomad.EFCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public Book GetFirst()
        {
            var service = new BookService(_context);
            return service.GetFirst();
        }
    }
}
