using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data.EfCode;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Services.AdminServices.Concrete
{
    public class ChangePriceOfferService : IChangePriceOfferService
    {
        private readonly AppDbContext _context;
        public Book OrgBook { get; private set; }

        public ChangePriceOfferService(AppDbContext context)
        {
            _context = context;
        }

        
        public Book ChangePriceOffer(PriceOffer priceOffer)
        {
            var book = _context.Books //#B
                .Include(i => i.Promotion) //#A
                .First(i => i.Promotion == null); //#B;

            book.Promotion = priceOffer; //#C
            _context.SaveChanges(); //#D

            return book;
        }
        //#A Although the include isn’t needed because you’re loading something without a Promotion, using the include is good practice, as you should load any relationships if you’re going to change a relationship.
        //#B Finds the first book that doesn’t have an existing promotion
        //#C Adds a new PriceOffer to this book
        //#D The SaveChanges method calls DetectChanges, which finds that the Promotion property has changed, so it adds that entity to the PriceOffers table

        public PriceOffer GetOriginal(int id)       //#A
        {
            OrgBook = _context.Books                //#B
                .Include(r => r.Promotion)          //#B
                .Single(k => k.BookId == id);       //#B

            return OrgBook?.Promotion ??            //#C
                new PriceOffer                      //#C
                {                                   //#C
                    BookId = id,                    //#C
                    NewPrice = OrgBook.Price        //#C
                };                                  //#C
        }
        /*********************************************************
        #A This method gets a PriceOffer class to send to the user to update
        #B This loads the book with any existing Promotion
        #C I return either the existing Promotion for editing, or create a new one. The important point is to set the BookId, as we need to pass that through to the second stage
            if a price offer is a new one, assign existing book price*/

        public Book UpdateBook(PriceOffer promotion)//#D
        {
            var book = _context.Books                       //#E
                .Include(r => r.Promotion)                  //#E
                .Single(k => k.BookId == promotion.BookId); //#E

            if (book.Promotion == null)                     //#F
            {
                book.Promotion = promotion;                 //#G
            }
            else
            {
                book.Promotion.NewPrice = promotion.NewPrice;   //#H
                book.Promotion.PromotionalText = promotion.PromotionalText;    //#H
            }
            _context.SaveChanges();                 //#I

            return book;                            //#J
        }
        /*#D This method handles the second part of the update, i.e. performing a selective update of the chosen book
        #E This loads the book, with any existing promotion, which is important as otherwise our new PriceOffer will clash, and throw an error
        #F I check if this an update of an existing PriceOffer or adding a new PriceOffer
        #G I need to add a new ProceOffer, so I simply assign the promotion to the relational link. EF Core will see this and add a new row in the PriceOffer table
        #H I need to do an update, so I copy over just the parts that I want to change. EF Core will see this update and produce code to unpdate just these two columns
        #I The SaveChanges uses its DetectChanges method, which sees what has changes - either adding a new PriceOffer or updating an existing PriceOffer
        #H The method returns the updated book*/
    }
}
