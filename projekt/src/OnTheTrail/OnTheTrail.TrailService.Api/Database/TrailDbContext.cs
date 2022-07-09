using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace OnTheTrail.TrailService.Api.Database;

public class TrailDbContext : DbContext
{
    [UsedImplicitly]
    public TrailDbContext(DbContextOptions<TrailDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}