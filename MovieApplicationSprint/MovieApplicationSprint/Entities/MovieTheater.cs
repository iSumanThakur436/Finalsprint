using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieApplicationSprint.Entities
{
    public class MovieTheater
    {
        [Key]
        public string MovieTheaterId { get; set; }
        [ForeignKey("MovieNavigation")]
        public string MovieId { get; set; }
        [ForeignKey("TheaterNavigation")]
        public string TheaterId { get; set; }

        public Theater TheaterNavigation { get; set; }
        public Movie MovieNavigation { get; set; }
    }
}