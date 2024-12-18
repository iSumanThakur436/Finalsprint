using MovieApplicationSprint.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly MovieAppContext _context;

        public TicketRepository()
        {
            _context = new MovieAppContext();
        }

        public void AddTicket(Ticket ticket)
        {
            try
            {
                // Validate the ShowTime existence
                var showtime = _context.ShowTimes
                                       .Include("MovieTheaterNavigation.TheaterNavigation")
                                       .FirstOrDefault(st => st.ShowTimeId == ticket.ShowTimeId);

                if (showtime == null)
                {
                    throw new Exception("Invalid ShowTime ID. The showtime does not exist.");
                }

                // Validate the MovieTheater's seat capacity
                var theater = showtime.MovieTheaterNavigation.TheaterNavigation;
                if (theater == null)
                {
                    throw new Exception("Invalid Theater. The associated theater does not exist.");
                }

                // Check if the seat is already booked for the same ShowTime
                var bookedSeats = _context.Tickets
                                          .Where(t => t.ShowTimeId == ticket.ShowTimeId)
                                          .Select(t => t.SeatNumber)
                                          .ToList();

                if (bookedSeats.Contains(ticket.SeatNumber))
                {
                    throw new Exception("The seat number is already booked for this ShowTime.");
                }

                if (bookedSeats.Count >= theater.SeatCount)
                {
                    throw new Exception("No more seats are available for this ShowTime.");
                }

                // Generate ticket ID dynamically (e.g., tk01, tk02, etc.)
                int nextTicketNumber = _context.Tickets.Count() + 1;
                ticket.TicketId = $"tk{nextTicketNumber:00}";

                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add ticket: {ex.Message}");
            }
        }

        public void DeleteTicket(string ticketId)
        {
            try
            {
                var ticket = _context.Tickets.Find(ticketId);
                if (ticket != null)
                {
                    _context.Tickets.Remove(ticket);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Ticket not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete ticket: {ex.Message}");
            }
        }

        public void DeleteTicketsByBooking(string bookingId)
        {
            try
            {
                var tickets = _context.Tickets.Where(t => t.BookingId == bookingId).ToList();
                if (tickets.Any())
                {
                    _context.Tickets.RemoveRange(tickets);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete tickets for booking {bookingId}: {ex.Message}");
            }
        }

        public Ticket GetTicketById(string ticketId)
        {
            try
            {
                return _context.Tickets
                               .Include("BookingNavigation")
                               .Include("ShowTimeNavigation")
                               .FirstOrDefault(t => t.TicketId == ticketId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch ticket by ID: {ex.Message}");
            }
        }

        public List<Ticket> GetTicketsByBooking(string bookingId)
        {
            try
            {
                return _context.Tickets
                               .Include("ShowTimeNavigation.MovieTheaterNavigation.MovieNavigation")
                               .Where(t => t.BookingId == bookingId)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch tickets by Booking ID: {ex.Message}");
            }
        }

        public List<Ticket> GetTicketsByMovieName(string movieName)
        {
            try
            {
                return _context.Tickets
                               .Include("ShowTimeNavigation.MovieTheaterNavigation.MovieNavigation")
                               .Where(t => t.ShowTimeNavigation.MovieTheaterNavigation.MovieNavigation.Title == movieName)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch tickets by Movie Name: {ex.Message}");
            }
        }

        public List<IGrouping<string, Ticket>> GetAllTicketsGroupedByShowId()
        {
            try
            {
                return _context.Tickets
                               .GroupBy(t => t.ShowTimeId)
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch grouped tickets by ShowTime ID: {ex.Message}");
            }
        }
    }
}
