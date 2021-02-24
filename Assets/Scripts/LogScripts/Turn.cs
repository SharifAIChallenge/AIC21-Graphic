﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public int Base0Health { get; }
    public int Base1Health { get; }
    public int[][] Resources0 { get; }
    public int[][] Resources1 { get; }
    public List<Ant> Ants { get; }
    public Attack[] Attacks { get; }

    public Turn(int base0Health, int base1Health, int[][] resources0, int[][] resources1, List<Ant> ants, Attack[] attacks)
    {
        this.Base0Health = base0Health;
        this.Base1Health = base1Health;
        this.Resources0 = resources0;
        this.Resources1 = resources1;
        this.Ants = ants;
        this.Attacks = attacks;
    }
}
