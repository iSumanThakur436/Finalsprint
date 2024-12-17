using MovieApplicationSprint.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public class ShowTimeRepository : IShowTimeRepository
    {
        private readonly MovieAppContext _context;

        public ShowTimeRepository()
        {
            _context = new MovieAppContext();
        }

        public void CreateShowTime(ShowTime showTime)
        {
            try
            {
                var movieTheater = _context.MovieTheaters.Find(showTime.MovieTheaterId);
                if (movieTheater == null)
                {
                    throw new Exception("Invalid MovieTheaterId. The movie-theater combination does not exist.");
                }

                _context.ShowTimes.Add(showTime);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating showtime: {ex.Message}");
            }
        }

        public List<ShowTime> GetAllShowTimes()
        {
            try
            {
                return _context.ShowTimes
                    .Include("MovieTheaterNavigation.MovieNavigation")
                    .Include("MovieTheaterNavigation.TheaterNavigation")
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching all showtimes: {ex.Message}");
            }
        }

        public List<ShowTime> GetShowTimesByMovieTheater(string movieTheaterId)
        {
            try
            {
                return _context.ShowTimes
                    .Include("MovieTheaterNavigation.MovieNavigation")
                    .Include("MovieTheaterNavigation.TheaterNavigation")
                    .Where(st => st.MovieTheaterId == movieTheaterId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching showtimes for MovieTheaterId {movieTheaterId}: {ex.Message}");
            }
        }

        public List<ShowTime> GetShowTimesByDate(DateTime date)
        {
            try
            {
                return _context.ShowTimes
                    .Include("MovieTheaterNavigation.MovieNavigation")
                    .Include("MovieTheaterNavigation.TheaterNavigation")
                    .Where(st => st.ShowDateTime.Date == date.Date)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching showtimes for date {date}: {ex.Message}");
            }
        }

        public ShowTime GetShowTimeById(string showTimeId)
        {
            try
            {
                return _context.ShowTimes
                    .Include("MovieTheaterNavigation.MovieNavigation")
                    .Include("MovieTheaterNavigation.TheaterNavigation")
                    .FirstOrDefault(st => st.ShowTimeId == showTimeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching showtime by ID: {ex.Message}");
            }
        }

        public void UpdateShowTime(ShowTime showTime)
        {
            try
            {
                var existingShowTime = _context.ShowTimes.Find(showTime.ShowTimeId);
                if (existingShowTime == null)
                {
                    throw new Exception("ShowTime not found.");
                }

                existingShowTime.ShowDateTime = showTime.ShowDateTime;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating showtime: {ex.Message}");
            }
        }

        public void DeleteShowTime(string showTimeId)
        {
            try
            {
                var showTime = _context.ShowTimes.Find(showTimeId);
                if (showTime == null)
                {
                    throw new Exception("ShowTime not found.");
                }

                _context.ShowTimes.Remove(showTime);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting showtime: {ex.Message}");
            }
        }
    }
}
