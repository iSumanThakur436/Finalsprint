using System;

namespace MovieApplicationMVC.Models
{
    public class Movie
    {
        public string MovieId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Actor { get; set; }
        public string Poster { get; set; }
        public string Trailer { get; set; }
        public string Director { get; set; }
    }
}
