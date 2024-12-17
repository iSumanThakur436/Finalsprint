using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieApplicationMVC.Models
{
    public class Theater

    {
        [Key]
        public string TheaterId { get; set; }
        public string TheaterName { get; set; }
        public string Location { get; set; }
        public int SeatCount { get; set; }
        public string TheaterType { get; set; }
        public string City { get; set; }
        public int TicketPrice { get; set; }
    }
}