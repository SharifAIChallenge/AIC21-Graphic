using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameLogReader : MonoBehaviour
{
    public GameLog GameLog { get; private set; }

    private void MakeLog(string filePath)
    {
        string jsonStr = File.ReadAllText(filePath);
        GameDTO gameDTO = JsonUtility.FromJson<GameDTO>(jsonStr);
        Debug.Log(JsonUtility.ToJson(gameDTO, prettyPrint: true));

        // making map
        GameConfigDTO gameConfigDTO = gameDTO.game_config;
        int height = gameConfigDTO.map_height;
        int width = gameConfigDTO.map_width;
        int[][] cells = new int[height][];
        for (int i = 0; i < height; i++)
        {
            cells[i] = new int[width];
        }

        foreach (CellTypeDTO cellTypeDTO in gameConfigDTO.cells_type)
        {
            cells[cellTypeDTO.row][cellTypeDTO.col] = cellTypeDTO.cell_type;
        }

        Map map = new Map(cells, gameConfigDTO.base_health, gameConfigDTO.worker_health, gameConfigDTO.soldier_health);

        //making turns
        Turn[] turns = new Turn[gameDTO.turns.Length];
        foreach (TurnDTO turnDTO in gameDTO.turns)
        {
            int base0Health = turnDTO.base0_health;
            int base1Health = turnDTO.base1_health;
            int[][] resources0 = new int[height][];
            int[][] resources1 = new int[height][];
            for (int i = 0; i < height; i++)
            {
                resources0[i] = new int[width];
                resources1[i] = new int[width];
                for (int j = 0; j < width; j++)
                {
                    resources0[i][j] = 0;
                    resources1[i][j] = 0;
                }
            }

            List<Ant> ants = new List<Ant>();
            foreach (CellDTO cellDTO in turnDTO.cells)
            {
                if (cellDTO.resource_type == 0)
                {
                    resources0[cellDTO.row][cellDTO.col] = cellDTO.resource_value;
                }
                else if (cellDTO.resource_type == 1)
                {
                    resources1[cellDTO.row][cellDTO.col] = cellDTO.resource_value;
                }

                foreach (AntDTO antDTO in cellDTO.ants)
                {
                    //NOTE: ants with same Id shouldn't have the same object here
                    ants.Add(new Ant(antDTO.id, antDTO.team, antDTO.type, antDTO.resource, cellDTO.row, cellDTO.col,
                        antDTO.health));
                }
            }

            Attack[] attacks = new Attack[turnDTO.attacks.Length];
            for (int i = 0; i < turnDTO.attacks.Length; i++)
            {
                AttackDTO attackDTO = turnDTO.attacks[i];
                attacks[i] = new Attack(attackDTO.attacker_id, attackDTO.defender_id, attackDTO.src_row,
                    attackDTO.src_col,
                    attackDTO.dst_row, attackDTO.dst_col);
            }

            turns[turnDTO.turn_num] = new Turn(base0Health, base1Health, resources0, resources1, ants, attacks,
                turnDTO.chat_box_0, turnDTO.chat_box_1);
        }

        this.GameLog = new GameLog(map, turns);
    }

    private void Awake()
    {
        MakeLog("./test1.json"); //TODO: must be called from UI, this is for test. ALSO don't need to inherit MonoBehaviour
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().StartGameManager(GameLog);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}