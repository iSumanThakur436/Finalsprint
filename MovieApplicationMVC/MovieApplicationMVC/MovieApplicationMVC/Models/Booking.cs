using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieApplicationMVC.Models
{
    public class Booking
    {
        [Key]
        public string BookingId { get; set; }

        [ForeignKey("UserNavigation")]
        public string UserId { get; set; }// Foreign Key to User table
        [ForeignKey("MovieNavigation")]
        public string MovieId { get; set; }

        [ForeignKey("TheaterNavigation")]

        public string TheaterId { get; set; }
        [ForeignKey("ShowTimeNavigation")]
        public string ShowTimeId { get; set; } // Foreign Key to ShowTime table

        public DateTime BookingDate { get; set; } // Date of booking
        public int NumberOfSeats { get; set; } // Total cost of booking

        public int TotalPrice { get; set; } // Total cost of booking

        public string Status { get; set; } // Example: confirmed, canceled, etc.

        // Navigation properties
        public User UserNavigation { get; set; }
        public ShowTime ShowTimeNavigation { get; set; }
        public Movie MovieNavigation { get; set; }

        public Theater TheaterNavigation { get; set; }
    }
}