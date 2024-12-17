using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Model;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWebApplication.Repository
{
    public class MovieTheaterRepository : IMovieTheaterRepository
    {
        private MovieAppContext _appContext;

        public MovieTheaterRepository()
        {
            _appContext = new MovieAppContext();
        }

        public List<MovieTheater> GetAllMovieTheater()
        {
            return _appContext.MovieTheaters.ToList();
        }

        public List<MovieTheaterDetails> GetTheaterByMovieId(string movieid, string city)
        {
            var movietheaters = (from movie in _appContext.Movies
                                 join movietheater in _appContext.MovieTheaters
                                 on movie.MovieId equals movietheater.MovieId
                                 join theater in _appContext.Theaters
                                 on movietheater.TheaterId equals theater.TheaterId
                                 join show in _appContext.ShowTimes
                                 on movietheater.MovieTheaterId equals show.MovieTheaterId
                                 where movietheater.MovieId == movieid && theater.City == city
                                 select new MovieTheaterDetails()
                                 {
                                     MovieId = movieid,
                                     TheaterId = theater.TheaterId,
                                     ShowTimeId = show.ShowTimeId,
                                     Title = movie.Title,
                                     TheaterName = theater.TheaterName,
                                     TicketPrice = theater.TicketPrice,
                                     ShowDateTime = show.ShowDateTime,

                                 }).ToList();
            return movietheaters;
        
            
        }
    }
}