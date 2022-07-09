using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OnTheTrail.IdentityService.Api.Database.Entities;

namespace OnTheTrail.IdentityService.Api.Database;

public class IdentityDbContext : DbContext
{
    [UsedImplicitly]
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
        
    }
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}