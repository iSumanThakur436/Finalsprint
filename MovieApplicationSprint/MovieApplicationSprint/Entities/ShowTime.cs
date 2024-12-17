using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApplicationSprint.Entities
{
    public class ShowTime
    {
        [Key]
        public string ShowTimeId { get; set; }

        [ForeignKey("MovieTheaterNavigation")]
        public string MovieTheaterId { get; set; } // Links to MovieTheater entity

        public DateTime ShowDateTime { get; set; } // Exact date and time of the show

        // Navigation property
        public MovieTheater MovieTheaterNavigation { get; set; }
    }
}
