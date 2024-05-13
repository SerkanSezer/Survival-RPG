[System.Serializable]
public class ResponseNode
{
    public string responseText;
    public int toDialogue;
    public ResponseType responseType;
}
public enum ResponseType {
    ToDialogue,
    ToAction,
    ToFinish
}

