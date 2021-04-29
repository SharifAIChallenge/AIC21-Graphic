using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CellTypeMakeJson
{
    public int row;
    public int col;
    public int cell_type;
    public int rec1;
    public int rec2;
    public CellTypeMakeJson(
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
    }

    public override bool Equals(object obj)
    {
        CellTypeMakeJson ce = (CellTypeMakeJson) obj;
        return this.row == ce.row && this.col == ce.col;
    }
}