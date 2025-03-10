using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Interfaces;
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