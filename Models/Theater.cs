using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class Theater
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Quan hệ 1 - n với Showtimes
        public List<Showtime> Showtimes { get; set; } = new();
    }
}