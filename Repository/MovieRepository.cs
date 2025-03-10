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
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies.Include(m => m.Showtimes).ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies.Include(m => m.Showtimes).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetPagedMovies(int page, int pageSize)
        {
            return await _context.Movies.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> SearchMovies(string querry)
        {
            return await _context.Movies.Where(m => m.Title.Contains(querry) || m.Genre.Contains(querry)).ToListAsync();
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            var existingMovie = await _context.Movies.FindAsync(movie.Id);
            if (existingMovie == null)
            {
                return false;
            }

            existingMovie.Title = movie.Title;
            existingMovie.Genre = movie.Genre;
            existingMovie.Duration = movie.Duration;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}