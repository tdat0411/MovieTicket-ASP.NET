using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private readonly ITheaterRepository _theaterRepo;
        public TheaterController(ITheaterRepository theaterRepo)
        {
            _theaterRepo = theaterRepo;
        }

        // Lấy danh sách rạp chiếu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theater>>> GetAllTheaters()
        {
            var theaters = await _theaterRepo.GetAllTheaters();
            return Ok(theaters);
        }

        // Lấy danh sách rạp chiếu theo id
        [HttpGet("{id}")]
        public async Task<ActionResult<Theater>> GetTheaterById(int id)
        {
            var theater = await _theaterRepo.GetTheaterById(id);
            if (theater == null)
            {
                return NotFound(new { message = "Theater not found" });
            }
            return Ok(theater);
        }

        // Thêm theater
        [HttpPost]
        public async Task<ActionResult<Theater>> AddTheater([FromBody] Theater theater)
        {
            var newTheater = await _theaterRepo.AddTheater(theater);
            return CreatedAtAction(nameof(GetTheaterById), new { id = newTheater.Id }, newTheater);
        }

        // Xóa theater
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheater(int id)
        {
            var deleted = await _theaterRepo.DeleteTheater(id);
            if (!deleted)
            {
                return NotFound(new { message = "Theater not found" });
            }
            return NoContent();
        }
    }
}