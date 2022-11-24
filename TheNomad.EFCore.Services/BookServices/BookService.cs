using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Entity;

namespace TheNomad.EFCore.Services.BookServices
{
    public class BookService
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public Book GetFirst()
        {
            return _context.Books.Include(r => r.Reviews).FirstOrDefault();
        }
    }
}
