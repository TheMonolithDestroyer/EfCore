using System;
using System.ComponentModel.DataAnnotations;
namespace TheNomad.EFCore.Services.AdminServices
{
    public class ChangePubDateDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishedOn { get; set; }
    }
}
