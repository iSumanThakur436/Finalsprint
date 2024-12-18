using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApplicationMVC.Models
{
    public class Ticket
    {
       
        public string TicketId { get; set; }

       
        public string BookingId { get; set; }

        
        public string ShowTimeId { get; set; }

        public string SeatNumber { get; set; }

       
    }
}
