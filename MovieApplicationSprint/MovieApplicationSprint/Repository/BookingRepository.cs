using MovieApplicationSprint.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly MovieAppContext _context;

        public BookingRepository()
        {
            _context = new MovieAppContext();
        }

        public void CreateBooking(Booking booking)
        {
            try
            {
                // Ensure the associated user exists
                var user = _context.Users.Find(booking.UserId);
                if (user == null)
                {
                    throw new Exception("Invalid User ID. The user does not exist.");
                }

                // Ensure the associated showtime exists
                var showTime = _context.ShowTimes.Find(booking.ShowTimeId);
                if (showTime == null)
                {
                    throw new Exception("Invalid ShowTime ID. The showtime does not exist.");
                }

                _context.Bookings.Add(booking);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating booking: {ex.Message}");
            }
        }

        public List<Booking> GetAllBookings()
        {
            try
            {
                return _context.Bookings.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching all bookings: {ex.Message}");
            }
        }

        public Booking GetBookingById(string bookingId)
        {
            try
            {
                return _context.Bookings
                    .Include("UserNavigation")
                    .Include("ShowTimeNavigation")
                    .FirstOrDefault(b => b.BookingId == bookingId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching booking by ID: {ex.Message}");
            }
        }

        public List<Booking> GetBookingsByUser(string userId)
        {
            try
            {
                return _context.Bookings
                    .Include("UserNavigation")
                    .Include("ShowTimeNavigation")
                    .Where(b => b.UserId == userId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching bookings for user: {ex.Message}");
            }
        }

        public void UpdateBookingStatus(string bookingId, string status)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                booking.Status = status;
                _context.SaveChanges();
            }
        }


        public void CancelBooking(string bookingId)
        {
            try
            {
                var booking = _context.Bookings.Find(bookingId);
                if (booking != null)
                {
                    _context.Bookings.Remove(booking);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Booking not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error canceling booking: {ex.Message}");
            }
        }
    }
}
