using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntScript : MonoBehaviour
{
    private int x;

    private int y;

    private int health;

    private int recource;
    private int team;
    private int type;

    public void Set(int x, int y,int team,int type, int health, int recource)
    {
        this.x = x;
        this.y = y;
        this.recource = recource;
        this.health = health;
        this.team = team;
        this.type = type;
        //todo GUI effect
    }
}
