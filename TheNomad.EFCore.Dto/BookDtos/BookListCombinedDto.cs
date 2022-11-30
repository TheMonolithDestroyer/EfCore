using System;
using System.Collections.Generic;
using System.Text;

namespace TheNomad.EFCore.Dto.BookDtos
{
    public class BookListCombinedDto : TraceIdentBaseDto
    {
        public BookListCombinedDto(string traceIdentifier, SortFilterPageOptions sortFilterPageData, IEnumerable<BookListDto> booksList)
            : base(traceIdentifier)
        {
            SortFilterPageData = sortFilterPageData;
            BooksList = booksList;
        }

        public SortFilterPageOptions SortFilterPageData { get; private set; }

        public IEnumerable<BookListDto> BooksList { get; private set; }
    }
}
