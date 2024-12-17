using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MovieApplicationSprint.Controllers
{
    [RoutePrefix("api/tickets")]
    public class TicketController : ApiController
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IBookingRepository _bookingRepository;
        public TicketController()
        {
            _ticketRepository = new TicketRepository();
            _bookingRepository = new BookingRepository();
        }

        public TicketController(ITicketRepository ticketRepository, IBookingRepository bookingRepository)
        {
            _ticketRepository = ticketRepository;
            _bookingRepository = bookingRepository;
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create([FromBody] Ticket ticket)
        {
            try
            {
                // Validate Booking existence
                var booking = _bookingRepository.GetBookingById(ticket.BookingId);
                if (booking == null)
                {
                    return BadRequest("Invalid Booking ID. The booking does not exist.");
                }

                // Add ticket with validation
                _ticketRepository.AddTicket(ticket);
                return Ok("Ticket created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IHttpActionResult GetById(string id)
        {
            try
            {
                var ticket = _ticketRepository.GetTicketById(id);
                if (ticket == null)
                {
                    return NotFound();
                }
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByMovieName/{movieName}")]
        public IHttpActionResult GetByMovieName(string movieName)
        {
            try
            {
                var tickets = _ticketRepository.GetTicketsByMovieName(movieName);
                if (!tickets.Any())
                {
                    return NotFound();
                }
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                _ticketRepository.DeleteTicket(id);
                return Ok("Ticket deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteByBooking/{bookingId}")]
        public IHttpActionResult DeleteByBooking(string bookingId)
        {
            try
            {
                _ticketRepository.DeleteTicketsByBooking(bookingId);
                return Ok("All tickets associated with the booking have been deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllGroupedByShowId")]
        [Authorize(Roles = "Admin")] // Restrict to Admins only
        public IHttpActionResult GetAllGroupedByShowId()
        {
            try
            {
                var tickets = _ticketRepository.GetAllTicketsGroupedByShowId();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
