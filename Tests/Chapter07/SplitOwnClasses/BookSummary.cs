using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Chapter07.SplitOwnClasses
{
    public class BookSummary
    {
        public int BookSummaryId { get; set; }

        public string Title { get; set; }

        public string AuthorsString { get; set; }

        public BookDetail Details { get; set; }
    }
}
