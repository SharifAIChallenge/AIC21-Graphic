
public class Ant
{
    public static readonly int WORKER = 1; 
    public static readonly int SOLDIER = 0;
    public int Id { get; }
    public int Team { get; }
    public int Type { get; }
    public int Resource { get; }
    public int Row { get; }
    public int Col { get; }
    public int Health { get; }

    public Ant(int id, int team, int type, int resource, int row, int col,int health)
    {
        Id = id;
        Team = team;
        Type = type;
        Resource = resource;
        Row = row;
        Col = col;
        Health = health;
    }

    public override bool Equals(object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null))
        {
            return false;
        }
        else {
            Ant p = (Ant) obj;
            return this.Id == p.Id;
        }
    }
}
