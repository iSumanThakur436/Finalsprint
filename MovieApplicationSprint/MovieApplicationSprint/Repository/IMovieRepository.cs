using MovieApplicationSprint.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApplicationSprint.Repository
{
    internal interface IMovieRepository
    {
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(string movieId);
        Movie GetMovieById(string movieId);

        List<Movie> GetMoviesByName(string name);
        List<Movie> GetMoviesByGenre(string genre);
        List<Movie> GetMoviesByActor(string actor);
        List<Movie> GetMoviesByDirector(string director);
        List<Movie> GetAllMovies();
        List<Movie> GetMoviesWithPoster();
        List<Movie> GetMoviesWithTrailer();
    }
}
