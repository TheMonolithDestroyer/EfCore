using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data;

namespace TheNomad.EFCore.Services.AdminServices.Concrete
{
    public class ChangePubDateService
    {
        private readonly AppDbContext _context;

        public ChangePubDateService(AppDbContext context)
        {
            _context = context;
        }

        public void ChangeBookPubDate()
        {
            var book = _context.Books.Single(p => p.Title == "Quantum Networking");
            book.PublishedOn = new DateTime(2058, 1, 1);
            _context.SaveChanges();
        }
    }
}
