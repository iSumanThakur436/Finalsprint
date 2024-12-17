using MovieApplicationSprint.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private MovieAppContext context;

        public MovieRepository()
        {
            context = new MovieAppContext();
        }

        public void AddMovie(Movie movie)
        {
            try
            {
                context.Movies.Add(movie);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateMovie(Movie movie)
        {
            try
            {
                var existingMovie = context.Movies.Find(movie.MovieId);
                if (existingMovie != null)
                {
                    existingMovie.Title = movie.Title;
                    existingMovie.Actor = movie.Actor;
                    existingMovie.Director = movie.Director;
                    existingMovie.ReleaseDate = movie.ReleaseDate;
                    existingMovie.Genre = movie.Genre;
                    existingMovie.Language = movie.Language;
                    existingMovie.Poster = movie.Poster;
                    existingMovie.Synopsis = movie.Synopsis;
                    existingMovie.Trailer = movie.Trailer;

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteMovie(string movieId)
        {
            try
            {
                var movie = context.Movies.Find(movieId);
                if (movie != null)
                {
                    context.Movies.Remove(movie);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Movie GetMovieById(string movieId)
        {
            try
            {
                return context.Movies.Find(movieId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Movie> GetMoviesByName(string name)
        {
            try
            {
                return context.Movies
                    .Where(m => m.Title.ToLower().Contains(name.ToLower()))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Movie> GetMoviesByGenre(string genre)
        {
            try
            {
                return context.Movies
                    .Where(m => m.Genre.ToLower() == genre.ToLower())
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Movie> GetMoviesByActor(string actor)
        {
            try
            {
                return context.Movies
                    .Where(m => m.Actor.ToLower().Contains(actor.ToLower()))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Movie> GetMoviesByDirector(string director)
        {
            try
            {
                return context.Movies
                    .Where(m => m.Director.ToLower().Contains(director.ToLower()))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Movie> GetAllMovies()
        {
            try
            {
                return context.Movies.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Movie> GetMoviesWithPoster()
        {
            try
            {
                return context.Movies
                    .Where(m => !string.IsNullOrEmpty(m.Poster))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Movie> GetMoviesWithTrailer()
        {
            try
            {
                return context.Movies
                    .Where(m => !string.IsNullOrEmpty(m.Trailer))
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
