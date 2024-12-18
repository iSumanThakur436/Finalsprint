using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApplicationMVC.Models
{
    public class Ticket
    {
        [Key]
        public string TicketId { get; set; }

        [ForeignKey("BookingNavigation")]
        public string BookingId { get; set; }

        [ForeignKey("ShowTimeNavigation")]
        public string ShowTimeId { get; set; }

        public string SeatNumber { get; set; }

        public Booking BookingNavigation { get; set; }
        public ShowTime ShowTimeNavigation { get; set; }
    }
}
