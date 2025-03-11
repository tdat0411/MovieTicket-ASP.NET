using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBooking(Booking booking);
        Task<IEnumerable<Booking>> GetAllBooking();
        Task<Booking> GetBookingById(int id);
    }
}