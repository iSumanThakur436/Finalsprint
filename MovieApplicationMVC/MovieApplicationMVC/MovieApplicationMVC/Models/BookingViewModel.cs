using System;

namespace MovieApplicationMVC.Models
{
    public class BookingViewModel
    {
        public string BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
