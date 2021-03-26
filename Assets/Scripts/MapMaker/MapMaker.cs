using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MapMaker : MonoBehaviour
{
    [SerializeField] private InputField res1Input;
    [SerializeField] private InputField res2Input;
    [SerializeField] private InputField widthInput;
    [SerializeField] private InputField heightInput;
    [SerializeField] private Canvas canvas;
     private int width;
     private int height;
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject res1Prefab;
    [SerializeField] private GameObject res2Prefab;
    int[][] map = new int[0][];
    int[][] res1 = new int[21][];
    int[][] res2 = new int[21][];
    GameObject[][] Cells = new GameObject[0][];


    // Start is called before the first frame update
    void Start()
    {
        // int[][] map = new int[21][];
        // map[0] = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
        // map[0] = new[] {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3};
        // map[1] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[2] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[3] = new[] {3, 2, 2, 3, 3, 3, 3, 3, 3, 2, 2, 2, 3, 3, 3, 3, 3, 3, 2, 2, 3};
        // map[4] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[5] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[6] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[7] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[8] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[9] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[10] = new[] {3, 2, 2, 2, 0, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 1, 2, 2, 3};
        // map[11] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[12] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[13] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[14] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[15] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[16] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[17] = new[] {3, 2, 2, 3, 3, 3, 3, 3, 3, 2, 2, 2, 3, 3, 3, 3, 3, 3, 2, 2, 3};
        // map[18] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[19] = new[] {3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3};
        // map[20] = new[] {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3};
        // int[][] res1 = new int[21][];
        //
        // res1[0] = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
        // res1[0] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[1] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[2] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[3] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[4] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[5] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[6] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[7] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[8] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[9] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[10] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[11] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[12] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[13] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[14] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[15] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[16] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[17] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[18] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[19] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res1[20] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // int[][] res2 = new int[21][];
        // res2[0] = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
        // res2[0] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[1] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[2] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[3] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[4] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[5] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[6] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[7] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[8] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[9] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[10] = new[] {0, 10, 0, 0, 0, 0, 0, 0, 0, 12, 0, 12, 0, 0, 0, 0, 0, 0, 0, 10, 0};
        // res2[11] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[12] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[13] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[14] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[15] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[16] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[17] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[18] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[19] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // res2[20] = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        // CellTypeMake[] cellTypeMake = new CellTypeMake [21 * 21];
        // for (int i = 0; i < 21; i++)
        // {
        //     for (int j = 0; j < 21; j++)
        //     {
        //         cellTypeMake[i * 21 + j] = new CellTypeMake(i, j, map[i][j], res1[i][j], res2[i][j]);
        //     }
        // }
        //
        // GameConfigMake gameConfigMake = new GameConfigMake();
        // gameConfigMake.cells_type = cellTypeMake;
        // string json = JsonUtility.ToJson(gameConfigMake);
        // Debug.Log(json);
        
    }

    public void changeCell(int row, int col, int type)
    {
        map[col][row] = type;
    }

    public void setRes(int row, int col, GameObject cell)
    {
        int res1Amount = 0;
        int res2Amount = 0;
        try
        {
            res1Amount = Convert.ToInt32(res1Input.text);
            res2Amount = Convert.ToInt32(res2Input.text);
        }
        catch (Exception e)
        {
        }

        res1[row][col] = res1Amount;
        res2[row][col] = res2Amount;
        cell.GetComponent<CellTypeMake>().res1Text.text = res1Amount.ToString();
        cell.GetComponent<CellTypeMake>().res2Text.text = res2Amount.ToString();
    }

    public void ExportMap()
    {
        CellTypeMakeJson[] cellTypeMake = new CellTypeMakeJson [width * height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                cellTypeMake[i * width + j] = new CellTypeMakeJson(i,j, map[i][j], res1[i][j], res2[i][j]);
            }
        }

        GameConfigMake gameConfigMake = new GameConfigMake();
        gameConfigMake.cells_type = cellTypeMake;
        string json = JsonUtility.ToJson(gameConfigMake);
        Debug.Log(json);
        StreamWriter writer = new StreamWriter("map1.json", false);
        writer.Write(json);
        writer.Close();
    }

    public void StartMapMaker()
    {
        width = Convert.ToInt32(widthInput.text);
        height = Convert.ToInt32(heightInput.text);
        map = new int[height][];
        res1 = new int[height][];
        res2 = new int[height][];
        Cells = new GameObject[height][];
        for (int i = 0; i < height; i++)
        {
            map[i] = new int[width];
            res1[i] = new int[width];
            res2[i] = new int[width];
            Cells[i] = new GameObject[width];
            for (int j = 0; j < width; j++)
            {
                map[i][j] = 2;
                GameObject cellObj = Instantiate(cell);
                cellObj.GetComponent<CellTypeMake>().SetCellTypeMake(j, i, 2, 0, 0);
                cellObj.transform.SetParent(canvas.transform);
                cellObj.transform.position = new Vector3(j * 4, -i * 4);
                Cells[i][j] = cellObj;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}