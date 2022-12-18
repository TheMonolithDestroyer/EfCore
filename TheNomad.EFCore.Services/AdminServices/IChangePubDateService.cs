using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Services.AdminServices
{
    public interface IChangePubDateService
    {
        ChangePubDateDto GetBook(int id);
        Book UpdateBook(ChangePubDateDto dto);
    }
}
