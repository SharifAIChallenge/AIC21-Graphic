using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    public GameObject cell_empty;
    public GameObject cell_wall;
    public GameObject base1;
    public GameObject base2;
    public GameObject rec1;
    public GameObject rec2;
    private GameLog gameLog;
    private int currTurn;
    public int x0;
    public int y0;
    public int width;
    public int haight;
    private List<Object> Temps;


    public void StartGameManager(GameLog gameLog)
    {
        this.gameLog = gameLog;
        ShowMap();
        wait(1);
        for (int i = 0; i < gameLog.turns.Length; i++)
        {
            ApplyTurn(gameLog.turns[i]);
            wait(1);
        }
    }

    private IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void ApplyLog(int turn)
    {
        this.currTurn = turn;
        // Todo
    }

    public void SetGameLog(GameLog gameLog)
    {
        this.gameLog = gameLog;
    }

    private void ApplyTurn(Turn turn)
    {
        base1.GetComponent<BaseScript>().setHealth(turn.base1Health);
        base2.GetComponent<BaseScript>().setHealth(turn.base2Health);
        ApplyRecources(turn.recources1,rec1);
        ApplyRecources(turn.recources2,rec2);
    }

    private void ApplyRecources(int[][] recources,GameObject mainRec)
    {
        for (int i = 0; i < recources.Length; i++)
        {
            for (int j = 0; j < recources[0].Length; j++)
            {
                if (recources[i][j] > 0)
                {
                    GameObject rec = InstansiateCell(mainRec, i, j);
                    rec.GetComponent<RecourceScript>().SetAmount(recources[i][j]);
                    // Temps.Add(rec);
                }
            }
        }
    }

    private void ShowMap()
    {
        for (int i = 0; i < gameLog.map.cells.Length; i++)
        {
            for (int j = 0; j < gameLog.map.cells[0].Length; j++)
            {
                switch (gameLog.map.cells[i][j])
                {
                    case 1:
                        InstansiateCell(cell_empty, i, j);
                        break;
                    case 2:
                        InstansiateCell(cell_wall, i, j);
                        break;
                    case 3:
                        base1 = InstansiateCell(base1, i, j);
                        break;
                    case 4:
                        base2 = InstansiateCell(base2, i, j);
                        break;
                }
            }
        }
    }

    private GameObject InstansiateCell(GameObject instance, int i, int j)
    {
        return Instantiate(instance,
            new Vector3(x0 + i * width, y0 + j * haight, 0),
            Quaternion.identity);
    }
}