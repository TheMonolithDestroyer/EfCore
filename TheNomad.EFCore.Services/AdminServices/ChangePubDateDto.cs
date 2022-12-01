using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
