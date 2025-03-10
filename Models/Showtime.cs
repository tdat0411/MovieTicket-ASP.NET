using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class Showtime
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }
        [Required]
        public int TheaterId { get; set; }
        [ForeignKey("TheaterId")]
        public Theater? Theater { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }

        // Quan hệ 1 - n với Seat
        public List<Seat>? Seats { get; set; }
        // Quan hệ 1 - n với Booking
        public List<Booking>? Bookings { get; set; }
    }
}