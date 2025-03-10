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
    public class TheaterRepository : ITheaterRepository
    {
        private readonly AppDbContext _context;
        public TheaterRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Theater> AddTheater(Theater theater)
        {
            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();
            return theater;
        }

        public async Task<bool> DeleteTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null)
            {
                return false;
            }

            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Theater>> GetAllTheaters()
        {
            return await _context.Theaters.ToListAsync();
        }

        public async Task<Theater> GetTheaterById(int id)
        {
            return await _context.Theaters.FindAsync(id);
        }
    }
}