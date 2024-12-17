using MovieApplicationSprint.Entities;
using System.Collections.Generic;

namespace MovieApplicationSprint.Repositories
{
    public interface IBookingRepository
    {
        void CreateBooking(Booking booking); // Create a new booking
        List<Booking> GetAllBookings(); // Retrieve all bookings (Admin only)
        void UpdateBookingStatus(string bookingId, string status);
        Booking GetBookingById(string bookingId); // Retrieve a specific booking by ID
        List<Booking> GetBookingsByUser(string userId); // Fetch bookings for a user
        void CancelBooking(string bookingId); // Cancel an existing booking
    }
}
