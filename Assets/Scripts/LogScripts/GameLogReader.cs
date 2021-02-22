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
        cells[0] = new[] {3, 2, 1};
        cells[1] = new[] {2, 1, 2};
        cells[2] = new[] {1, 2, 4};
        int[][] recources1 = new int[3][];
        recources1[0] = new[] {0, 0, 5};
        recources1[1] = new[] {0, 0, 0};
        recources1[2] = new[] {0, 0, 0};
        int[][] recources2 = new int[3][];
        recources2[0] = new[] {0, 0, 0};
        recources2[1] = new[] {0, 0, 0};
        recources2[2] = new[] {3, 0, 0};
        Turn[] turns = new Turn[1];
        turns[0] = new Turn(4,6,recources1,recources2);
        GameLog = new GameLog(new Map(cells), turns);
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