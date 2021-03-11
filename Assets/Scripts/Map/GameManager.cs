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
    private float baseTime;
    public bool playAnime;


    public void StartGameManager(GameLog gameLog)
    {
        baseTime = UIManager.Instance.BaseTime;
        this.gameLog = gameLog;
        base1.GetComponent<BaseScript>().SetMaxHealth(gameLog.Map.BaseHealth);
        base2.GetComponent<BaseScript>().SetMaxHealth(gameLog.Map.BaseHealth);
        ShowMap();
        FindObjectOfType<MoveCamera>().setMaid(gameLog.Map.cells.Length * width, gameLog.Map.cells[0].Length * haight);
        MaxTurns = gameLog.Turns.Length;
    }

    public void ApplyLog(int turn)
    {
        Debug.Log(currTurn+" "+turn);
        if (currTurn == turn - 1)
        {
            playAnime = true;
            StartCoroutine(ApplyTurnAnim(gameLog.Turns[turn - 1]));
        }
        else
        {
            playAnime = false;
            ApplyTurnUnAnim(gameLog.Turns[turn - 1]);
        }
        currTurn = turn;
    }


    private void ApplyTurnUnAnim(Turn turn)
    {
        ChatManager.Instance.SetLeftChatMessages(turn.ChatBox0.Split(','));
        ChatManager.Instance.SetRightChatMessages(turn.ChatBox1.Split(','));
        Debug.Log("unAnim move "+currTurn);
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

    private IEnumerator ApplyTurnAnim(Turn turn)
    {
        //attack and dead and get recource and set recource and set base healthes
        ChatManager.Instance.SetLeftChatMessages(turn.ChatBox0.Split(','));
        ChatManager.Instance.SetRightChatMessages(turn.ChatBox1.Split(','));
        Debug.Log("anim move " + currTurn);
        foreach (GameObject temp in Temps)
        {
            Destroy(temp);
        }

        base1.GetComponent<BaseScript>().setHealth(turn.Base0Health);
        base2.GetComponent<BaseScript>().setHealth(turn.Base1Health);
        ApplyResources(turn.Resources0, rec1);
        ApplyResources(turn.Resources1, rec2);
        Hashtable cloneAntTable = (Hashtable) AntsTable.Clone();
        Hashtable MovingAnts = new Hashtable();
        Hashtable NewAnts = new Hashtable();
        foreach (Ant ant in turn.Ants)
        {
            GameObject antObject = null;
            if (AntsTable.Contains(ant.Id))
            {
                cloneAntTable.Remove(ant.Id);
                antObject = (GameObject) AntsTable[ant.Id];
                MovingAnts.Add(antObject, ant);
                //todo move ant animation
                // antObject.GetComponent<AntScript>().Go(ant.Row, ant.Col, ant.Health, ant.Resource,UIManager.Instance.BaseTime/2);
            }
            else
            {
                //new ants
                NewAnts.Add(ant.Id, ant);
                // antObject = Instantiate(antPrefab);
                // antObject.GetComponent<AntScript>().Set(ant.Row, ant.Col, ant.Team, ant.Type, ant.Health, ant.Resource);
            }
        }

        foreach (DictionaryEntry antDE in cloneAntTable)
        {
            //dead ants
            AntScript antScript = (AntScript) (antDE.Value);
            StartCoroutine(antScript.die(baseTime / 2));
        }

            foreach (DictionaryEntry antDE in NewAnts)
            {
                //new ants
                Ant antObject = (Ant) antDE.Value;
                GameObject ant;
                ant = Instantiate(antPrefab);
                ant.GetComponent<AntScript>().Set(antObject.Row, antObject.Col, antObject.Team, antObject.Type,
                    antObject.Health, antObject.Resource);
                AntsTable.Add(antObject.Id, ant);
            }

            Debug.Log("end phase1");
            yield return new WaitForSeconds(baseTime / 2);
            if (playAnime)
            {
                //move ants time
                Debug.Log("start phase2");
                foreach (DictionaryEntry antDE in MovingAnts)
                {
                    Ant ant = (Ant) antDE.Value;
                    GameObject antScript = (GameObject) antDE.Key;
                    StartCoroutine(antScript.GetComponent<AntScript>()
                        .Go(ant.Row, ant.Col, ant.Health, ant.Resource, baseTime / 2));
                }

                Debug.Log("end phase2");
            }
        }
    }

    private void ApplyResources(int[][] resources, GameObject mainRec)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            for (int j = 0; j < resources[0].Length; j++)
            {
                if (resources[i][j] > 0)
                {
                    GameObject rec = InstansiateCell(mainRec, i, j);
                    rec.GetComponent<RecourceScript>().SetAmount(resources[i][j]);
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
            ConvertPosition(i, j),
            Quaternion.identity);
    }

    public Vector3 ConvertPosition(int x, int y)
    {
        return new Vector3(x0 + x * width, y0 - y * haight, 1);
    }
}