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
    public Chat[] ImportantChatBox0 { get; }
    public Chat[] ImportantChatBox1 { get; }
    public Chat[] TrivialChatBox0 { get; }
    public Chat[] TrivialChatBox1 { get; }
    public List<int>[][] CellAnts { get; }

    public Turn(int base0Health, int base1Health, int[][] resources0, int[][] resources1, List<Ant> ants,
        Attack[] attacks, Chat[] importantChatBox0, Chat[] importantChatBox1, 
        Chat[] trivialChatBox0, Chat[] trivialChatBox1)
    {
        this.Base0Health = base0Health;
        this.Base1Health = base1Health;
        this.Resources0 = resources0;
        this.Resources1 = resources1;
        this.Ants = ants;
        this.Attacks = attacks;
        ImportantChatBox0 = importantChatBox0;
        ImportantChatBox1 = importantChatBox1;
        TrivialChatBox0 = trivialChatBox0;
        TrivialChatBox1 = trivialChatBox1;

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