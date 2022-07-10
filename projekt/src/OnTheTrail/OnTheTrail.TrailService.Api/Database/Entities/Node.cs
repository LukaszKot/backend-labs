using JetBrains.Annotations;
using OnTheTrail.TrailService.Api.Enums;

namespace OnTheTrail.TrailService.Api.Database.Entities;

public class Node
{
    public long Id { get; protected set; }
    public string Name { get; protected set; }
    public int Height { get; protected set; }
    public NodeType Type { get; protected set; }
    public ICollection<Trail> TrailsBeginnings { get; protected set; }
    public ICollection<Trail> TrailsEnds { get; protected set; }
    public List<Trail> Trails => TrailsBeginnings.Union(TrailsEnds).ToList();

    [UsedImplicitly]
    protected Node()
    {
    }
    public Node(string name, int height, NodeType type)
    {
        Name = name;
        Height = height;
        Type = type;
        TrailsBeginnings = new List<Trail>();
        TrailsEnds = new List<Trail>();
    }
}