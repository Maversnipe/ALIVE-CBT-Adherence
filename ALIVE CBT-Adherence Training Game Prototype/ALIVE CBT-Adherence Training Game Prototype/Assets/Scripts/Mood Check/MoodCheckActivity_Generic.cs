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
