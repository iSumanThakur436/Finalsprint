using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace MovieApplicationSprint.Entities
{
    public class MovieAppContext:DbContext
    {
        public MovieAppContext():base("MovieConnection")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}