using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheNomad.EFCore.Data.Entities
{
    public class Review                      //#L
    {
        [Key]
        public int ReviewId { get; set; }
        public string VoterName { get; set; }
        public int NumStars { get; set; }
        public string Comment { get; set; }

        //Relationships
        public int BookId { get; set; }       //#M
    }
    /*******************************************************
    #L This holds customer reviews with their rating
    #M This foreign key holds the key of the book this review belongs to
     * *****************************************************/

    // From the software design point of view we dont need backtracking navigational property "book", thus i did not defined it here.
}
