using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // API đặt vé
        [HttpPost("book")]
        public async Task<IActionResult> BookTicket([FromBody] Booking booking)
        {
            var showtime = await _context.Showtimes.FindAsync(booking.ShowtimeId);
            if (showtime == null)
            {
                return NotFound("Suất chiếu không tồn tại");
            }
            var seat = await _context.Seats.FindAsync(booking.SeatId);
            if (seat == null || seat.Status == "Booked")
            {
                return BadRequest("Ghế không hợp lệ hoặc đã được đặt");
            }

            // Đánh dấu ghế đã đặt
            seat.Status = "Booked";
            // Lưu booking vào db
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Đặt vé thành công!", booking });
        }

        // API Xem danh sách Booking của User
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUser(int userId)
        {
            var bookings = await _context.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.Showtime)
            .Include(b => b.Seat)
            .ToListAsync();

            return Ok(bookings);
        }
    }
}