using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tests.Chapter07.EfClasses
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// holds the ASP.NET authorization UserId, which is the person's email address and is unique 
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string UserId { get; set; }
        public ContactInfo ContactInfo { get; set; }

        /// <summary>
        /// Links LibrarianBooks to the Librarian navigational property in the LibraryBook class
        /// </summary>
        [InverseProperty("Librarian")]
        public ICollection<LibraryBook> LibrarianBooks { get; set; }

        /// <summary>
        /// Links the BooksBorrowedByMe to the OnLoanTo navigational property in the LibraryBook class
        /// </summary>
        [InverseProperty("OnLoanTo")]
        public ICollection<LibraryBook> BooksBorrowedByMe { get; set; }
    }
}