using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Services.AdminServices.Concrete
{
    public class ChangeAuthorService
    {
        private readonly AppDbContext _context;

        public ChangeAuthorService(AppDbContext context)
        {
            _context = context;
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors
                .Select(b => new Author
                {
                    AuthorId = b.AuthorId,
                    Name = b.Name
                })
                .Single(k => k.AuthorId == id);
        }

        public Author UpdateDisconectedAuthor(ChangeAuthorNameDto dto)
        {

            var author = new Author 
            { 
                AuthorId = dto.AuthorId, 
                Name = dto.Name 
            };
            
            _context.Authors.Update(author);
            _context.SaveChanges();

            return author;
        }

        public Author UpdateDisconectedAuthorV2()
        {
            string json;
            Author author = null;

            using (var context = _context.GetAppDbContext())
            {
                author = context.Books
                    .Where(p => p.Title == "Quantum Networking")
                    .Select(p => p.AuthorsLink.First().Author)
                    .Single();
                author.Name = "Future Person 2";
                json = JsonConvert.SerializeObject(author);
            }

            author = null;
            using (var context = _context.GetAppDbContext())
            {
                author = JsonConvert.DeserializeObject<Author>(json);
                context.Authors.Update(author);
                context.SaveChanges();
            }

            return author;
        }
    }
}
