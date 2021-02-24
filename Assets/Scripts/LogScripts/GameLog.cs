
public class GameLog 
{
    public Map Map { get; }

    public Turn[] Turns { get; }

    public GameLog(Map map, Turn[] turns)
    {
        this.Map = map;
        this.Turns = turns;
    }

}
