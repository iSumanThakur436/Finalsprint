using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieApplicationSprint.Entities
{
    public class Review
    {
        [Key]
        public string ReviewId { get; set; }
        [ForeignKey("UserNavigation")]
        public string UserId { get; set; }
        [ForeignKey("MovieNavigation")]
        public string MovieId { get; set; }
        public string Ratings { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public User UserNavigation { get; set; }
        public Movie MovieNavigation { get; set; }
    }
}