using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMoodCheckActivity_Generic {

   void Back();
   void Next();
   void Save();
   void Reset();
}

public enum QuestionType
{
    None,
    Situation,
    PhysicalSensations,
    UnhelpfulThoughts,
    ChallengeThoughts,
}

[Serializable]
public class QuestionAndResponse
{
    public QuestionAndResponse()
    {

    }

    public QuestionAndResponse(string _question, string _response)
    {
        this.Question = _question;
        this.Response = _response;
    }

    public string Question;
    public string Response;
}

[Serializable]
[CreateAssetMenu(fileName = "Dialogue_Empty", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueNode : ScriptableObject
{
    public DialogueType dialogueType;
    public QuestionType questionType;

    public DialogueNode nextNode;
    public DialogueNode prevNode;

    public string content;

    public enum DialogueType
    {
        Question,
        Response
    }

    public DialogueNode()
    {
        this.nextNode = null;
        this.prevNode = null;
        this.dialogueType = DialogueType.Question;
        this.questionType = QuestionType.None;
    }
}
