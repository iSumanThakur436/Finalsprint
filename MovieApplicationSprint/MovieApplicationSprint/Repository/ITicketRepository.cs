using MovieApplicationSprint.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public interface ITicketRepository
    {
        void AddTicket(Ticket ticket);
        void DeleteTicket(string ticketId);
        void DeleteTicketsByBooking(string bookingId);
        Ticket GetTicketById(string ticketId);
        List<IGrouping<string, Ticket>> GetAllTicketsGroupedByShowId();
        List<Ticket> GetTicketsByBooking(string bookingId); // Method to fetch tickets by Booking ID
        List<Ticket> GetTicketsByMovieName(string title); // Method to fetch tickets by Movie Name
    }
}
