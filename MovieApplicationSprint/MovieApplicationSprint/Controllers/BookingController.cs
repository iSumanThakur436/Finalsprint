using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Repositories;
using System.Web.Http;
using System;

[RoutePrefix("Booking")]
public class BookingController : ApiController
{
    private readonly IBookingRepository _repository;

    public BookingController()
    {
        _repository = new BookingRepository();
    }

    [HttpPost, Route("Create")]
    public IHttpActionResult Create([FromBody] Booking booking)
    {
        if (booking == null)
            return BadRequest("Invalid data.");

        try
        {
            _repository.CreateBooking(booking);
            return Ok("Booking created successfully.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }



    [HttpGet, Route("GetAll")]
    public IHttpActionResult GetAll()
    {
        try
        {
            var bookings = _repository.GetAllBookings();
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpGet, Route("GetById/{id}")]
    public IHttpActionResult GetById(string id)
    {
        try
        {
            var booking = _repository.GetBookingById(id);
            if (booking == null) return NotFound();

            var result = new
            {
                booking.BookingId,
                booking.BookingDate,
                booking.ShowTimeId,
                booking.UserId,
                booking.TotalPrice,
                booking.Status,
                User = new
                {
                    booking.UserNavigation.UserId,
                    booking.UserNavigation.UserName,
                    booking.UserNavigation.Email,
                    booking.UserNavigation.Mobile
                }
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpPut, Route("UpdateBookingStatus/{id}")]
    public IHttpActionResult UpdateBookingStatus(string id, [FromBody] Booking booking)
    {
        if (booking == null) return BadRequest("Invalid data.");
        try
        {
            _repository.UpdateBookingStatus(id, booking.Status);
            return Ok("Booking status updated.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    [HttpDelete, Route("Cancel/{id}")]
    public IHttpActionResult Cancel(string id)
    {
        try
        {
            _repository.CancelBooking(id);
            return Ok("Booking cancelled successfully.");
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}
