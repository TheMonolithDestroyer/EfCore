using System;
using System.Collections.Generic;
using System.Linq;
using TheNomad.EFCore.Data.EfCode;
using TheNomad.EFCore.Services.BookService.QueryObjects;
using TheNomad.EFCore.Utils.Enums;

namespace TheNomad.EFCore.Services.BookService.Concrete
{
    public class BookFilterDropdownService
    {
        private readonly AppDbContext _db;

        public BookFilterDropdownService(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<DropdownTuple> GetFilterDropDownValues(BooksFilterBy filterBy)
        {
            switch (filterBy)
            {
                case BooksFilterBy.NoFilter:
                    return new List<DropdownTuple>();
                case BooksFilterBy.ByVotes:
                    return FormVotesDropDown();
                case BooksFilterBy.ByPublicationYear:
                    var comingSoon = _db.Books.Any(x => x.PublishedOn > DateTime.UtcNow);  //#A
                    var nextYear = DateTime.UtcNow.AddYears(1).Year;//#B

                    var result = _db.Books                          //#C
                        .Select(x => x.PublishedOn.Year)            //#C
                        .Distinct()                                 //#C
                        .Where(x => x < nextYear)                   //#C
                        .OrderByDescending(x => x)                  //#C
                        .Select(x => new DropdownTuple              //#D
                        {                                           //#D
                            Value = x.ToString(),                   //#D
                            Text = x.ToString()                     //#D
                        }).ToList();                                //#D

                    if (comingSoon)                                 //#E
                        result.Insert(0, new DropdownTuple          //#E
                        {
                            Value = BookListDtoFilter.AllBooksNotPublishedString,
                            Text = BookListDtoFilter.AllBooksNotPublishedString
                        });

                    return result;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterBy), filterBy, null);
            }

            /*****************************************************************
               #A This returns true if there is a book in the list that is not yet published
               #B This gets next year so we can filter out all future publications
               #C This long command gets the year of publication, uses distinct to only have one of each year, 
                   filters out the future books and orders with newest year at the top
               #D I finally use two client/server evaluations to turn the values into strings
               #E Finally I add a "coming soon" filter for all the future books
                * ***************************************************************/
        }

        private static IEnumerable<DropdownTuple> FormVotesDropDown()
        {
            return new[]
            {
                new DropdownTuple {Value = "4", Text = "4 stars and up"},
                new DropdownTuple {Value = "3", Text = "3 stars and up"},
                new DropdownTuple {Value = "2", Text = "2 stars and up"},
                new DropdownTuple {Value = "1", Text = "1 star and up"},
            };
        }
    }
}
