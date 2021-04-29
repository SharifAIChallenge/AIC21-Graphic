using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int[][] cells { get; } // base0 --> 0, base1 --> 1, empty --> 2, wall --> 3
    public int BaseHealth { get; set; }
    public int WorkerHealth { get; set; }
    public int SoldierHealth { get; set; }
    public string Team0Name { get; set; }
    public string Team1Name { get; set; }
    public int WinnerTeam { get; set; }
    public int ShiftX { get; set; }
    public int ShiftY { get; set; }

    public Map(int[][] cells, int baseHealth, int workerHealth, int soldierHealth, string team0Name, string team1Name,
        int winnerTeam,int shiftX,int shiftY)
    {
        this.cells = cells;
        this.BaseHealth = baseHealth;
        this.WorkerHealth = workerHealth;
        this.SoldierHealth = soldierHealth;
        this.Team0Name = team0Name;
        this.Team1Name = team1Name;
        this.WinnerTeam = winnerTeam;
        this.ShiftX = shiftX;
        this.ShiftY = shiftY;
    }
}