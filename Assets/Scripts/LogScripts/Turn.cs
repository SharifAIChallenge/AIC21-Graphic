using System.Collections;
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
    public string ChatBox0 { get; }
    public string ChatBox1 { get; }
    public List<int>[][] CellAnts { get; }

    public Turn(int base0Health, int base1Health, int[][] resources0, int[][] resources1, List<Ant> ants,
        Attack[] attacks, string chatBox0,string chatBox1)
    {
        this.Base0Health = base0Health;
        this.Base1Health = base1Health;
        this.Resources0 = resources0;
        this.Resources1 = resources1;
        this.Ants = ants;
        this.Attacks = attacks;
        ChatBox0 = chatBox0;
        ChatBox1 = chatBox1;

        int rows = resources0.Length;
        int cols = resources0[0].Length;
        CellAnts = new List<int>[rows][];
        for (int i = 0; i < rows; i++)
        {
            CellAnts[i] = new List<int>[cols];
            for (int j = 0; j < cols; j++)
            {
                CellAnts[i][j] = new List<int>();
            }
        }
        foreach (Ant ant in ants)
        {
            CellAnts[ant.Row][ant.Col].Add(ant.Id);
        }
    }
}