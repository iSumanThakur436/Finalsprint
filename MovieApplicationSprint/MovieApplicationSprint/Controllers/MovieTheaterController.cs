using MovieWebApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MovieWebApplication.Controllers
{
    public class MovieTheaterController : ApiController
    {
        private MovieTheaterRepository repository;
        public MovieTheaterController()
        {
            repository = new MovieTheaterRepository();
        }

        [HttpGet, Route("GetAllMovieTheater")]

        public IHttpActionResult GetAllMovieTheater()
        {
            try
            {
                var movietheater = repository.GetAllMovieTheater();
                return Ok(movietheater);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("GetTheaterByMovieId/{movieid}/{city}")]
        public IHttpActionResult GetTheaterByMovieId(string movieid,string city)
        {
            try
            {
                var movietheater = repository.GetTheaterByMovieId(movieid,city);
                return Ok(movietheater);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
