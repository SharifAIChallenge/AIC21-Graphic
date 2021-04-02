[System.Serializable]
public class GameConfigDTO
{
    public int map_height;
    public int map_width;
    public int base_health;
    public int worker_health;
    public int soldier_health;
    public CellTypeDTO[] cells_type;
    public string team0_name;
    public string team1_name;
    public int winner;
}
