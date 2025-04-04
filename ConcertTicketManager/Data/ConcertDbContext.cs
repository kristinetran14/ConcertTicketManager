using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System;

namespace ConcertTicketManager.Data
{
    public class ConcertDbContext : DbContext
    {
        public ConcertDbContext(DbContextOptions<ConcertDbContext> options) : base(options) { }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Venue)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.HasOne(tt => tt.Event)
                      .WithMany(e => e.TicketTypes)
                      .HasForeignKey(tt => tt.EventId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(tt => tt.Price)
                      .HasPrecision(18, 4);
            });

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.TicketType)
                .WithMany(tt => tt.Reservations)
                .HasForeignKey(r => r.TicketTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.TicketType)
                .WithMany(tt => tt.Tickets)
                .HasForeignKey(t => t.TicketTypeId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Reservation)
                .WithMany()
                .HasForeignKey(t => t.ReservationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
