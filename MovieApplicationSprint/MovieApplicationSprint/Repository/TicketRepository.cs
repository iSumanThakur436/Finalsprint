using MovieApplicationSprint.Entities;
using System;
using System.Collections.Generic;
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
                // Validate the ShowTime
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

                // Generate ticket ID (e.g., tk01, tk02)
                int nextTicketNumber = _context.Tickets.Count() + 1;
                ticket.TicketId = $"tk{nextTicketNumber:00}";

                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        public Ticket GetTicketById(string ticketId)
        {
            try
            {
                return _context.Tickets.Find(ticketId);
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
