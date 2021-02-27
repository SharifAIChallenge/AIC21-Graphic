using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    public GameObject antPrefab;
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
    private List<GameObject> Temps = new List<GameObject>();
    private Hashtable AntsTable = new Hashtable();
    public int MaxTurns { get; private set; }


    public void StartGameManager(GameLog gameLog)
    {
        this.gameLog = gameLog;
        base1.GetComponent<BaseScript>().SetMaxHealth(gameLog.Map.BaseHealth);
        base2.GetComponent<BaseScript>().SetMaxHealth(gameLog.Map.BaseHealth);
        ShowMap();
        FindObjectOfType<MoveCamera>().setMaid(gameLog.Map.cells.Length*width,gameLog.Map.cells[0].Length*haight);
        MaxTurns = gameLog.Turns.Length;
    }

    public void ApplyLog(int turn)
    {
        if (currTurn == turn + 1)
        {
            ApplyTurnAnim(gameLog.Turns[turn - 1]);
        }
        else
        {
            ApplyTurnUnAnim(gameLog.Turns[turn - 1]);
        }

        this.currTurn = turn;
    }


    private void ApplyTurnUnAnim(Turn turn)
    {
        ChatManager.Instance.SetLeftChatMessages(turn.ChatBox0.Split(','));
        ChatManager.Instance.SetRightChatMessages(turn.ChatBox1.Split(','));
        Debug.Log("applyTurn " + currTurn);
        foreach (GameObject temp in Temps)
        {
            Destroy(temp);
        }

        base1.GetComponent<BaseScript>().setHealth(turn.Base0Health);
        base2.GetComponent<BaseScript>().setHealth(turn.Base1Health);
        ApplyResources(turn.Resources0, rec1);
        ApplyResources(turn.Resources1, rec2);
        foreach (DictionaryEntry antDE in AntsTable)
        {
            Destroy((GameObject) antDE.Value);
        }

        AntsTable.Clear();
        foreach (Ant ant in turn.Ants)
        {
            GameObject antObject = Instantiate(antPrefab);
            antObject.GetComponent<AntScript>().Set(ant.Row, ant.Col, ant.Team, ant.Type, ant.Health, ant.Resource);
            AntsTable.Add(ant.Id, antObject);
        }
    }

    private void ApplyTurnAnim(Turn turn)
    {
        ChatManager.Instance.SetLeftChatMessages(turn.ChatBox0.Split(','));
        ChatManager.Instance.SetRightChatMessages(turn.ChatBox1.Split(','));
        Debug.Log("applyTurn " + currTurn);
        foreach (GameObject temp in Temps)
        {
            Destroy(temp);
        }

        base1.GetComponent<BaseScript>().setHealth(turn.Base0Health);
        base2.GetComponent<BaseScript>().setHealth(turn.Base1Health);
        ApplyResources(turn.Resources0, rec1);
        ApplyResources(turn.Resources1, rec2);
        Hashtable cloneAntTable = (Hashtable) AntsTable.Clone();
        foreach (Ant ant in turn.Ants)
        {
            GameObject antObject = null;
            if (AntsTable.Contains(ant.Id))
            {
                cloneAntTable.Remove(ant.Id);
                antObject = (GameObject) AntsTable[ant.Id];
                antObject.GetComponent<AntScript>().Go(ant.Row, ant.Col, ant.Health, ant.Resource);
            }
            else
            {
                antObject = Instantiate(antPrefab);
                antObject.GetComponent<AntScript>().Set(ant.Row, ant.Col, ant.Team, ant.Type, ant.Health, ant.Resource);
                AntsTable.Add(ant.Id, antObject);
            }
        }
        foreach (DictionaryEntry antDE in cloneAntTable)
        {
            int a = 1;
            Destroy((GameObject) antDE.Value);
        }
    }

    private void ApplyResources(int[][] recources, GameObject mainRec)
    {
        for (int i = 0; i < recources.Length; i++)
        {
            for (int j = 0; j < recources[0].Length; j++)
            {
                if (recources[i][j] > 0)
                {
                    GameObject rec = InstansiateCell(mainRec, i, j);
                    rec.GetComponent<RecourceScript>().SetAmount(recources[i][j]);
                    Temps.Add(rec);
                }
            }
        }
    }

    private void ShowMap()
    {
        for (int i = 0; i < gameLog.Map.cells.Length; i++)
        {
            for (int j = 0; j < gameLog.Map.cells[0].Length; j++)
            {
                switch (gameLog.Map.cells[i][j])
                {
                    case 2:
                        InstansiateCell(cell_empty, i, j);
                        break;
                    case 3:
                        InstansiateCell(cell_wall, i, j);
                        break;
                    case 0:
                        base1 = InstansiateCell(base1, i, j);
                        break;
                    case 1:
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