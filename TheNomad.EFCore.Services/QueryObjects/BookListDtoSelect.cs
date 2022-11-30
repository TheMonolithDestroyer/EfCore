using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Dto.BookDtos;

namespace TheNomad.EFCore.Services.QueryObjects
{
    public static class BookListDtoSelect
    {
        public static IQueryable<BookListDto> MapBookDto(this IQueryable<Book> books) //#A
        {
            return books.Select(i => new BookListDto
            {
                BookId = i.BookId, //#B
                Title = i.Title, //#B
                Price = i.Price, //#B
                PublishedOn = i.PublishedOn, //#B
                ActualPrice = i.Promotion == null ? i.Price : i.Promotion.NewPrice, //#C
                PromotionPromotionalText = i.Promotion == null ? null : i.Promotion.PromotionalText, //#D
                AuthorsOrdered = string.Join(", ", i.AuthorsLink.OrderBy(j => j.Order).Select(j => j.Author.Name)), //#E
                ReviewsCount = i.Reviews.Count, //#F
                ReviewsAverageVotes = i.Reviews.Select(j => (double?)j.NumStars).Average() //#G
            });

            /*********************************************************
            #A This method takes in IQueryable<Book> and returns IQueryable<BookListDto>
            #B These are simple copies of existing columns in the Books table
            #C This calculates the selling price, which is the normal price, or the promotion price if that relationship exists 
            #D The PromotionalText depends on whether a PriceOffer exists for this book
            #E This obtains an array of Authors' names, in the right order. We are using a Client vs. Server evaluation as we want the author's names combined into one string
            #F We need to calculate how many reviews there are
            #G To get EF Core to turn the LINQ average into the SQL AVG command I need to cast the NumStars to (double?)
            * *******************************************************/
        }
    }
}
