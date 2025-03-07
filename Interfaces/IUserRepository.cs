using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> Register(User user);
        Task<User?> Authenticate(string email, string password);
        Task<List<User>> GetAllUser();
    }
}