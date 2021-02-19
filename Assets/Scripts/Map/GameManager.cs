public class GameManager
{
    private NewBehaviourScript gameLog;   //Todo change class to GameLog(It's not created yet)
    private int currTurn;

    public GameManager(NewBehaviourScript gameLog)
    {
        this.gameLog = gameLog;
    }

    public void applyLog(int turn)
    {
        this.currTurn = turn;
        // Todo
    }
}
