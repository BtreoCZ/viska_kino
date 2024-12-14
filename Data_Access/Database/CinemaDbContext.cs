using Data_Access.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Database
{
    public class CinemaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ScreeningRoom> ScreeningRooms { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cinema.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Film)
                .WithMany()
                .HasForeignKey(s => s.FilmId);

            modelBuilder.Entity<Screening>()
                .HasOne(s => s.ScreeningRoom)
                .WithMany()
                .HasForeignKey(s => s.ScreeningRoomId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Screening)
                .WithMany()
                .HasForeignKey(r => r.ScreeningId);
        }
    }
}
