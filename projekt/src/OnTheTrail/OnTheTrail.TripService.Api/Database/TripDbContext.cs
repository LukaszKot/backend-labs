using Microsoft.EntityFrameworkCore;
using OnTheTrail.TripService.Api.Database.Entities;

namespace OnTheTrail.TripService.Api.Database;

public class TripDbContext : DbContext
{
    public TripDbContext(DbContextOptions<TripDbContext> options) : base(options){}

    public virtual DbSet<Search> Searches { get; set; } = null!;
    public virtual DbSet<Trip> Trips { get; set; } = null!;
    public virtual DbSet<TripNode> TripNodes { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();
            user.HasKey(x => x.Id);
            user.Property(x => x.Id).ValueGeneratedNever();
        base.OnModelCreating(modelBuilder);
    }
}