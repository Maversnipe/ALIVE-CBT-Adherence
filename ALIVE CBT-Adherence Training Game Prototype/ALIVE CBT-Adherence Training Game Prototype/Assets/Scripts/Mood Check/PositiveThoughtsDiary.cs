using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositiveThoughtsDiary : MonoBehaviour {
    public string[] situationQuestions;

    public MoodCheckManager moodCheckManager;
    public Text question;
    public InputField answerInput;

    private PositiveThoughtsJournalInfo positiveThoughtsJournalInfo;

    // Use this for initialization
    void Start()
    {
        // Setting up Mood Diary Info 
        positiveThoughtsJournalInfo = new PositiveThoughtsJournalInfo();

        QuestionGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (answerInput.text == "")
        {
            moodCheckManager.next.interactable = false;
        }
        else
        {
            moodCheckManager.next.interactable = true;
        }
    }

    public void QuestionGenerator()
    {
        int randNum;
        randNum = Random.Range(0, situationQuestions.Length);
        positiveThoughtsJournalInfo.Question_Situation = question.text = situationQuestions[randNum];
    }

    public void Back()
    {
        Reset();
        moodCheckManager.OpenActivitySelection();
    }

    public void Next()
    {
        // Add the player's input to the mood diary info
        positiveThoughtsJournalInfo.Answer_Situation = answerInput.text;
        Save();
        moodCheckManager.OpenActivitySelection();
        moodCheckManager.activitySelection.GetComponent<ActivitySelection>().RemoveActivity(1);
    }

    public void Save()
    {
        moodCheckManager.moodCheckInfo.posThoughtsJournalInfo = positiveThoughtsJournalInfo;
        moodCheckManager.moodCheckInfo.posThoughtsJournalActive = true;
    }

    public void Reset()
    {
        positiveThoughtsJournalInfo.Answer_Situation = "";
        positiveThoughtsJournalInfo.Question_Situation = "";
        
        answerInput.text = "";
    }

}
