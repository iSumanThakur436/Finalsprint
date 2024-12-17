using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApplicationSprint.Entities
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; } // Unique Payment ID

        [ForeignKey("UserNavigation")]
        public string UserId { get; set; } // User ID initiating the payment

        [ForeignKey("BookingNavigation")]
        public string BookingId { get; set; } // Associated Booking ID

        public string PaymentMethod { get; set; } // e.g., "Credit Card", "Razorpay"

        public int Amount { get; set; } // Payment amount

        public string PaymentStatus { get; set; } // "Success" or "Failed"

        public DateTime PaymentDate { get; set; } // Timestamp of payment

        public User UserNavigation { get; set; }

        public Booking BookingNavigation { get; set; }
    }
}
