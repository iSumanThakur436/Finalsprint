using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repositories;
using System;
using System.Web.Http;

namespace MovieApplicationSprint.Controllers
{
    [RoutePrefix("api/ShowTime")]
    public class ShowTimeController : ApiController
    {
        private readonly IShowTimeRepository _repository;

        public ShowTimeController()
        {
            _repository = new ShowTimeRepository();
        }

        [HttpPost, Route("Create")]
        public IHttpActionResult Create([FromBody] ShowTime showTime)
        {
            try
            {
                _repository.CreateShowTime(showTime);
                return Ok("ShowTime created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var showTimes = _repository.GetAllShowTimes();
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetByDate/{date}")]
        public IHttpActionResult GetByDate(DateTime date)
        {
            try
            {
                var showTimes = _repository.GetShowTimesByDate(date);
                return Ok(showTimes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetById/{id}")]
        public IHttpActionResult GetById(string id)
        {
            try
            {
                var showTime = _repository.GetShowTimeById(id);
                if (showTime == null)
                {
                    return NotFound();
                }
                return Ok(showTime);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("Update")]
        public IHttpActionResult Update([FromBody] ShowTime showTime)
        {
            try
            {
                _repository.UpdateShowTime(showTime);
                return Ok("ShowTime updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("Delete/{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                _repository.DeleteShowTime(id);
                return Ok("ShowTime deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
