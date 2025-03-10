using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieTicketBooking.Models;
using MovieTicketBooking.Models.DTOs;

namespace MovieTicketBooking.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Showtime, ShowtimeDto>();
            CreateMap<ShowtimeDto, Showtime>();
            CreateMap<Seat, SeatDto>();
        }
    }
}