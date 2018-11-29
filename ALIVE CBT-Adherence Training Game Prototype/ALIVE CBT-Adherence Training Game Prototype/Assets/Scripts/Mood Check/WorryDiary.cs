using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorryDiary : MonoBehaviour, IMoodCheckActivity_Generic
{

    public string[] situationQuestions;
    public string[] challengeThoughtsQuestions;

    public MoodCheckManager moodCheckManager;
    public Text question;
    public InputField answerInput;

    private QuestionType questionType;
    private int challengeThoughtsQuestionsCount = 0;
    private WorryDiaryInfo worryDiaryInfo;

    // Use this for initialization
    void Start()
    {
        // Set the question type to the first type, which is situation
        questionType = QuestionType.Situation;

        // Setting up Mood Diary Info 
        worryDiaryInfo = new WorryDiaryInfo();
        worryDiaryInfo.Question_ChallengeThoughts = new string[challengeThoughtsQuestions.Length];
        worryDiaryInfo.Answer_ChallengeThoughts = new string[challengeThoughtsQuestions.Length];

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
        switch (questionType)
        {
            case QuestionType.Situation:
                randNum = Random.Range(0, situationQuestions.Length);
                worryDiaryInfo.Question_Situation = question.text = situationQuestions[randNum];
                break;
            case QuestionType.ChallengeThoughts:
                if (challengeThoughtsQuestionsCount < challengeThoughtsQuestions.Length)
                {
                    worryDiaryInfo.Question_ChallengeThoughts[challengeThoughtsQuestionsCount] = question.text
                        = challengeThoughtsQuestions[challengeThoughtsQuestionsCount];
                    challengeThoughtsQuestionsCount++;
                }
                break;
        }
    }

    public void Back()
    {
        switch (questionType)
        {
            case QuestionType.Situation:
                Reset();
                moodCheckManager.OpenActivitySelection();
                break;
            case QuestionType.ChallengeThoughts:
                if (challengeThoughtsQuestionsCount == 1)
                {
                    worryDiaryInfo.Answer_ChallengeThoughts[0] = answerInput.text;
                    answerInput.text = worryDiaryInfo.Answer_Situation;
                    questionType = QuestionType.Situation;
                    challengeThoughtsQuestionsCount = 0;
                    QuestionGenerator();
                }
                else if (challengeThoughtsQuestionsCount > 1)
                {
                    challengeThoughtsQuestionsCount -= 2;
                    worryDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount + 1] = answerInput.text;
                    answerInput.text = worryDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount];
                    QuestionGenerator();
                }
                break;
        }
    }

    public void Next()
    {
        switch (questionType)
        {
            case QuestionType.Situation:
                // Set next question type
                questionType = QuestionType.ChallengeThoughts;
                // Add the player's input to the mood diary info
                worryDiaryInfo.Answer_Situation = answerInput.text;
                // Set the input text as next question's answer
                answerInput.text = worryDiaryInfo.Answer_ChallengeThoughts[0];
                QuestionGenerator();
                break;
            case QuestionType.ChallengeThoughts:
                if (challengeThoughtsQuestionsCount >= challengeThoughtsQuestions.Length)
                {
                    // Add the player's input to the mood diary info
                    worryDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount - 1] = answerInput.text;
                    Save();
                    moodCheckManager.OpenActivitySelection();
                    moodCheckManager.activitySelection.GetComponent<ActivitySelection>().RemoveActivity(2);
                }
                else
                {
                    // Add the player's input to the mood diary info
                    worryDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount - 1] = answerInput.text;
                    // Set the input text as next question's answer
                    answerInput.text = worryDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount];
                    QuestionGenerator();
                }
                break;
        }
    }

    public void Save()
    {
        moodCheckManager.moodCheckInfo.worryDiaryInfo = worryDiaryInfo;
        moodCheckManager.moodCheckInfo.worryDiaryActive = true;
    }

    public void Reset()
    {
        worryDiaryInfo.Answer_Situation = "";
        worryDiaryInfo.Question_Situation = "";

        for (int i = 0; i < worryDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            worryDiaryInfo.Answer_ChallengeThoughts[i] = "";
            worryDiaryInfo.Question_ChallengeThoughts[i] = "";
        }

        questionType = QuestionType.Situation;
        challengeThoughtsQuestionsCount = 0;
        answerInput.text = "";
    }

}

