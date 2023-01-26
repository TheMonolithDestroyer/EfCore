using System.ComponentModel.DataAnnotations;

namespace Tests.Chapter07.EfClasses
{
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }
        public string MobileNumber { get; set; }
        public string LandlineNumber { get; set; }

        /// <summary>
        /// is used as a foreign key for the Person entity to link to this contact info
        /// </summary>
        [MaxLength(256)]
        [Required]
        public string EmailAddress { get; set; }
    }
}