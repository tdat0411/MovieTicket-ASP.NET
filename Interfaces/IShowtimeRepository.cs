using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface IShowtimeRepository
    {
        Task<IEnumerable<Showtime>> GetAllShowtimes();
        Task<Showtime> GetShowtimeById(int id);
        Task<bool> UpdateShowtime(Showtime showtime);
        Task<Showtime> AddShowtime(Showtime showtime);
        Task<bool> DeleteShowtime(int id);
    }
}