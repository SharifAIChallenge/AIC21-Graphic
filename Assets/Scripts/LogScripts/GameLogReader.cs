﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


public class GameLogReader : MonoBehaviour
{
    private string json;

    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData)
    {
        UploadFile(gameObject.name, "OnFileUpload", ".txt", false);
        Debug.Log("u");
    }

    // Called from browser
    public void OnFileUpload(string url)
    {
        Debug.Log(url);

        StartCoroutine(OutputRoutine(url));
    }

    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        Debug.Log(url);
        Debug.Log(Application.dataPath);
        yield return loader;
        GameLogReader.Instance.MakeLog(loader.text);
    }

    public static GameLogReader Instance;

    private void Awake()
    {
        Instance = this;
        // StartCoroutine(OutputRoutine(new System.Uri( Application.dataPath+"/log1.json").AbsoluteUri));
        // ReadLog("/log1.json");
    }

    public void ReadLog(string filePath)
    {
        StartCoroutine(OutputRoutine(new System.Uri(Application.dataPath + "/" + filePath).AbsoluteUri));
    }

    public void MakePartLog(string partJson)
    {
        json += partJson;
    }

    public void MakeLogStart()
    {
        MakeLog(json);
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
        int widthB = height;
        int heightB = width;
        int[][] cells = new int[heightB][];
        for (int i = 0; i < heightB; i++)
        {
            cells[i] = new int[widthB];
        }

        foreach (CellTypeDTO cellTypeDTO in gameConfigDTO.cells_type)
        {
            cells[cellTypeDTO.col][cellTypeDTO.row] = cellTypeDTO.cell_type;
        }

        Map map = new Map(cells, gameConfigDTO.base_health, gameConfigDTO.worker_health,
            gameConfigDTO.soldier_health, gameConfigDTO.team0_name, gameConfigDTO.team1_name, gameConfigDTO.winner,
            gameConfigDTO.shift_x, gameConfigDTO.shift_y);

        //making turns
        Turn[] turns = new Turn[gameDTO.turns.Length];
        foreach (TurnDTO turnDTO in gameDTO.turns)
        {
            int base0Health = turnDTO.base0_health;
            int base1Health = turnDTO.base1_health;
            int[][] resources0 = new int[heightB][];
            int[][] resources1 = new int[heightB][];
            for (int i = 0; i < heightB; i++)
            {
                resources0[i] = new int[widthB];
                resources1[i] = new int[widthB];
                for (int j = 0; j < widthB; j++)
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
                    resources0[cellDTO.col][cellDTO.row] = cellDTO.resource_value;
                }
                else if (cellDTO.resource_type == 1)
                {
                    resources1[cellDTO.col][cellDTO.row] = cellDTO.resource_value;
                }

                foreach (AntDTO antDTO in cellDTO.ants)
                {
                    //NOTE: ants with same Id shouldn't have the same object here
                    ants.Add(new Ant(antDTO.id, antDTO.team, antDTO.type, antDTO.resource, cellDTO.col, cellDTO.row,
                        antDTO.health));
                }
            }

            Attack[] attacks = new Attack[turnDTO.attacks.Length];
            for (int i = 0; i < turnDTO.attacks.Length; i++)
            {
                AttackDTO attackDTO = turnDTO.attacks[i];
                attacks[i] = new Attack(attackDTO.attacker_id, attackDTO.defender_id, attackDTO.src_col,
                    attackDTO.src_row,
                    attackDTO.dst_col, attackDTO.dst_row);
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
                importantChatBox0, importantChatBox1, trivialChatBox0, trivialChatBox1,
                turnDTO.team0_alive_workers,
                turnDTO.team0_total_workers,
                turnDTO.team0_alive_soldiers,
                turnDTO.team0_total_soldiers,
                turnDTO.team0_current_resource0,
                turnDTO.team0_total_resource0,
                turnDTO.team0_current_resource1,
                turnDTO.team0_total_resource1,
                turnDTO.team1_alive_workers,
                turnDTO.team1_total_workers,
                turnDTO.team1_alive_soldiers,
                turnDTO.team1_total_soldiers,
                turnDTO.team1_current_resource0,
                turnDTO.team1_total_resource0,
                turnDTO.team1_current_resource1,
                turnDTO.team1_total_resource1
            );
        }

        this.GameLog = new GameLog(map, turns);
        GameManager.Instance.StartGameManager(GameLog);
    }
}