
public class Attack
{
    public int AttackerId { get; }
    public int DefenderId { get; }
    public int SrcRow { get; }
    public int SrcCol { get; }
    public int DstRow { get; }
    public int DstCol { get; }

    public Attack(int attackerId, int defenderId, int srcRow, int srcCol, int dstRow, int dstCol)
    {
        AttackerId = attackerId;
        DefenderId = defenderId;
        SrcRow = srcRow;
        SrcCol = srcCol;
        DstRow = dstRow;
        DstCol = dstCol;
    }
}
