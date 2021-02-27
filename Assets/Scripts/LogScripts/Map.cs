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

    public Map(int[][] cells, int baseHealth, int workerHealth, int soldierHealth)
    {
        this.cells = cells;
        this.BaseHealth = baseHealth;
        this.WorkerHealth = workerHealth;
        this.SoldierHealth = soldierHealth;
    }
}