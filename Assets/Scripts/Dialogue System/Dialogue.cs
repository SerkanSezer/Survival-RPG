using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Dialogue",menuName ="ScriptableObjects/Dialogue",order =4)]
public class Dialogue : ScriptableObject
{
    public List<DialogueNode> dialogueNodes;
}
