using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repositories;
using System;
using System.Web.Http;

namespace MovieApplicationSprint.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserRepository _repository;

        public UserController()
        {
            _repository = new UserRepository();
        }

        [HttpPost, Route("Register")]
        public IHttpActionResult Register([FromBody] User user)
        {
            try
            {
                _repository.AddUser(user);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("Login")]
        public IHttpActionResult Login([FromBody] User loginRequest)
        {
            try
            {
                var user = _repository.ValidateUser(loginRequest.Email, loginRequest.Password);
                if (user != null)
                {
                    return Ok(new { Message = "Login successful", User = user });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("UpdateProfile")]
        public IHttpActionResult UpdateProfile([FromBody] User user)
        {
            try
            {
                _repository.UpdateUser(user);
                return Ok("User profile updated successfully");
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
                var user = _repository.GetUserById(id);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var users = _repository.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
