using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MovieApplicationSprint.Entities;

namespace MovieApplicationSprint.Model
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