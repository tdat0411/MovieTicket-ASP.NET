using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MovieTicketBooking.Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        // Quan hệ 1 - n với Bookings
        public List<Booking> Bookings { get; set; } = new();
    }
}