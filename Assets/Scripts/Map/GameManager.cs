using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cell_empty;
    public GameObject cell_wall;
    private GameLog gameLog;
    private int currTurn;
    public int x0;
    public int y0;
    public int width;
    public int haight;
    

    public void StartGameManager(GameLog gameLog)
    {
        this.gameLog = gameLog;
        ShowMap();
    }

    public void applyLog(int turn)
    {
        this.currTurn = turn;
        // Todo
    }

    public void SetGameLog(GameLog gameLog)
    {
        this.gameLog = gameLog;
    }

    public void ShowMap()
    {
        for (int i = gameLog.map.cells.Length-1; i >= 0; i--)
        {
            for (int j = gameLog.map.cells[0].Length-1; j >= 0; j--)
            {
                GameObject instance = null;
                switch (gameLog.map.cells[i][j])
                {
                    case 1:
                        instance = cell_empty;
                        break;
                    case 2:
                        instance = cell_wall;
                        break;
                }
                Instantiate(instance,
                    new Vector3(x0 + i * width, y0 + j * haight, 0),
                    Quaternion.identity);
            }
        }
    }
}