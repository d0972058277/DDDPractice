using Microsoft.EntityFrameworkCore;

namespace TrainTicketBookingSystem.WebApi;

public class TrainTicketBoolingSystemDbContext : DbContext
{
    public TrainTicketBoolingSystemDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Train> Trains => Set<Train>();

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

            train.OwnsOne(e => e.Date, date =>
            {
                date.Property(e => e.Value).HasColumnName("Date");
            });
        });
    }
}
