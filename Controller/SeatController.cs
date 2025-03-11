using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;
using MovieTicketBooking.Models.DTOs;

namespace MovieTicketBooking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository _seatRepo;
        private readonly IMapper _mapper;

        public SeatController(ISeatRepository seatRepo, IMapper mapper)
        {
            _seatRepo = seatRepo;
            _mapper = mapper;
        }

        // Lấy danh sách ghế theo ShowtimeId
        [HttpGet("showtime/{showtimeId}")]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeatsByShowtime(int showtimeId)
        {
            var seats = await _seatRepo.GetSeatByShowtime(showtimeId);
            return Ok(seats);
        }

        // Thêm ghế mới
        [HttpPost]
        public async Task<ActionResult<Seat>> AddSeat([FromBody] Seat seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSeat = await _seatRepo.AddSeat(seat);
            return CreatedAtAction(nameof(GetSeatsByShowtime), new { showtimeId = newSeat.ShowtimeId }, newSeat);
        }

        // Lấy danh sách ghế trống theo suất chiếu
        [HttpGet("available/{showtimeId}")]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetAvailableSeats(int showtimeId)
        {
            var availableSeats = await _seatRepo.GetAvailableSeatsByShowtime(showtimeId);
            var seatDtos = _mapper.Map<IEnumerable<SeatDto>>(availableSeats);

            return Ok(seatDtos);
        }

    }
}