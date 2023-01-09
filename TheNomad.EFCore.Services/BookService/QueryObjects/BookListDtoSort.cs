using System;
using System.Linq;
using TheNomad.EFCore.Utils.Enums;

namespace TheNomad.EFCore.Services.BookService.QueryObjects
{
    public static class BookListDtoSort
    {
        public static IQueryable<BookListDto> OrderBooksBy(this IQueryable<BookListDto> books, OrderByOptions orderByOptions) =>
            orderByOptions switch
            {
                OrderByOptions.SimpleOrder => books.OrderByDescending(_ => _.BookId),
                OrderByOptions.ByVotes => books.OrderByDescending(_ => _.ReviewsAverageVotes),
                OrderByOptions.ByPublicationDate => books.OrderByDescending(_ => _.PublishedOn),
                OrderByOptions.ByPriceLowestFirst => books.OrderBy(_ => _.ActualPrice),
                OrderByOptions.ByPriceHighestFirst => books.OrderByDescending(_ => _.ActualPrice),
                _ => throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null)
            };
    }
}
