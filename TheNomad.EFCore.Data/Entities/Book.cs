using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheNomad.EFCore.Utils;

namespace TheNomad.EFCore.Data.Entities
{
    public class Book //#A
    {
        [Key]
        public int BookId { get; set; } //#B
        [Required]
        [MaxLength(Constants.Title)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        [MaxLength(Constants.Publisher)]
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        [MaxLength(Constants.ImageUrl)]
        public string ImageUrl { get; set; }
        public bool SoftDeleted { get; set; }

        public PriceOffer Promotion { get; set; } //#C
        public ICollection<Review> Reviews { get; set; } //#D
        public ICollection<BookAuthor> AuthorsLink { get; set; } //#E
    }
    /****************************************************#
    #A The Book class contains the main book information
    #B I use EF Core's 'by convention' approach to defining the primary key of this entity class. In this case I use <ClassName>Id, 
        and because the property if of type int EF Core assumes that the database will use the SQL IDENTITY command to create a unique key when a new row is added
    #C This is the link to the optional PriceOffer
    #D There can be zero to many Reviews of the book
    #E This provides a link to the Many-to-Many linking table that links to the Authors of this book
     * **************************************************/

    // From the software design point of view we need review class be defined as navigational property here in order to to calculate the average review score
}
