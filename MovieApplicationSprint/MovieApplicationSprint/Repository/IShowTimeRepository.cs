using MovieApplicationSprint.Entities;
using System;
using System.Collections.Generic;

namespace MovieApplicationSprint.Repositories
{
    public interface IShowTimeRepository
    {
        void CreateShowTime(ShowTime showTime); // Add a new showtime
        List<ShowTime> GetAllShowTimes(); // Get all showtimes (Admin only)
        List<ShowTime> GetShowTimesByMovieTheater(string movieTheaterId); // Get showtimes for a specific movie-theater
        List<ShowTime> GetShowTimesByDate(DateTime date); // Get all showtimes for a specific date
        ShowTime GetShowTimeById(string showTimeId); // Get a specific showtime by ID
        void UpdateShowTime(ShowTime showTime); // Update an existing showtime
        void DeleteShowTime(string showTimeId); // Delete a specific showtime
    }
}
