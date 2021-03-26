using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    public GameObject InGame;
    public GameObject GameLogBrowser;

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
    public int MaxTurns { get; private set; }
    private float baseTime;
    [HideInInspector] private bool playAnime;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartGameManager(GameLog gameLog)
    {
        GameLogBrowser.SetActive(false);
        InGame.SetActive(true);

        baseTime = UIManager.Instance.BaseTime;
        this.gameLog = gameLog;
        base1.GetComponent<BaseScript>().SetMaxHealth(gameLog.Map.BaseHealth);
        base2.GetComponent<BaseScript>().SetMaxHealth(gameLog.Map.BaseHealth);
        ShowMap();
        FindObjectOfType<MoveCamera>().setMaid(gameLog.Map.cells.Length * width, gameLog.Map.cells[0].Length * haight);
        MaxTurns = gameLog.Turns.Length;
    }

    public void ApplyLog(int turn, bool isAnim)
    {
        Debug.Log(currTurn + "    " + turn);
        if (currTurn == turn - 1 && isAnim)
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
    }

    private IEnumerator ApplyTurnAnim(Turn turn)
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

        if (playAnime)
        {
            // Debug.Log("start phase1");
            foreach (DictionaryEntry antDE in NewAnts)
            {
                //new ants
                Ant antObject = (Ant) antDE.Value;
                GameObject ant;
                int numbers = (int) Mathf.Pow(Mathf.Ceil(Mathf.Pow(turn.CellAnts[antObject.Row][antObject.Col].Count, 0.5f)), 2);
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
                GameObject attacker = (GameObject) AntsTable[attack.AttackerId];
                attacker.GetComponent<AntScript>().Attack(attack.DstRow, attack.DstCol, baseTime / 2);
            }

            foreach (DictionaryEntry antDE in cloneAntTable)
            {
                //dead ants
                GameObject antScript = (GameObject) (antDE.Value);
                AntsTable.Remove(antDE.Key);
                StartCoroutine(antScript.GetComponent<AntScript>().die(baseTime / 4, baseTime / 4));
            }


            // Debug.Log("end phase1");
            yield return new WaitForSecondsRealtime((baseTime / 2) * UIManager.Instance.Speed);
            if (playAnime)
            {
                //move ants time
                // Debug.Log("start phase2");
                foreach (DictionaryEntry antDE in MovingAnts)
                {
                    Ant ant = (Ant) antDE.Value;
                    int numbers = (int) Mathf.Pow(Mathf.Ceil(Mathf.Pow(turn.CellAnts[ant.Row][ant.Col].Count, 0.5f)), 2);
                    int n = turn.CellAnts[ant.Row][ant.Col].IndexOf(ant.Id) + 1;
                    GameObject antScript = (GameObject) antDE.Key;
                    StartCoroutine(antScript.GetComponent<AntScript>()
                        .Go(ant.Row, ant.Col, ant.Health, ant.Resource, baseTime / 2, numbers, n));
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