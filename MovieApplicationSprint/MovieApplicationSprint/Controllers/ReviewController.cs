using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repository;
using MovieWebApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieWebApplication.Controllers
{
    public class ReviewController : ApiController
    {
        private ReviewRepository reviewRepository;
        public ReviewController()
        {
            reviewRepository = new ReviewRepository();
        }
        [HttpGet, Route("GetAllReviews")]
        public IHttpActionResult GetAllReviews()
        {
            try
            {
                var reviews = reviewRepository.GetAllReviews();
                return Ok(reviews);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("GetReviewByUserId/{userId}")]
        public IHttpActionResult GetReviewByUserId(string userId)
        {
            try
            {
                var reviews = reviewRepository.GetReviewByUserId(userId);
                return Ok(reviews);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("AddReview")]
        public IHttpActionResult AddReview([FromBody] Review review)
        {
            try
            {
                reviewRepository.AddReview(review);
                return Ok(review);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete, Route("DeleteReview")]
        public IHttpActionResult DeleteReview(string reviewId)
        {
            try
            {
                reviewRepository.DeleteReview(reviewId);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut, Route("UpdateReview")]

        public IHttpActionResult UpdateReview([FromBody] Review review)
        {
            try
            {
                reviewRepository.UpdateReview(review);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
