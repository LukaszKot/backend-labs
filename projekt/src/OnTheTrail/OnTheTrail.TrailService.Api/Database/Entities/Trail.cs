using JetBrains.Annotations;
using OnTheTrail.TrailService.Api.Enums;

namespace OnTheTrail.TrailService.Api.Database.Entities;

public class Trail
{
    public long Id { get; protected set; }
    public string Color { get; protected set; }
    public int MaxHeight { get; protected set; }
    public TrailDifficulty Difficulty { get; protected set; }
    public Node Beginning { get; protected set; }
    public Node End { get; protected set; }
    public int TimeInMinutes { get; protected set; }

    [UsedImplicitly]
    protected Trail()
    {
    }

    public Trail(Node from, string color, Node to, TrailDifficulty difficulty, int timeInMinutes, int maxHeight)
    {
        Beginning = from;
        Color = color;
        End = to;
        Difficulty = difficulty;
        TimeInMinutes = timeInMinutes;
        MaxHeight = maxHeight;
    }
}