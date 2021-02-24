
public class Ant
{
    public int Id { get; }
    public int Team { get; }
    public int Type { get; }
    public int Resource { get; }
    public int Row { get; }
    public int Col { get; }

    public Ant(int id, int team, int type, int resource, int row, int col)
    {
        Id = id;
        Team = team;
        Type = type;
        Resource = resource;
        Row = row;
        Col = col;
    }
}
