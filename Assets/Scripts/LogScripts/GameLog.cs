using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLog 
{
    public Map map { get; }

    public Turn[] turns { get; }

    public GameLog(Map map, Turn[] turns)
    {
        this.map = map;
        this.turns = turns;
    }

}
