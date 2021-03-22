using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.Networking;


public class GameLogReader : MonoBehaviour
{
    public static GameLogReader Instance;

    public bool isWebGL;
    public TextMeshProUGUI TextMeshProUgui;

    private void Awake()
    {
        Instance = this;
    }

    public GameLog GameLog { get; private set; }

    public void MakeLog(string jsonStr)
    {
        // Debug.Log(jsonStr);

        GameDTO gameDTO = JsonUtility.FromJson<GameDTO>(jsonStr);
        // Debug.Log(JsonUtility.ToJson(gameDTO, prettyPrint: true));

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

        Map map = new Map(cells, gameConfigDTO.base_health, gameConfigDTO.worker_health, 
            gameConfigDTO.soldier_health, gameConfigDTO.team0_name, gameConfigDTO.team1_name);

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

            int len = turnDTO.important_chat_box_0.Length;
            Chat[] importantChatBox0 = new Chat[len];
            for (int i = 0; i < len; i++)
            {
                ChatDTO chatDTO = turnDTO.important_chat_box_0[i];
                importantChatBox0[i] = new Chat(chatDTO.text, chatDTO.value, chatDTO.sender_id);
            }
            len = turnDTO.important_chat_box_1.Length;
            Chat[] importantChatBox1 = new Chat[len];
            for (int i = 0; i < len; i++)
            {
                ChatDTO chatDTO = turnDTO.important_chat_box_1[i];
                importantChatBox1[i] = new Chat(chatDTO.text, chatDTO.value, chatDTO.sender_id);
            }
            len = turnDTO.trivial_chat_box_0.Length;
            Chat[] trivialChatBox0 = new Chat[len];
            for (int i = 0; i < len; i++)
            {
                ChatDTO chatDTO = turnDTO.trivial_chat_box_0[i];
                trivialChatBox0[i] = new Chat(chatDTO.text, chatDTO.value, chatDTO.sender_id);
            }
            len = turnDTO.trivial_chat_box_1.Length;
            Chat[] trivialChatBox1 = new Chat[len];
            for (int i = 0; i < len; i++)
            {
                ChatDTO chatDTO = turnDTO.trivial_chat_box_1[i];
                trivialChatBox1[i] = new Chat(chatDTO.text, chatDTO.value, chatDTO.sender_id);
            }

            turns[turnDTO.turn_num] = new Turn(base0Health, base1Health, resources0, resources1, ants, attacks,
                importantChatBox0, importantChatBox1, trivialChatBox0, trivialChatBox1);
        }

        this.GameLog = new GameLog(map, turns);
        GameManager.Instance.StartGameManager(GameLog);
    }
}