using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Định nghĩa mối quan hệ 1 - 1 seat và booking
            builder.Entity<Seat>()
            .HasOne(s => s.Booking)
            .WithOne(b => b.Seat)
            .HasForeignKey<Booking>(b => b.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

            // Mối quan hệ 1 - n giữa User và Booking
            builder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            // Mối quan hệ 1 - n giữa Movie và Showtime
            builder.Entity<Showtime>()
            .HasOne(s => s.Movie)
            .WithMany(m => m.Showtimes)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ 1 - n giữa Theater và Showtime
            builder.Entity<Showtime>()
            .HasOne(s => s.Theater)
            .WithMany(t => t.Showtimes)
            .HasForeignKey(s => s.TheaterId)
            .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ 1 - n giữa Showtime và Seat
            builder.Entity<Seat>()
            .HasOne(seat => seat.Showtime)
            .WithMany(showtime => showtime.Seats)
            .HasForeignKey(seat => seat.ShowtimeId)
            .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ 1 - n giữa Showtime và Booking
            builder.Entity<Booking>()
            .HasOne(b => b.Showtime)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.ShowtimeId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }

    }
}