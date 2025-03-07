using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ShowtimeId { get; set; }
        [ForeignKey("ShowtimeId")]
        public Showtime Showtime { get; set; }
        [Required, MaxLength(10)]
        public string SeatNumber { get; set; }
        [Required]
        public string Status { get; set; } = "Available";

        // Quan hệ 1 - 1 với Booking(Nếu ghế đã được đặt)
        public Booking Booking { get; set; }
    }
}