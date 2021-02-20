public class GameManager
{
    private GameLog gameLog;   //Todo change class to GameLog(It's not created yet)
    private int currTurn;

    public GameManager(GameLog gameLog)
    {
        this.gameLog = gameLog;
    }

    public void applyLog(int turn)
    {
        this.currTurn = turn;
        // Todo
    }
}
