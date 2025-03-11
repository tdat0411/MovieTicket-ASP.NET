using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookRepo;

        public BookingController(IBookingRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        // API đặt vé
        [HttpPost("book")]
        public async Task<IActionResult> BookTicket([FromBody] Booking booking)
        {
            try
            {
                var newBooking = await _bookRepo.CreateBooking(booking);
                return Ok(new { message = "Booking successful", booking = newBooking });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // API LẤY DANH SÁCH VÉ ĐÃ ĐẶT
        [HttpGet("all")]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookRepo.GetAllBooking();
            return Ok(bookings);
        }

        // API LẤY THÔNG TIN VÉ THEO ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookRepo.GetBookingById(id);
            if (booking == null)
                return NotFound("Booking not found");

            return Ok(booking);
        }
    }
}