using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models.DTOs
{
    public class ShowtimeDto
    {
        public int Id { get; set; }
        public DateTime Showtime { get; set; }
    }
}