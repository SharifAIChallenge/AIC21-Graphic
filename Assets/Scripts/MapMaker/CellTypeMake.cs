using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CellTypeMake : MonoBehaviour
{
    private Image Sprite;
    [SerializeField] private Toggle isRes;
    public MapMaker MapMaker;
    [SerializeField] private Sprite wall;
    [SerializeField] private Sprite empty;
    [SerializeField] private Sprite base1;
    [SerializeField] private Sprite base2;
    [HideInInspector] public int row;
    [HideInInspector] public int col;
    [HideInInspector] public int cell_type;
    [HideInInspector] public int rec1;
    [HideInInspector] public int rec2;
    public Text res1Text;
    public Text res2Text;

    private void Awake()
    {
        Sprite = gameObject.GetComponent<Image>();
        isRes = GameObject.FindWithTag("Finish").GetComponent<Toggle>();
        MapMaker = GameObject.FindWithTag("MainCamera").GetComponent<MapMaker>();
    }

    public void SetCellTypeMake(
        int row,
        int col,
        int cell_type,
        int rec1,
        int rec2
    )
    {
        this.row = row;
        this.col = col;
        this.cell_type = cell_type;
        this.rec1 = rec1;
        this.rec2 = rec2;
        transform.position = new Vector3(col * 2, -row * 2, 1);
        switch (cell_type)
        {
            case 0:
                Sprite.sprite = base1;
                break;
            case 1:
                Sprite.sprite = base2;
                break;
            case 2:
                Sprite.sprite = empty;
                break;
            case 3:
                Sprite.sprite = wall;
                break;
        }

        res1Text.text = rec1.ToString();
        res2Text.text = rec2.ToString();
    }

    public void OnClick()
    {
        if (!isRes.isOn)
        {
            Vector2 sizeTemp = new Vector2();
            switch (cell_type)
            {
                case 0:
                    cell_type = 1;
                    Sprite.sprite = base2;
                    break;
                case 1:
                    cell_type = 2;
                    Sprite.sprite = empty;
                    break;
                case 2:
                    cell_type = 3;
                    Debug.Log("case 2");
                    Sprite.sprite = wall;
                    break;
                case 3:
                    cell_type = 0;
                    Sprite.sprite = base1;
                    break;
            }

            MapMaker.changeCell(row, col, cell_type);
        }
        else
        {
            MapMaker.setRes(row, col, this.gameObject);
        }
    }
}