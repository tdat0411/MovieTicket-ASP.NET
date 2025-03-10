using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repository
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly AppDbContext _context;
        public ShowtimeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Showtime> AddShowtime(Showtime showtime)
        {
            _context.Showtimes.Add(showtime);
            await _context.SaveChangesAsync();
            return showtime;
        }

        public async Task<bool> DeleteShowtime(int id)
        {
            var showtime = await _context.Showtimes.FindAsync(id);
            if (showtime == null)
            {
                return false;
            }

            _context.Showtimes.Remove(showtime);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Showtime>> GetAllShowtimes()
        {
            return await _context.Showtimes
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .ToListAsync();
        }

        public async Task<Showtime> GetShowtimeById(int id)
        {
            return await _context.Showtimes
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateShowtime(Showtime showtime)
        {
            var existingShowtime = await _context.Showtimes.FindAsync(showtime.Id);
            if (existingShowtime == null)
            {
                return false;
            }

            existingShowtime.MovieId = showtime.MovieId;
            existingShowtime.TheaterId = showtime.TheaterId;
            existingShowtime.StartTime = showtime.StartTime;
            existingShowtime.Price = showtime.Price;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}