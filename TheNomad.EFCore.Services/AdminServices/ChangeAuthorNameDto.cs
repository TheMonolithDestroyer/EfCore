using System;
using System.Collections.Generic;
using System.Text;

namespace TheNomad.EFCore.Services.AdminServices
{
    public class ChangeAuthorNameDto
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
    }
}
