using Microsoft.EntityFrameworkCore;

public class ConcertDbContext : DbContext
{
    public ConcertDbContext(DbContextOptions<ConcertDbContext> options)
        : base(options)
    { }

    public DbSet<Venue> Venues { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<TicketType> TicketTypes { get; set; }
    public DbSet<TicketReservation> TicketReservations { get; set; }
    public DbSet<TicketPurchase> TicketPurchases { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        // TicketType and Event
        modelBuilder.Entity<TicketType>()
            .HasOne(t => t.Event) // Each TicketType has one Event
            .WithMany(e => e.TicketTypes) // Each Event can have many TicketTypes
            .HasForeignKey(t => t.EventId) // Foreign key on EventId in TicketType
            .OnDelete(DeleteBehavior.Cascade); // Delete related TicketTypes when an Event is deleted

        // TicketReservation and Customer
        modelBuilder.Entity<TicketReservation>()
            .HasOne(tr => tr.Customer) // Each TicketReservation is associated with one Customer
            .WithMany(c => c.Reservations) // Each Customer can have many TicketReservations
            .HasForeignKey(tr => tr.CustomerId) // Foreign key on CustomerId in TicketReservation
            .OnDelete(DeleteBehavior.Cascade); // Delete related TicketReservations when a Customer is deleted

        // TicketReservation and TicketType
        modelBuilder.Entity<TicketReservation>()
            .HasOne(tr => tr.TicketType) // Each TicketReservation is associated with one TicketType
            .WithMany(tt => tt.Reservations) // Each TicketType can have many TicketReservations
            .HasForeignKey(tr => tr.TicketTypeId) // Foreign key on TicketTypeId in TicketReservation
            .OnDelete(DeleteBehavior.Cascade); // Delete related TicketReservations when a TicketType is deleted

        // TicketPurchase and TicketReservation
        modelBuilder.Entity<TicketPurchase>()
            .HasOne(tp => tp.Reservation) // Each TicketPurchase is associated with one TicketReservation
            .WithOne(tr => tr.TicketPurchase) // Each TicketReservation has one associated TicketPurchase
            .HasForeignKey<TicketPurchase>(tp => tp.ReservationId) // Foreign key on ReservationId in TicketPurchase
            .OnDelete(DeleteBehavior.Cascade); // Delete related TicketPurchases when a TicketReservation is deleted

        // Venue and Event
        modelBuilder.Entity<Venue>()
            .HasMany(v => v.Events) // Each Venue can have many Events
            .WithOne(e => e.Venue) // Each Event is associated with one Venue
            .HasForeignKey(e => e.VenueId) // Foreign key on VenueId in Event
            .OnDelete(DeleteBehavior.Cascade); // Delete related Events when a Venue is deleted

        modelBuilder.Entity<TicketType>()
        .Property(t => t.Price)
        .HasPrecision(18, 2);
    }
}
