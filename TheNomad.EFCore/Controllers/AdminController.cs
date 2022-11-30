using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TheNomad.EFCore.Api.Helpers;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Services.AdminServices.Concrete;
using TheNomad.EFCore.Services.DatabaseServices.Concrete;

namespace TheNomad.EFCore.Api.Controllers
{
    public class AdminController : BaseTraceController
    {
        private readonly AppDbContext _context;
        private readonly IHostEnvironment _env;

        public AdminController(AppDbContext context, IHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult ChangePubDate(int id)
        {
            Request.ThrowErrorIfNotLocal();

            var service = new ChangePubDateService(_context);
            return Ok();
        }

        [HttpGet]
        public IActionResult ResetDatabase()
        {
            Request.ThrowErrorIfNotLocal();

            _context.DevelopmentEnsureCreated();
            var numBooks = _context.SeedDatabase();
            return Ok($"BookUpdated, Successfully reset the database and added {numBooks} books.");
        }
    }
}
