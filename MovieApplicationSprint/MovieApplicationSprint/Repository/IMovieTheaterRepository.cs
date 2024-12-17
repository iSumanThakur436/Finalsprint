using MovieApplicationSprint.Entities;
using MovieApplicationSprint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebApplication.Repository
{
    internal interface IMovieTheaterRepository
    {
        List<MovieTheater> GetAllMovieTheater();
        List<MovieTheaterDetails> GetTheaterByMovieId(string Movieid,string city);
    }
}
