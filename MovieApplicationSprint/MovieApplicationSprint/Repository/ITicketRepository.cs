using MovieApplicationSprint.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public interface ITicketRepository
    {
        void AddTicket(Ticket ticket);
        void DeleteTicket(string ticketId);
        void DeleteTicketsByBooking(string bookingid);
        Ticket GetTicketById(string ticketId);
        List<IGrouping<string, Ticket>> GetAllTicketsGroupedByShowId();
        List<Ticket> GetTicketsByMovieName(string title); // New method to fetch tickets by Booking ID
    }
}
