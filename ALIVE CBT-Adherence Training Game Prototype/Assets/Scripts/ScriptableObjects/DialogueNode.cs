using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue_Empty", menuName = "ScriptableObjects/DialogueNode", order = 1)]
public class DialogueNode : ScriptableObject
{
    public DialogueType dialogueType;
    public QuestionType questionType;

    public DialogueNode nextNode;
    public DialogueNode prevNode;

    public string[] content = new string[1];

    public enum DialogueType
    {
        Question,
        Response
    }

    public int index;
}
