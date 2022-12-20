using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Services.AdminServices
{
    public interface IAddReviewService
    {
        Review GetBlankReview(int id);
        Book AddReviewToBook(Review review);
    }
}
