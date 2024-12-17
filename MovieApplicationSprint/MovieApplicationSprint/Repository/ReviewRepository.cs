using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWebApplication.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private MovieAppContext _appContext;
        public ReviewRepository()
        {
            _appContext = new MovieAppContext();
        }
        public void AddReview(Review reviews)
        {
            try
            {
                _appContext.Reviews.Add(reviews);
                _appContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteReview(string reviewId)
        {
            var review = _appContext.Reviews.Find(reviewId);
            if (review != null)
            {
                try
                {
                    _appContext.Reviews.Remove(review);
                    _appContext.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Review> GetAllReviews()
        {
            try
            {
                return _appContext.Reviews.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Review GetReviewByUserId(string userId)
        {
            try
            {
                var review = _appContext.Reviews.SingleOrDefault(r => r.UserId == userId);
                return review;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateReview(Review reviews)
        {
            try
            {
                var obj = _appContext.Reviews.SingleOrDefault(r => r.ReviewId == reviews.ReviewId);
                if (obj != null)
                {
                    obj.ReviewText = reviews.ReviewText;
                    obj.Ratings = reviews.Ratings;
                    _appContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}