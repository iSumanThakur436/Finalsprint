using System;

namespace MovieApplicationMVC.Models
{
    public class PaymentDetails
    {
        public string PaymentId { get; set; }
        public string BookingId { get; set; }
        public string UserId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string NumberOfSeats { get; set; }
        public string ShowtimeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
    }
}
