using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface ITheaterRepository
    {
        Task<IEnumerable<Theater>> GetAllTheaters();
        Task<Theater> GetTheaterById(int id);
        Task<Theater> AddTheater(Theater theater);
        Task<bool> DeleteTheater(int id);
    }
}