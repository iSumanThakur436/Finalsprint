using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repositories;

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

        // Create a ticket after payment
        [HttpPost]
        [Route("CreateAfterPayment")]
        public IHttpActionResult CreateAfterPayment([FromBody] Ticket ticket)
        {
            try
            {
                // Validate Booking existence
                var booking = _bookingRepository.GetBookingById(ticket.BookingId);
                if (booking == null)
                    return BadRequest("Invalid Booking ID. The booking does not exist.");

                // Generate a unique Ticket ID
                ticket.TicketId = Guid.NewGuid().ToString();
                _ticketRepository.AddTicket(ticket);

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get tickets by Booking ID
        [HttpGet]
        [Route("GetByBooking/{bookingId}")]
        public IHttpActionResult GetByBooking(string bookingId)
        {
            try
            {
                var tickets = _ticketRepository.GetTicketsByBooking(bookingId);
                if (!tickets.Any())
                    return NotFound();

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
