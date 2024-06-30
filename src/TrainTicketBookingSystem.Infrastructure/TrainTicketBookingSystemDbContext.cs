using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainTicketBookingSystem.Domain.Models;

namespace TrainTicketBookingSystem.Infrastructure;

public class TrainTicketBookingSystemDbContext : DbContext
{
    public TrainTicketBookingSystemDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Train> Trains => Set<Train>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Train>(train =>
        {
            train.ToTable("Train");

            train.HasKey(e => e.Id);
            train.Ignore(e => e.DomainEvents);

            train.Property(e => e.Seats);

            train.OwnsMany(e => e.Locations, location =>
            {
                location.ToTable("TrainLocation");

                location.Property<long>("_id").ValueGeneratedOnAdd().IsRequired();
                location.HasKey("_id");

                location.Property(e => e.Name).HasColumnName("Name");
            });

            train.OwnsOne(e => e.Date, date => { date.Property(e => e.Value).HasColumnName("Date"); });
        });

        modelBuilder.Entity<Ticket>(ticket =>
        {
            ticket.ToTable("Ticket");

            ticket.HasKey(e => e.Id);
            ticket.Ignore(e => e.DomainEvents);

            ticket.Property(e => e.TrainId);
            ticket.OwnsOne(e => e.From, location => { location.Property(e => e.Name).HasColumnName("From"); });
            ticket.OwnsOne(e => e.To, location => { location.Property(e => e.Name).HasColumnName("To"); });
            ticket.OwnsOne(e => e.Date, date => { date.Property(e => e.Value).HasColumnName("Date"); });
            ticket.Property(e => e.PaymentStatus).HasConversion(new EnumToStringConverter<PaymentStatus>());
        });
    }
}