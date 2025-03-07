using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        public string Genre { get; set; }
        [Required]
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Quan hệ 1 - n với Showtimes
        public List<Showtime> Showtimes { get; set; } = new();
    }
}