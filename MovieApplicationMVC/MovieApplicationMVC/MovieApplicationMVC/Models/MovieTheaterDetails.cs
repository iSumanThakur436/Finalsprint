using System;

namespace MovieApplicationMVC.Models
{
    public class MovieTheaterDetails
    {


        public string UserId { get; set; }

        public string MovieId { get; set; }

        public string TheaterId { get; set; }

        public string ShowTimeId { get; set; }

        public string Title { get; set; }

        public string TheaterName { get; set; }

        public DateTime ShowDateTime { get; set; }

        public int TicketPrice { get; set; }

    }
}
