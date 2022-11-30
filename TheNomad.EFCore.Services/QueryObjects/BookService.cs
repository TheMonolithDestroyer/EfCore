using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Dto.BookDtos;
using TheNomad.EFCore.Services.Concrete;

namespace TheNomad.EFCore.Services.QueryObjects
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

        public Book EagerGetFirstWithRelations()
        {
            var book = _context.Books
                .Include(a => a.AuthorsLink)
                    .ThenInclude(a => a.Author)
                .Include(r => r.Reviews)
                .Include(p => p.Promotion)
                .AsNoTracking()
                .FirstOrDefault();

            return book;
        }

        public object ExplicitGetFirst()
        {
            var book = _context.Books.First();
            var numReviews = _context.Entry(book).Collection(b => b.Reviews).Query().Count();
            var starRatings = _context.Entry(book).Collection(b => b.Reviews).Query().Select(x => x.NumStars).ToList();

            return new
            {
                Book = book,
                NumberOfReviews = numReviews,
                StarRatings = starRatings
            };
        }
        
        public object SelectiveGetFirst()
        {
            var result = _context.Books
                .OrderBy(i => i.BookId)
                .Select(p => new
                {
                    p.Title,
                    p.Price,
                    NumberOfReviews = p.Reviews.Count
                }).First();
            
            return result;
        }

        public object ClientServerEvaluation()
        {
            var book = _context.Books
                .OrderBy(i => i.BookId)
                .Select(i => new
                {
                    i.BookId,
                    i.Title,
                    AuthorsString = string.Join(", ", i.AuthorsLink.OrderBy(j => j.Order).Select(j => j.Author.Name))
                }).First();

            return book;
        }
    }
}
