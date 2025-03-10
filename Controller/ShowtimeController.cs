using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;
using MovieTicketBooking.Models.DTOs;

namespace MovieTicketBooking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimeRepository _showTimeRepo;
        private readonly IMapper _mapper;
        public ShowtimeController(IShowtimeRepository showTimeRepo, IMapper mapper)
        {
            _showTimeRepo = showTimeRepo;
            _mapper = mapper;
        }

        // Lấy danh sách showtime
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Showtime>>> getAllShowtime()
        {
            var showTimes = await _showTimeRepo.GetAllShowtimes();
            var showtimeDtos = _mapper.Map<IEnumerable<ShowtimeDto>>(showTimes);
            return Ok(showtimeDtos);
        }

        // Lấy theo id
        [HttpGet("{id}")]
        public async Task<ActionResult<Showtime>> getShowTimeById(int id)
        {
            var showtime = await _showTimeRepo.GetShowtimeById(id);
            if (showtime == null)
            {
                return NotFound(new { message = "Showtime not found" });
            }
            var showtimeDto = _mapper.Map<ShowtimeDto>(showtime);
            return Ok(showtimeDto);
        }

        // Add showtime(Nhận ShowtimeDto, Lưu Showtime)
        [HttpPost]
        public async Task<ActionResult<Showtime>> AddShowTime([FromBody] ShowtimeDto showtimeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var showtime = _mapper.Map<Showtime>(showtimeDto);
            var newShowTime = await _showTimeRepo.AddShowtime(showtime);
            var newShowtimeDto = _mapper.Map<ShowtimeDto>(newShowTime);

            return CreatedAtAction(nameof(getShowTimeById), new { id = newShowtimeDto.Id }, newShowtimeDto);

        }

        // Delete showtime
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowTime(int id)
        {
            var deleted = await _showTimeRepo.DeleteShowtime(id);
            if (!deleted)
            {
                return NotFound(new { message = "Showtime not found" });
            }
            return NoContent();
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShowTime(int id, ShowtimeDto showtimeDto)
        {
            if (id != showtimeDto.Id)
            {
                return BadRequest(new { message = "ShowTime ID missmatch!" });
            }

            var showtime = _mapper.Map<Showtime>(showtimeDto);
            var updated = await _showTimeRepo.UpdateShowtime(showtime);

            if (!updated)
            {
                return NotFound(new { message = "ShowTime not found!" });
            }

            return NoContent();
        }
    }
}