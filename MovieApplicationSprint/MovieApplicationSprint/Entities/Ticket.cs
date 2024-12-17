using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApplicationSprint.Entities
{
    public class Ticket
    {
        [Key]
        public string TicketId { get; set; } // e.g., "tk01"

        [ForeignKey("BookingNavigation")]
        public string BookingId { get; set; }

        [ForeignKey("ShowTimeNavigation")]
        public string ShowTimeId { get; set; }

        public string SeatNumber { get; set; } // User-selected seat number

        public Booking BookingNavigation { get; set; }
        public ShowTime ShowTimeNavigation { get; set; }
    }
}
