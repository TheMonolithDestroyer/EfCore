using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Services.BookService.Concrete
{
    public class BookService2
    {
        private readonly AppDbContext _context;
        public BookService2(AppDbContext context)
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

        public void CreateBookOneAuthor()
        {
            var oneBook = CreateDummyBookOneAuthor();
            _context.Add(oneBook);
            _context.SaveChanges();

            var book = new Book
            {
                Title = "Test Book",
                PublishedOn = DateTime.Today
            };
            book.AuthorsLink = new List<BookAuthor>
            {
                new BookAuthor
                {
                    Book = book,
                    Author = oneBook.AuthorsLink.First().Author
                }
            };
            _context.Add(book);
            _context.SaveChanges();
        }

        public static Book CreateDummyBookOneAuthor()
        {
            var book = new Book
            {
                Title = "Book Title",
                Description = "Book Description",
                Price = 123,
                PublishedOn = new DateTime(2010, 1, 1)
            };

            var author = new Author { Name = "Test Author" };
            book.AuthorsLink = new List<BookAuthor>
            {
                new BookAuthor {Book = book, Author = author},
            };

            return book;
        }
    }
}
