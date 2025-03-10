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
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepo;
        public MovieController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        // Lấy danh sách phim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var movies = await _movieRepo.GetAllMovies();
            return Ok(movies);
        }

        // Lấy danh sách phim theo id
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _movieRepo.GetMovieById(id);
            if (movie == null)
            {
                return NotFound(new { message = "Movie not found!" });
            }
            return Ok(movie);
        }

        // Thêm phim mới
        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMovie = await _movieRepo.AddMovie(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = newMovie.Id }, newMovie);
        }

        // Cập nhật phim
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest(new { message = "Movie ID missmatch!" });
            }

            var updateMovie = await _movieRepo.UpdateMovie(movie);

            if (!updateMovie)
            {
                return NotFound(new { message = "Movie not found!" });
            }

            return NoContent();
        }

        // Xóa phim
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var success = await _movieRepo.DeleteMovie(id);
            if (!success)
            {
                return NotFound(new { message = "Movie not found!" });
            }
            return Ok(new { message = "Movie deleted successfully" });
        }

        // Tìm kiếm phim
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies([FromQuery] string querry)
        {
            if (string.IsNullOrEmpty(querry))
            {
                return BadRequest(new { message = "Query cannot be empty!" });
            }

            var movies = await _movieRepo.SearchMovies(querry);
            return Ok(movies);
        }

        // Phân trang
        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetPagedMovies([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new { message = "Page and pageSize must be greater than 0!" });
            }

            var movies = await _movieRepo.GetPagedMovies(page, pageSize);
            return Ok(movies);
        }
    }
}