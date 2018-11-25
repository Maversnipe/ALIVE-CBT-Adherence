using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngerDiary : MonoBehaviour, IMoodCheckActivity_Generic
{

    public string[] situationQuestions;
    public string[] physicalSensationQuestions;
    public string[] unhelpfulThoughtsQuestions;
    public string[] challengeThoughtsQuestions;

    public MoodCheckManager moodCheckManager;
    public Text question;
    public InputField answerInput;

    private QuestionType questionType;
    private int situationQuestionsCount = 0;
    private int challengeThoughtsQuestionsCount = 0;
    private AngerDiaryInfo angerDiaryInfo;

    // Use this for initialization
    void Start()
    {
        // Set the question type to the first type, which is situation
        questionType = QuestionType.Situation;

        // Setting up Mood Diary Info 
        angerDiaryInfo = new AngerDiaryInfo();
        angerDiaryInfo.Question_Situation = new string[situationQuestions.Length];
        angerDiaryInfo.Answer_Situation = new string[situationQuestions.Length];
        angerDiaryInfo.Question_ChallengeThoughts = new string[challengeThoughtsQuestions.Length];
        angerDiaryInfo.Answer_ChallengeThoughts = new string[challengeThoughtsQuestions.Length];

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
                if (situationQuestionsCount < situationQuestions.Length)
                {
                    angerDiaryInfo.Question_Situation[situationQuestionsCount] = question.text
                        = situationQuestions[situationQuestionsCount];
                    situationQuestionsCount++;
                }
                break;
            case QuestionType.PhysicalSensations:
                randNum = Random.Range(0, physicalSensationQuestions.Length);
                angerDiaryInfo.Question_PhysicalSensation = question.text = physicalSensationQuestions[randNum];
                break;
            case QuestionType.UnhelpfulThoughts:
                randNum = Random.Range(0, unhelpfulThoughtsQuestions.Length);
                angerDiaryInfo.Question_UnhelpfulThoughts = question.text = unhelpfulThoughtsQuestions[randNum];
                break;
            case QuestionType.ChallengeThoughts:
                if (challengeThoughtsQuestionsCount < challengeThoughtsQuestions.Length)
                {
                    angerDiaryInfo.Question_ChallengeThoughts[challengeThoughtsQuestionsCount] = question.text
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
                if (situationQuestionsCount == 1)
                {
                    Reset();
                    moodCheckManager.OpenActivitySelection();
                }
                else if (situationQuestionsCount > 1)
                {
                    situationQuestionsCount -= 2;
                    angerDiaryInfo.Answer_Situation[situationQuestionsCount + 1] = answerInput.text;
                    answerInput.text = angerDiaryInfo.Answer_Situation[situationQuestionsCount];
                    QuestionGenerator();
                }
                break;
            case QuestionType.PhysicalSensations:
                situationQuestionsCount--;
                angerDiaryInfo.Answer_PhysicalSensation = answerInput.text;
                answerInput.text = angerDiaryInfo.Answer_Situation[situationQuestionsCount];
                questionType = QuestionType.Situation;
                QuestionGenerator();
                break;
            case QuestionType.UnhelpfulThoughts:
                angerDiaryInfo.Answer_UnhelpfulThoughts = answerInput.text;
                answerInput.text = angerDiaryInfo.Answer_PhysicalSensation;
                questionType = QuestionType.PhysicalSensations;
                QuestionGenerator();
                break;
            case QuestionType.ChallengeThoughts:
                if (challengeThoughtsQuestionsCount == 1)
                {
                    angerDiaryInfo.Answer_ChallengeThoughts[0] = answerInput.text;
                    answerInput.text = angerDiaryInfo.Answer_UnhelpfulThoughts;
                    questionType = QuestionType.UnhelpfulThoughts;
                    challengeThoughtsQuestionsCount = 0;
                    QuestionGenerator();
                }
                else if (challengeThoughtsQuestionsCount > 1)
                {
                    challengeThoughtsQuestionsCount -= 2;
                    angerDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount + 1] = answerInput.text;
                    answerInput.text = angerDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount];
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
                if (situationQuestionsCount >= situationQuestions.Length)
                {
                    // Set next question type
                    questionType = QuestionType.PhysicalSensations;
                    // Add the player's input to the mood diary info
                    angerDiaryInfo.Answer_Situation[situationQuestionsCount - 1] = answerInput.text;
                    // Set the input text as next question's answer
                    answerInput.text = angerDiaryInfo.Answer_PhysicalSensation;
                    QuestionGenerator();
                }
                else
                {
                    // Add the player's input to the mood diary info
                    angerDiaryInfo.Answer_Situation[situationQuestionsCount - 1] = answerInput.text;
                    // Set the input text as next question's answer
                    answerInput.text = angerDiaryInfo.Answer_Situation[situationQuestionsCount];
                    QuestionGenerator();
                }
                break;
            case QuestionType.PhysicalSensations:
                // Set next question type
                questionType = QuestionType.UnhelpfulThoughts;
                // Add the player's input to the mood diary info
                angerDiaryInfo.Answer_PhysicalSensation = answerInput.text;
                // Set the input text as next question's answer
                answerInput.text = angerDiaryInfo.Answer_UnhelpfulThoughts;
                QuestionGenerator();
                break;
            case QuestionType.UnhelpfulThoughts:
                // Set next question type
                questionType = QuestionType.ChallengeThoughts;
                // Add the player's input to the mood diary info
                angerDiaryInfo.Answer_UnhelpfulThoughts = answerInput.text;
                // Set the input text as next question's answer
                answerInput.text = angerDiaryInfo.Answer_ChallengeThoughts[0];
                QuestionGenerator();
                break;
            case QuestionType.ChallengeThoughts:
                if (challengeThoughtsQuestionsCount >= challengeThoughtsQuestions.Length)
                {
                    // Add the player's input to the mood diary info
                    angerDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount - 1] = answerInput.text;
                    Save();
                    moodCheckManager.OpenActivitySelection();
                    moodCheckManager.activitySelection.GetComponent<ActivitySelection>().RemoveActivity(3);
                }
                else
                {
                    // Add the player's input to the mood diary info
                    angerDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount - 1] = answerInput.text;
                    // Set the input text as next question's answer
                    answerInput.text = angerDiaryInfo.Answer_ChallengeThoughts[challengeThoughtsQuestionsCount];
                    QuestionGenerator();
                }
                break;
        }
    }

    public void Save()
    {
        moodCheckManager.moodCheckInfo.angerDiaryInfo = angerDiaryInfo;
        moodCheckManager.moodCheckInfo.angerDiaryActive = true;
    }

    public void Reset()
    {
        for (int i = 0; i < angerDiaryInfo.Question_Situation.Length; ++i)
        {
            angerDiaryInfo.Answer_Situation[i] = "";
            angerDiaryInfo.Question_Situation[i] = "";
        }

        angerDiaryInfo.Answer_PhysicalSensation = "";
        angerDiaryInfo.Question_PhysicalSensation = "";

        angerDiaryInfo.Answer_UnhelpfulThoughts = "";
        angerDiaryInfo.Question_PhysicalSensation = "";

        for (int i = 0; i < angerDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            angerDiaryInfo.Answer_ChallengeThoughts[i] = "";
            angerDiaryInfo.Question_ChallengeThoughts[i] = "";
        }

        questionType = QuestionType.Situation;
        challengeThoughtsQuestionsCount = 0;
        answerInput.text = "";
    }

}
