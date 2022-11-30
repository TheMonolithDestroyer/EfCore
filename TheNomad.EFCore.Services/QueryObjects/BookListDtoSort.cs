using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data.Entities;
using TheNomad.EFCore.Dto.BookDtos;
using TheNomad.EFCore.Utils.Enums;

namespace TheNomad.EFCore.Services.QueryObjects
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
                OrderByOptions.ByPriceHigestFirst => books.OrderByDescending(_ => _.ActualPrice),
                _ => throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null) 
            };
    }
}
