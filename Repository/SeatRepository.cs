using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _context;
        public SeatRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Seat>> GetAvailableSeatsByShowtime(int showtimeId)
        {
            return await _context.Seats
            .Where(s => s.ShowtimeId == showtimeId && s.Status == "Available")
            .ToListAsync();
        }
    }
}