using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public int base1Health { get; }
    public int base2Health { get; }
    public int[][] recources1 { get; }
    public int[][] recources2 { get; }

    public Turn(int base1Health,int base2Health,int[][] recources1,int[][] recources2)
    {
        this.base1Health = base1Health;
        this.base2Health = base2Health;
        this.recources1 = recources1;
        this.recources2 = recources2;
    }
}
