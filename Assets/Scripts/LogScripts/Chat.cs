public class Chat
{
    private string text;
    private int value;
    private int senderID;

    public Chat(string text, int value, int senderID)
    {
        this.text = text;
        this.value = value;
        this.senderID = senderID;
    }

    public override string ToString()
    {
        return senderID + ": " + text + "(" + value + ")";
    }
}