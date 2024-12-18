using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repository;
using System;
using System.Web.Http;

namespace MovieApplicationSprint.Controllers
{
    public class MovieController : ApiController
    {
        private IMovieRepository repository;

        public MovieController()
        {
            repository = new MovieRepository();
        }

        [HttpPost, Route("AddMovie")]
        public IHttpActionResult Add([FromBody] Movie movie)
        {
            try
            {
                repository.AddMovie(movie);
                return Ok("Movie added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("UpdateMovie")]
        public IHttpActionResult Update([FromBody] Movie movie)
        {
            try
            {
                repository.UpdateMovie(movie);
                return Ok("Movie updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("DeleteMovie/{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                repository.DeleteMovie(id);
                return Ok("Movie deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetMovieById/{id}")]
        public IHttpActionResult GetMovieById(string id)
        {
            try
            {
                var movie = repository.GetMovieById(id);
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetMoviesByGenre/{genre}")]
        public IHttpActionResult GetByGenre(string genre)
        {
            try
            {
                var movies = repository.GetMoviesByGenre(genre);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAllMovies")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var movies = repository.GetAllMovies();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetMoviesByName/{name}")]
        public IHttpActionResult GetMoviesByName(string name)
        {
            try
            {
                var movies = repository.GetMoviesByName(name);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetMoviesByActor/{actor}")]
        public IHttpActionResult GetMoviesByActor(string actor)
        {
            try
            {
                var movies = repository.GetMoviesByActor(actor);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetMoviesByDirector/{director}")]
        public IHttpActionResult GetMoviesByDirector(string director)
        {
            try
            {
                var movies = repository.GetMoviesByDirector(director);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetMoviesWithPoster")]
        public IHttpActionResult GetMoviesWithPoster()
        {
            try
            {
                var movies = repository.GetMoviesWithPoster();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetMoviesWithTrailer")]
        public IHttpActionResult GetMoviesWithTrailer()
        {
            try
            {
                var movies = repository.GetMoviesWithTrailer();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("SearchMovies/{keyword}")]
        public IHttpActionResult SearchMovies(string keyword)
        {
            try
            {
                var movies = repository.SearchMovies(keyword);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
