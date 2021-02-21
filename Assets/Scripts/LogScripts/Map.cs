using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map 
{
    public int[][] cells { get; } // base --> 0, empty --> 1, wall --> 2
    public Map(int[][] cells)
    {
        this.cells = cells;
    }
}