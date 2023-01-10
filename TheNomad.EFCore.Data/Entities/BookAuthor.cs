
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheNomad.EFCore.Data.Entities
{
    public class BookAuthor                    //#G
    {
        [Key]
        [Column(Order = 0)]
        public int BookId { get; set; }        //#H
        [Key]
        [Column(Order = 1)]
        public int AuthorId { get; set; }      //#H
        public byte Order { get; set; }        //#I
        public Book Book { get; set; }        //#J
        public Author Author { get; set; }    //#K
    }
    /**************************************************
    #G The BookAuthor class is the Many-to-Many linking table between the Books and Authors tables
    #H The Primary Key is made up of the two keys of the Book and Author
    #I The order of the Authors in a book matters, so I use this to set the right order
    #J This is the link to the Book side of the relationship
    #K And this links to the Author side of the relationship
     * ***********************************************/
}
