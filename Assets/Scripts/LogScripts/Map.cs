using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map 
{
    public int[][] cells { get; } // base0 --> 0, base1 --> 1, empty --> 2, wall --> 3
    public Map(int[][] cells)
    {
        this.cells = cells;
    }
}