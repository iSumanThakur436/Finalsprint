using MovieApplicationSprint.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplicationSprint.Repositories
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly MovieAppContext _context;

        public TheaterRepository()
        {
            _context = new MovieAppContext();
        }

        public void AddTheater(Theater theater)
        {
            _context.Theaters.Add(theater);
            _context.SaveChanges();
        }

        public void UpdateTheater(Theater theater)
        {
            var existingTheater = _context.Theaters.Find(theater.TheaterId);
            if (existingTheater != null)
            {
                existingTheater.TheaterName = theater.TheaterName;
                existingTheater.Location = theater.Location;
                existingTheater.SeatCount = theater.SeatCount;
                existingTheater.TheaterType = theater.TheaterType;
                existingTheater.City = theater.City;
                _context.SaveChanges();
            }
        }

        public void DeleteTheater(string theaterId)
        {
            var theater = _context.Theaters.Find(theaterId);
            if (theater != null)
            {
                _context.Theaters.Remove(theater);
                _context.SaveChanges();
            }
        }

        public Theater GetTheaterById(string theaterId)
        {
            return _context.Theaters.Find(theaterId);
        }

        public List<Theater> GetAllTheaters()
        {
            return _context.Theaters.ToList();
        }

        public List<Theater> GetTheatersByLocation(string location)
        {
            return _context.Theaters.Where(t => t.Location == location).ToList();
        }
    }
}
