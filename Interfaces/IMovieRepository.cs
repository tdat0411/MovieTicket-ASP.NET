using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(int id);
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<bool> DeleteMovie(int id);
    }
}