using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApplicationMVC.Models
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; }

        [ForeignKey("UserNavigation")]
        public string UserId { get; set; }

        [ForeignKey("BookingNavigation")]
        public string BookingId { get; set; }

        public string PaymentMethod { get; set; }
        public int Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }

        public User UserNavigation { get; set; }
        public Booking BookingNavigation { get; set; }
    }
}
