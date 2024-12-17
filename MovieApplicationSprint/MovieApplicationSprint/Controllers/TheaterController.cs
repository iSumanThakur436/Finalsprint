using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repositories;
using System;
using System.Web.Http;

namespace MovieApplicationSprint.Controllers
{
    [RoutePrefix("api/Theater")]
    public class TheaterController : ApiController
    {
        private readonly ITheaterRepository _repository;

        public TheaterController()
        {
            _repository = new TheaterRepository();
        }

        [HttpPost, Route("AddTheater")]
        public IHttpActionResult AddTheater([FromBody] Theater theater)
        {
            try
            {
                _repository.AddTheater(theater);
                return Ok("Theater added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("UpdateTheater")]
        public IHttpActionResult UpdateTheater([FromBody] Theater theater)
        {
            try
            {
                _repository.UpdateTheater(theater);
                return Ok("Theater updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("DeleteTheater/{id}")]
        public IHttpActionResult DeleteTheater(string id)
        {
            try
            {
                _repository.DeleteTheater(id);
                return Ok("Theater deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetTheaterById/{id}")]
        public IHttpActionResult GetTheaterById(string id)
        {
            try
            {
                var theater = _repository.GetTheaterById(id);
                return Ok(theater);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAllTheaters")]
        public IHttpActionResult GetAllTheaters()
        {
            try
            {
                var theaters = _repository.GetAllTheaters();
                return Ok(theaters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetTheatersByLocation/{location}")]
        public IHttpActionResult GetTheatersByLocation(string location)
        {
            try
            {
                var theaters = _repository.GetTheatersByLocation(location);
                return Ok(theaters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
