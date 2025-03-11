using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetAvailableSeatsByShowtime(int showtimeId);
        Task<Seat> AddSeat(Seat seat);
        Task<IEnumerable<Seat>> GetSeatByShowtime(int showtimeId);
    }
}