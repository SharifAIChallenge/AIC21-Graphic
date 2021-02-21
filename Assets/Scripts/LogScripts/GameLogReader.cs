using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogReader : MonoBehaviour
{
    public GameLog GameLog { get; private set; }
    private void Awake()
    {
        //hardcoded GameLog for Test
        int[][] cells = new int[3][];
        cells[0] = new[] {1, 2, 1};
        cells[1] = new[] {2, 1, 2};
        cells[2] = new[] {1, 2, 1};
        GameLog = new GameLog(new Map(cells), null);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().StartGameManager(GameLog);
        // GameManager.ga
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}