namespace Tests.Chapter07.EfClasses
{
    public class LibraryBook
    {
        public int LibraryBookId { get; set; }
        public string Title { get; set; }

        public int LibrarianPersonId { get; set; }
        public Person Librarian { get; set; }

        public int? OnLoanToPersonId { get; set; }
        public Person OnLoadTo { get; set; }
    }
}