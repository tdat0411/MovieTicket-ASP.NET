using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // Lấy all danh sách user
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userRepo.GetAllUser();
            return Ok(users);
        }

        // Đăng ký user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUsers = await _userRepo.GetAllUser();
            if (existingUsers.Any(u => u.Email == user.Email))
            {
                return BadRequest("Email đã tồn tại");
            }

            var newUser = await _userRepo.Register(user);
            return Ok(new { message = "Đăng ký thành công!", user = newUser });
        }

        // Đăng nhập user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userRepo.Authenticate(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized("Email hoặc Password không đúng!");
            }
            return Ok(new { message = "Đăng nhập thành công!", user });
        }
    }
}