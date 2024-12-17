using MovieApplicationSprint.Entities;
using System.Collections.Generic;

namespace MovieApplicationSprint.Repositories
{
    public interface ITheaterRepository
    {
        void AddTheater(Theater theater);
        void UpdateTheater(Theater theater);
        void DeleteTheater(string theaterId);
        Theater GetTheaterById(string theaterId);
        List<Theater> GetAllTheaters();
        List<Theater> GetTheatersByLocation(string location); // New for filtering by location
    }
}
