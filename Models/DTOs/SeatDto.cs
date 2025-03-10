using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models.DTOs
{
    public class SeatDto
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public string Status { get; set; }
    }
}