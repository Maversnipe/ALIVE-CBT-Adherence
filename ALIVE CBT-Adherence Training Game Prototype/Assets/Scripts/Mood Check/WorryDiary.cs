using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorryDiary : MonoBehaviour, IMoodCheckActivity_Generic
{

    public QuestionAndResponse[] situationQuestions;
    public QuestionAndResponse[] challengeThoughtsQuestions;
    public DialogueNode dialogueNode;

    public MoodCheckManager moodCheckManager;
    public Text question;
    public InputField answerInput;

    private QuestionType questionType;
    private int _challengeThoughtsQuestionsCount = 0;
    private int _challengeThoughtsQuestionsCountToMinus = 0;
    private WorryDiaryInfo _worryDiaryInfo;
    private QuestionAndResponse _currentQnR;
    private bool _isQuestion;

    // Use this for initialization
    void Start()
    {
        // Set the question type to the first type, which is situation
        questionType = QuestionType.Situation;

        // Setting up Mood Diary Info 
        _worryDiaryInfo = new WorryDiaryInfo();

        DialogueNode tempNode = dialogueNode;
        int CTQCount = 0;
        while(tempNode != null)
        {
            if(tempNode.questionType == QuestionType.ChallengeThoughts)
            {
                CTQCount++;
            }
            tempNode = tempNode.nextNode;
        }
        _worryDiaryInfo.Question_ChallengeThoughts = new string[CTQCount];
        _worryDiaryInfo.Answer_ChallengeThoughts = new string[CTQCount];

        QuestionGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (answerInput.gameObject.activeSelf && answerInput.text == "")
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
        // Set the input text as active
        answerInput.gameObject.SetActive(true);
        _isQuestion = true;
        int randNum;
        switch (questionType)
        {
            case QuestionType.Situation:
                randNum = Random.Range(0, situationQuestions.Length);
                _currentQnR = situationQuestions[randNum];
                if (_currentQnR.Question == "")
                {
                    _isQuestion = false;
                    ResponseGenerator();
                }
                else
                {
                    _isQuestion = true;
                    _worryDiaryInfo.Question_Situation = question.text = situationQuestions[randNum].Question;
                }
                break;
            case QuestionType.ChallengeThoughts:
                if (_challengeThoughtsQuestionsCount < challengeThoughtsQuestions.Length)
                {
                    _currentQnR = challengeThoughtsQuestions[_challengeThoughtsQuestionsCount];
                    if (_currentQnR.Question == "")
                    {
                        _isQuestion = false;
                        ResponseGenerator();
                    }
                    else
                    {
                        _isQuestion = true;
                        _worryDiaryInfo.Question_ChallengeThoughts[_challengeThoughtsQuestionsCount - _challengeThoughtsQuestionsCountToMinus] 
                            = question.text = challengeThoughtsQuestions[_challengeThoughtsQuestionsCount].Question;
                    }
                }
                break;
        }
    }

    public void ResponseGenerator()
    {
        // Set the input text as not active
        answerInput.gameObject.SetActive(false);
        _isQuestion = false;
        switch (questionType)
        {
            case QuestionType.Situation:
                question.text = _currentQnR.Response;
                break;
            case QuestionType.ChallengeThoughts:
                question.text = _currentQnR.Response;
                break;
        }
    }

    public void Back()
    {
        switch (questionType)
        {
            case QuestionType.Situation:
                if (_isQuestion)
                {
                    Reset();
                    moodCheckManager.OpenActivitySelection();
                }
                else
                {
                    QuestionGenerator();
                }
                break;

            case QuestionType.ChallengeThoughts:
                // Check if the count is above one
                if (_challengeThoughtsQuestionsCount - _challengeThoughtsQuestionsCountToMinus > 1)
                {
                    // Check if current text is question or response
                    if (_isQuestion)
                    {   // If current is a question
                        // Check if previous text is a response or question
                        QuestionAndResponse qnrChecker = challengeThoughtsQuestions[_challengeThoughtsQuestionsCount - 1];
                        // Check if prev text has a response
                        if (qnrChecker.Response == "")
                        {   // If prev text has no response
                            // Generate question instead
                            _challengeThoughtsQuestionsCount--;
                            QuestionGenerator();
                            answerInput.text = _worryDiaryInfo.Answer_ChallengeThoughts[_challengeThoughtsQuestionsCount - _challengeThoughtsQuestionsCountToMinus];
                            Debug.Log("Is Q, prev no res");
                        }
                        else
                        {   // If prev text has a response
                            // Generate response instead
                            _challengeThoughtsQuestionsCount -= 1;
                            ResponseGenerator();
                            Debug.Log("Is Q, prev has res");
                        }
                    }
                    else
                    {   // If current is a response
                        // Check if there is a question
                        if(_currentQnR.Question == "")
                        {   // If no question
                            // Check if previous is a response or question
                            QuestionAndResponse qnrChecker = challengeThoughtsQuestions[_challengeThoughtsQuestionsCount - 1];
                            // Check if prev text has a response
                            if (qnrChecker.Response == "")
                            {   // If prev text has no response
                                // Generate question instead
                                _challengeThoughtsQuestionsCount -= 1;
                                QuestionGenerator();
                                answerInput.text = _worryDiaryInfo.Answer_ChallengeThoughts[_challengeThoughtsQuestionsCount - _challengeThoughtsQuestionsCountToMinus];

                                Debug.Log("Is res, no curr Q, prev no res");
                            }
                            else
                            {   // If prev text has a response
                                // Generate response instead
                                _challengeThoughtsQuestionsCount -= 1;
                                ResponseGenerator();

                                Debug.Log("Is res, no curr Q, prev has res");
                            }

                            _challengeThoughtsQuestionsCountToMinus--;
                        }
                        else
                        {   // If have question
                            // Load question
                            QuestionGenerator();
                            answerInput.text = _worryDiaryInfo.Answer_ChallengeThoughts[_challengeThoughtsQuestionsCount - _challengeThoughtsQuestionsCountToMinus];

                            Debug.Log("Is res, has curr Q");
                        }
                    }
                }
                else
                {   // If count not above one
                    // Either go back or make to question
                    if(_isQuestion)
                    {   // If is a question
                        // Go to situations
                        
                    }

                }
                break;
        }
    }

    public void Next()
    {
        switch (questionType)
        {
            case QuestionType.Situation:
                if ((_currentQnR.Response == "") || !_isQuestion)
                {
                    // Set next question type
                    questionType = QuestionType.ChallengeThoughts;
                    // Add the player's input to the mood diary info
                    _worryDiaryInfo.Answer_Situation = answerInput.text;
                    // Set the input text as next question's answer
                    answerInput.text = _worryDiaryInfo.Answer_ChallengeThoughts[0];
                    QuestionGenerator();
                }
                else
                {
                    ResponseGenerator();
                }
                break;
            case QuestionType.ChallengeThoughts:
                // Checks if the counter is more than or equal to the number of questions
                if(_challengeThoughtsQuestionsCount >= challengeThoughtsQuestions.Length)
                {   // If it is more than or equal
                    // Move to the next scene

                    if ((_currentQnR.Response == "") || !_isQuestion)
                    {   // If there is no response,
                        // or the previous text was a response
                        // Ask question
                        
                        // Save Information
                        Save();
                        // Open the activity selection menu
                        moodCheckManager.OpenActivitySelection();
                        // Remove the worry diary activity from activity selection
                        moodCheckManager.activitySelection.GetComponent<ActivitySelection>().RemoveActivity(2);
                    }
                    else
                    {   // If the previous text was a question
                        // And there is a response
                        // Add response

                        // Add the player's input into the diary info
                        _worryDiaryInfo.Answer_ChallengeThoughts[_challengeThoughtsQuestionsCount - _challengeThoughtsQuestionsCountToMinus] = answerInput.text;
                        // Generate response
                        ResponseGenerator();
                    }
                }
                else
                {   // If it is less than
                    // Put next question or response
                    if((_currentQnR.Response == "") || !_isQuestion)
                    {   // If there is no response,
                        // or the previous text was a response
                        // Ask question

                        // Set the input text to be the next question's answer
                        _challengeThoughtsQuestionsCount++;
                        answerInput.text = _worryDiaryInfo.Answer_ChallengeThoughts[_challengeThoughtsQuestionsCount - _challengeThoughtsQuestionsCountToMinus];
                        // Generate question
                        QuestionGenerator();
                        if(_currentQnR.Question == "")
                        {
                            _challengeThoughtsQuestionsCountToMinus++;
                        }
                    }
                    else
                    {   // If the previous text was a question
                        // And there is a response
                        // Add response

                        // Add the player's input into the diary info
                        _worryDiaryInfo.Answer_ChallengeThoughts[_challengeThoughtsQuestionsCount - _challengeThoughtsQuestionsCountToMinus] = answerInput.text;
                        // Generate response
                        ResponseGenerator();
                    }
                }
                break;
        }
    }

    public void Save()
    {
        moodCheckManager.moodCheckInfo.worryDiaryInfo = _worryDiaryInfo;
        moodCheckManager.moodCheckInfo.worryDiaryActive = true;
    }

    public void Reset()
    {
        _worryDiaryInfo.Answer_Situation = "";
        _worryDiaryInfo.Question_Situation = "";

        for (int i = 0; i < _worryDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            _worryDiaryInfo.Answer_ChallengeThoughts[i] = "";
            _worryDiaryInfo.Question_ChallengeThoughts[i] = "";
        }

        questionType = QuestionType.Situation;
        _challengeThoughtsQuestionsCount = 0;
        _challengeThoughtsQuestionsCountToMinus = 0;
        answerInput.text = "";
    }

}

