using System;
using System.Collections.Generic;

[Serializable]
public class DialogueNode
{
    public int dialogueIndex;
    public string dialogueText;
    public List<ResponseNode> responseNodes;
}
