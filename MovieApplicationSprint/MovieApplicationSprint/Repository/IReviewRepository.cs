using MovieApplicationSprint.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebApplication.Repository
{
    internal interface IReviewRepository
    {
        void AddReview(Review reviews);
        void DeleteReview(string reviewId);
        void UpdateReview(Review reviews);
        List<Review> GetAllReviews();
        Review GetReviewByUserId(string userId);
    }
}
