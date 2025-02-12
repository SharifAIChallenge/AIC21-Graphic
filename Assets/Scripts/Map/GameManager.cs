﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    public GameObject InGame;
    public GameObject GameLogBrowser;
    private int shift_x;
    private int shift_y;
    [SerializeField] private GameObject ruler;
    private bool isRuler = true;
    private bool skipAnim = false;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private GameObject antPrefab;
    [SerializeField] private GameObject cell_empty;
    [SerializeField] private GameObject cell_wall;
    [SerializeField] private GameObject cell_glue;
    [SerializeField] private GameObject cell_mud;
    [SerializeField] private GameObject base1;
    [SerializeField] private GameObject base2;
    [SerializeField] private GameObject rec1;
    [SerializeField] private GameObject rec2;
    private GameLog gameLog;
    private int mapWidth;
    private int mapHeight;
    private int currTurn;
    public int x0;
    public int y0;
    public int width;
    public int haight;
    private List<GameObject> Temps = new List<GameObject>();
    private Hashtable AntsTable = new Hashtable();
    [SerializeField] private TextMeshProUGUI team0_alive_workers;
    [SerializeField] private TextMeshProUGUI team0_alive_soldiers;
    [SerializeField] private TextMeshProUGUI team0_total_resource0;
    [SerializeField] private TextMeshProUGUI team0_total_resource1;
    [SerializeField] private TextMeshProUGUI team1_alive_workers;
    [SerializeField] private TextMeshProUGUI team1_alive_soldiers;
    [SerializeField] private TextMeshProUGUI team1_total_resource0;
    [SerializeField] private TextMeshProUGUI team1_total_resource1;
    [SerializeField] private TextMeshProUGUI team0_name;
    [SerializeField] private TextMeshProUGUI team1_name;
    [SerializeField] private GameObject RightStats;
    [SerializeField] private GameObject LeftStats;
    public int MaxTurns { get; private set; }
    private float baseTime;
    [HideInInspector] private bool playAnime;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 50;
        Instance = this;
    }

    public void StartGameManager(GameLog gameLog)
    {
        shift_x = gameLog.Map.ShiftX;
        shift_y = gameLog.Map.ShiftY;
        GameLogBrowser.SetActive(false);
        InGame.SetActive(true);
        mapHeight = gameLog.Map.cells.Length;
        mapWidth = gameLog.Map.cells[0].Length;
        winnerPanel.SetActive(false);
        baseTime = UIManager.Instance.BaseTime;
        this.gameLog = gameLog;
        base1.GetComponent<BaseScript>().SetMaxHealth(gameLog.Map.BaseHealth);
        base2.GetComponent<BaseScript>().SetMaxHealth(gameLog.Map.BaseHealth);
        ShowMap();
        FindObjectOfType<MoveCamera>().setMaid(gameLog.Map.cells.Length * width, gameLog.Map.cells[0].Length * haight);
        MaxTurns = gameLog.Turns.Length;
        if (gameLog.Map.WinnerTeam == 0)
        {
            winnerText.text = gameLog.Map.Team0Name;
        }
        else
        {
            winnerText.text = gameLog.Map.Team1Name;
        }
    }

    public void ApplyLog(int turn, bool isAnim)
    {
        Debug.Log(currTurn + "    " + turn);
        if (currTurn == turn - 1 && isAnim)
        {
            if (skipAnim)
            {
                ApplyTurnUnAnim(gameLog.Turns[turn - 1], turn == MaxTurns);
                skipAnim = false;
            }
            else
            {
                playAnime = true;
                StartCoroutine(ApplyTurnAnim(gameLog.Turns[turn - 1], turn == MaxTurns));
            }
        }
        else
        {
            playAnime = false;
            ApplyTurnUnAnim(gameLog.Turns[turn - 1], turn == MaxTurns);
        }

        currTurn = turn;
    }


    private void ApplyTurnUnAnim(Turn turn, bool lastTurn)
    {
        team0_alive_workers.text = turn.team0_alive_workers.ToString();
        team0_alive_soldiers.text = turn.team0_alive_soldiers.ToString();
        team0_total_resource0.text = turn.team0_total_resource0.ToString();
        team0_total_resource1.text = turn.team0_total_resource1.ToString();
        team1_alive_workers.text = turn.team1_alive_workers.ToString();
        team1_alive_soldiers.text = turn.team1_alive_soldiers.ToString();
        team1_total_resource0.text = turn.team1_total_resource0.ToString();
        team1_total_resource1.text = turn.team1_total_resource1.ToString();
        ChatManager.Instance.SetLeftChatMessages(turn.ImportantChatBox0, turn.TrivialChatBox0);
        ChatManager.Instance.SetRightChatMessages(turn.ImportantChatBox1, turn.TrivialChatBox1);
        Debug.Log("unAnim move " + currTurn);
        foreach (GameObject temp in Temps)
        {
            Destroy(temp);
        }

        ShowMap();

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
            int numbers = (int) Mathf.Pow(Mathf.Ceil(Mathf.Pow(turn.CellAnts[ant.Row][ant.Col].Count, 0.5f)), 2);
            int n = turn.CellAnts[ant.Row][ant.Col].IndexOf(ant.Id) + 1;
            GameObject antObject = Instantiate(antPrefab);
            AntScript antScript = antObject.GetComponent<AntScript>();
            antScript.SetMaxHealth(ant.Type == Ant.WORKER ? gameLog.Map.WorkerHealth : gameLog.Map.SoldierHealth);
            antScript.Set(ant.Row, ant.Col, ant.Team, ant.Type, ant.Health, ant.Resource, ant.Id, numbers, n);
            AntsTable.Add(ant.Id, antObject);
        }

        if (lastTurn)
        {
            winnerPanel.SetActive(true);
        }
        else
        {
            winnerPanel.SetActive(false);
        }
    }

    private IEnumerator ApplyTurnAnim(Turn turn, bool lastTurn)
    {
        team0_alive_workers.text = turn.team0_alive_workers.ToString();
        team0_alive_soldiers.text = turn.team0_alive_soldiers.ToString();
        team0_total_resource0.text = turn.team0_total_resource0.ToString();
        team0_total_resource1.text = turn.team0_total_resource1.ToString();
        team1_alive_workers.text = turn.team1_alive_workers.ToString();
        team1_alive_soldiers.text = turn.team1_alive_soldiers.ToString();
        team1_total_resource0.text = turn.team1_total_resource0.ToString();
        team1_total_resource1.text = turn.team1_total_resource1.ToString();
        //attack and dead and get recource and set recource and set base healthes
        ChatManager.Instance.SetLeftChatMessages(turn.ImportantChatBox0, turn.TrivialChatBox0);
        ChatManager.Instance.SetRightChatMessages(turn.ImportantChatBox1, turn.TrivialChatBox1);
        // Debug.Log("anim move " + currTurn);
        foreach (GameObject temp in Temps)
        {
            Destroy(temp);
        }

        ShowMap();

        base1.GetComponent<BaseScript>().setHealth(turn.Base0Health);
        base2.GetComponent<BaseScript>().setHealth(turn.Base1Health);
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

        if (playAnime)
        {
            // Debug.Log("start phase1");
            foreach (DictionaryEntry antDE in NewAnts)
            {
                //new ants
                Ant antObject = (Ant) antDE.Value;
                GameObject ant;
                int numbers =
                    (int) Mathf.Pow(Mathf.Ceil(Mathf.Pow(turn.CellAnts[antObject.Row][antObject.Col].Count, 0.5f)), 2);
                int n = turn.CellAnts[antObject.Row][antObject.Col].IndexOf(antObject.Id) + 1;
                ant = Instantiate(antPrefab);
                AntScript antScript = ant.GetComponent<AntScript>();
                antScript.SetMaxHealth(antObject.Type == Ant.WORKER
                    ? gameLog.Map.WorkerHealth
                    : gameLog.Map.SoldierHealth);
                antScript.Set(antObject.Row, antObject.Col, antObject.Team, antObject.Type,
                    antObject.Health, antObject.Resource, antObject.Id, numbers, n);
                AntsTable.Add(antObject.Id, ant);
            }

            foreach (Attack attack in turn.Attacks)
            {
                if (attack.AttackerId == -1)
                {
                    base1.GetComponent<BaseScript>().Attack(attack.SrcRow, attack.SrcCol, attack.DstRow, attack.DstCol,
                        baseTime / 3);
                }

                if (attack.AttackerId == -2)
                {
                    base2.GetComponent<BaseScript>().Attack(attack.SrcRow, attack.SrcCol, attack.DstRow, attack.DstCol,
                        baseTime / 3);
                }
                else
                {
                    GameObject attacker = (GameObject) AntsTable[attack.AttackerId];
                    try
                    {
                        attacker.GetComponent<AntScript>().Attack(attack.DstRow, attack.DstCol, baseTime / 3);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(attack.AttackerId + " " + attack.DefenderId);
                    }
                }
            }

            foreach (DictionaryEntry antDE in cloneAntTable)
            {
                //dead ants
                GameObject antScript = (GameObject) (antDE.Value);
                AntsTable.Remove(antDE.Key);
                StartCoroutine(antScript.GetComponent<AntScript>().die(baseTime / 4, baseTime / 4));
            }

            
            // Debug.Log("end phase1");
            ApplyResources(turn.Resources0, rec1);
            ApplyResources(turn.Resources1, rec2);
            yield return new WaitForSecondsRealtime((baseTime / 3) * UIManager.Instance.Speed);
            // yield return null;
            if (lastTurn)
            {
                winnerPanel.SetActive(true);
            }
            else
            {
                winnerPanel.SetActive(false);
            }

            if (playAnime)
            {
                Debug.Log("move ant");
                //move ants time
                // Debug.Log("start phase2");
                foreach (DictionaryEntry antDE in MovingAnts)
                {
                    Ant ant = (Ant) antDE.Value;
                    int numbers = (int) Mathf.Pow(Mathf.Ceil(Mathf.Pow(turn.CellAnts[ant.Row][ant.Col].Count, 0.5f)),
                        2);
                    int n = turn.CellAnts[ant.Row][ant.Col].IndexOf(ant.Id) + 1;
                    GameObject antScript = (GameObject) antDE.Key;
                    StartCoroutine(antScript.GetComponent<AntScript>()
                        .Go(ant.Row, ant.Col, ant.Health, ant.Resource, baseTime*2 / 3, numbers, n));
                }

                // Debug.Log("end phase2");
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
        team0_name.text = gameLog.Map.Team0Name;
        team1_name.text = gameLog.Map.Team1Name;
        for (int i = 0; i < gameLog.Map.cells.Length; i++)
        {
            for (int j = 0; j < gameLog.Map.cells[0].Length; j++)
            {
                switch (gameLog.Map.cells[i][j])
                {
                    case 2:
                        Temps.Add(InstansiateCell(cell_empty, i, j));
                        break;
                    case 3:
                        Temps.Add(InstansiateCell(cell_wall, i, j));
                        break;
                    case 4:
                        Temps.Add(InstansiateCell(cell_glue, i, j));
                        break;
                    case 5:
                        Temps.Add(InstansiateCell(cell_mud, i, j));
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
        if (isRuler)
        {
            return new Vector3(x0 + ((x + shift_x) % mapHeight) * width, y0 - ((y + shift_y) % mapWidth) * haight, 1);
        }
        else
        {
            return new Vector3(x0 + ((x) % mapHeight) * width, y0 - ((y) % mapWidth) * haight, 1);
        }
    }

    public void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void RightStatsClicked()
    {
        RightStats.SetActive(!RightStats.activeSelf);
    }

    public void LeftStatsClicked()
    {
        LeftStats.SetActive(!LeftStats.activeSelf);
    }

    public void ApplyRuler()
    {
        if (isRuler)
        {
            skipAnim = true;
            isRuler = false;
            var color = ruler.GetComponent<Image>().color;
            ruler.GetComponent<Image>().color = new Color(color.r,color.g,color.b,50);
        }
        else
        {
            skipAnim = true;
            isRuler = true;
            var color = ruler.GetComponent<Image>().color;
            color.a = 200;
        }
    }
}