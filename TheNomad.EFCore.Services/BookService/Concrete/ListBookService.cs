using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Data.QueryObjects;
using TheNomad.EFCore.Services.BookService.QueryObjects;

namespace TheNomad.EFCore.Services.BookService.Concrete
{
    public class ListBookService
    {
        private readonly AppDbContext _context;

        public ListBookService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<BookListDto> SortFilterPage(SortFilterPageOptions options)
        {
            var booksQuery = _context.Books //#A
                .AsNoTracking() //#B
                .MapBookToDto() //#C
                .OrderBooksBy(options.OrderByOptions) //#D
                .FilterBooksBy(options.FilterBy, options.FilterValue); //#E

            options.SetupRestOfDto(booksQuery); //#F

            return booksQuery.Page(options.PageNum - 1, options.PageSize); //#G
        }

        /*********************************************************
        #A This starts by selecting the Books property in the Application's DbContext 
        #B Because this is a read-only query I add .AsNoTracking(). It makes the query faster
        #C It then uses the Select query object which will pick out/calculate the data it needs
        #D It then adds the commands to order the data using the given options
        #E Then it adds the commands to filter the data
        #F This stage sets up the number of pages and also makes sure PageNum is in the right range
        #G Finally it applies the paging commands
        * *****************************************************/
    }
}
