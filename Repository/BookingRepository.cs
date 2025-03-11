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
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            // Kiểm tra suất chiếu và ghế có tồn tại không
            var showtime = await _context.Showtimes.FindAsync(booking.ShowtimeId);
            var seat = await _context.Seats.FindAsync(booking.SeatId);
            var user = await _context.Users.FindAsync(booking.UserId);

            if (showtime == null || seat == null || user == null)
            {
                throw new Exception("Invalid showtime, seat, or user");
            }

            // Kiểm tra ghế đã đặt chưa
            var existingBooking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.SeatId == booking.SeatId && b.ShowtimeId == booking.ShowtimeId);

            if (existingBooking != null)
                throw new Exception("Seat already booked");

            booking.CreatedAt = DateTime.Now;
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllBooking()
        {
            return await _context.Bookings
            .Include(b => b.Showtime)
            .Include(b => b.Seat)
            .Include(b => b.User)
            .ToListAsync();
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _context.Bookings
            .Include(b => b.Showtime)
            .Include(b => b.Seat)
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}