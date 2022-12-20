using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Services.AdminServices
{
    public interface IChangeAuthorService
    {
        Author GetAuthor(int id);
        Author UpdateDisconectedAuthor(ChangeAuthorNameDto dto);
        Author UpdateDisconectedAuthorV2();
    }
}
