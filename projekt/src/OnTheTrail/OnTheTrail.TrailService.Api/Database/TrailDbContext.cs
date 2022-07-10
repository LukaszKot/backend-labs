using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OnTheTrail.TrailService.Api.Database.Entities;
using OnTheTrail.TrailService.Api.Enums;

namespace OnTheTrail.TrailService.Api.Database;

public class TrailDbContext : DbContext
{
    [UsedImplicitly]
    public TrailDbContext(DbContextOptions<TrailDbContext> options) : base(options)
    {
    }
    
    public DbSet<Node> Nodes { get; set; }
    public DbSet<Trail> Trails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Node>()
            .HasMany(x => x.TrailsBeginnings)
            .WithOne(x => x.Beginning)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Node>()
            .HasMany(x => x.TrailsEnds)
            .WithOne(x => x.End)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Node>()
            .Ignore(x => x.Trails);
        base.OnModelCreating(modelBuilder);
    }

    public void SeedData()
    {
        if (Nodes.Any()) return;
        var lysaPolana = new Node("Łysa Polana", 970, NodeType.Parking);
        var palenicaBialczanska = new Node("Palenica Białczańska", 984, NodeType.Parking);
        var lasPodWoloszynem = new Node("Las pod Wołoszynem", 1079, NodeType.Fork);
        var wodogrzmotyMickiewicza = new Node("Wodogrzmoty Mickiewicza", 1100, NodeType.Fork);
        var schroniskoWRoztoce = new Node("Schronisko w Roztoce", 1031, NodeType.Shelter);
        var nadZabimOkiem = new Node("Nad Żabim Okiem", 1398, NodeType.Fork);
        var schroniskoMorskieOko = new Node("Schronisko przy Morskim Oku", 1406, NodeType.Shelter);
        var schronisko5 = new Node("Schronisko w Dolinie Pięciu Stawów Polskich", 1672, NodeType.Shelter);
        var rzezuchy = new Node("Rzeżuchy", 1435, NodeType.Fork);
        var wielkiStawPolski = new Node("Wielki Staw Polski", 1666, NodeType.Fork);

        Trails.AddRange(new Trail[]
        {
            new(lysaPolana, "Nie oznaczony", palenicaBialczanska, TrailDifficulty.ValleyWalk, 20, 984),
            new(palenicaBialczanska, "Nie oznaczony", lysaPolana, TrailDifficulty.ValleyWalk, 20, 984),
            new(palenicaBialczanska, "Czerwony", lasPodWoloszynem, TrailDifficulty.ValleyWalk, 45, 1079),
            new(lasPodWoloszynem, "Czerwony", palenicaBialczanska, TrailDifficulty.ValleyWalk, 35, 1079),
            new(lasPodWoloszynem, "Czerwony", wodogrzmotyMickiewicza, TrailDifficulty.ValleyWalk, 5, 1100),
            new(wodogrzmotyMickiewicza, "Czerwony", lasPodWoloszynem, TrailDifficulty.ValleyWalk, 5, 1100),
            new(wodogrzmotyMickiewicza, "Zielony", schroniskoWRoztoce, TrailDifficulty.Normal, 10, 1100),
            new(schroniskoWRoztoce, "Zielony", wodogrzmotyMickiewicza, TrailDifficulty.Normal, 15, 1100),
            new(wodogrzmotyMickiewicza, "Czerwony", nadZabimOkiem, TrailDifficulty.ValleyWalk, 90, 1398),
            new(nadZabimOkiem, "Czerwony", wodogrzmotyMickiewicza, TrailDifficulty.ValleyWalk, 75, 1398),
            new(nadZabimOkiem, "Czerwony", schroniskoMorskieOko, TrailDifficulty.ValleyWalk, 5, 1406),
            new(schroniskoMorskieOko, "Czerwony", nadZabimOkiem, TrailDifficulty.ValleyWalk, 5, 1406),
            new(nadZabimOkiem, "Niebieski", schronisko5, TrailDifficulty.Normal, 120, 1850),
            new(schronisko5, "Niebieski", nadZabimOkiem, TrailDifficulty.Normal, 100, 1850),
            new(schronisko5, "Czarny", rzezuchy, TrailDifficulty.Normal, 30, 1672),
            new(rzezuchy, "Czarny", schronisko5, TrailDifficulty.Normal, 40, 1672),
            new(rzezuchy, "Zielony", wielkiStawPolski, TrailDifficulty.Normal, 45, 1666),
            new(wielkiStawPolski, "Zielony", rzezuchy, TrailDifficulty.Normal, 30, 1666),
            new(wielkiStawPolski, "Niebieski", schronisko5, TrailDifficulty.ValleyWalk, 10, 1672),
            new(schronisko5, "Niebieski", wielkiStawPolski, TrailDifficulty.ValleyWalk, 10, 1672),
            new(rzezuchy, "Zielony", wodogrzmotyMickiewicza, TrailDifficulty.Normal, 60, 1435),
            new(wodogrzmotyMickiewicza, "Zielony", rzezuchy, TrailDifficulty.Normal, 80, 1435),
        });
        SaveChanges();
    }
}