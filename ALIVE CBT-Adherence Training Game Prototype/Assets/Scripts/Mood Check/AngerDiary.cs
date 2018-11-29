using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngerDiary : MonoBehaviour, IMoodCheckActivity_Generic
{
    public DialogueNode dialogueNode;

    public MoodCheckManager moodCheckManager;
    public Text question;
    public InputField answerInput;
    
    private AngerDiaryInfo _angerDiaryInfo;

    // Use this for initialization
    void Start()
    {

        // Setting up Anger Diary Info 
        _angerDiaryInfo = new AngerDiaryInfo();

        DialogueNode tempNode = dialogueNode;
        int counter = 0;
        int counter2 = 0;
        while (tempNode != null)
        {
            if (tempNode.dialogueType == DialogueNode.DialogueType.Question)
            {
                if (tempNode.questionType == QuestionType.UnhelpfulThoughts)
                    counter++;
                else if (tempNode.questionType == QuestionType.ChallengeThoughts)
                    counter2++;
            }
            tempNode = tempNode.nextNode;
        }

        _angerDiaryInfo.Question_UnhelpfulThoughts = new string[counter];
        _angerDiaryInfo.Answer_UnhelpfulThoughts = new string[counter];

        _angerDiaryInfo.Question_ChallengeThoughts = new string[counter2];
        _angerDiaryInfo.Answer_ChallengeThoughts = new string[counter2];

        DialogueGenerator();
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


    public void DialogueGenerator()
    {
        // Set the question text
        if (dialogueNode.content.Length == 1)
            question.text = dialogueNode.content[0];

        if (dialogueNode.dialogueType == DialogueNode.DialogueType.Question)
        {   // If the dialogue type is question 

            // Set input text as active
            answerInput.gameObject.SetActive(true);
        }
        else if (dialogueNode.dialogueType == DialogueNode.DialogueType.Response)
        {   // If the dialogue type is question 

            // Set input text as not active
            answerInput.gameObject.SetActive(false);
        }
    }

    public void Back()
    {
        // Check if previous node is null
        if (dialogueNode.prevNode == null)
        {   // If previous node is null,
            // go back to activity selection
            Reset();
            moodCheckManager.OpenActivitySelection();
        }
        else
        {
            // If previous node not null,
            // set to previous dialogue

            // Set dialogueNode to it's prev node
            dialogueNode = dialogueNode.prevNode;
            // Check the dialogue type
            if (dialogueNode.dialogueType == DialogueNode.DialogueType.Question)
            {   // If it is question type
                switch (dialogueNode.questionType)
                {
                    case QuestionType.ChallengeThoughts:
                        if (dialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question
                            && dialogueNode.nextNode.questionType == QuestionType.ChallengeThoughts)
                        {
                            // Add player's input into the mood diary info
                            _angerDiaryInfo.Answer_ChallengeThoughts[dialogueNode.nextNode.index] = answerInput.text;
                        }
                        // Set the input text as next question's answer
                        answerInput.text = _angerDiaryInfo.Answer_ChallengeThoughts[dialogueNode.index];
                        break;
                    case QuestionType.UnhelpfulThoughts:
                        if (dialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question)
                        {
                            if (dialogueNode.nextNode.questionType == QuestionType.ChallengeThoughts)
                            {
                                // Add player's input into the mood diary info
                                _angerDiaryInfo.Answer_ChallengeThoughts[dialogueNode.nextNode.index] = answerInput.text;
                            }
                            else if (dialogueNode.nextNode.questionType == QuestionType.UnhelpfulThoughts)
                            {
                                // Add player's input into the mood diary info
                                _angerDiaryInfo.Answer_UnhelpfulThoughts[dialogueNode.nextNode.index] = answerInput.text;
                            }
                        }
                        // Set the input text as next question's answer
                        answerInput.text = _angerDiaryInfo.Answer_UnhelpfulThoughts[dialogueNode.index];
                        break;
                    case QuestionType.PhysicalSensations:
                        if (dialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question
                            && dialogueNode.nextNode.questionType == QuestionType.UnhelpfulThoughts)
                        {
                            // Add player's input into the mood diary info
                            _angerDiaryInfo.Answer_UnhelpfulThoughts[dialogueNode.nextNode.index] = answerInput.text;
                        }
                        // Set the input text as next question's answer
                        answerInput.text = _angerDiaryInfo.Answer_PhysicalSensation;
                        break;
                    case QuestionType.Situation:
                        // Set the input text as previous question's answer
                        answerInput.text = _angerDiaryInfo.Answer_Situation;
                        break;
                }
            }
            else if (dialogueNode.dialogueType == DialogueNode.DialogueType.Response)
            {   // If it is response type

                if (dialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question)
                {
                    if (dialogueNode.nextNode.questionType == QuestionType.ChallengeThoughts)
                    {
                        // Add player's input into the mood diary info
                        _angerDiaryInfo.Answer_ChallengeThoughts[dialogueNode.nextNode.index] = answerInput.text;
                    }
                    else if (dialogueNode.nextNode.questionType == QuestionType.UnhelpfulThoughts)
                    {
                        // Add player's input into the mood diary info
                        _angerDiaryInfo.Answer_UnhelpfulThoughts[dialogueNode.nextNode.index] = answerInput.text;
                    }
                    else if (dialogueNode.nextNode.questionType == QuestionType.PhysicalSensations)
                    {
                        // Add player's input into the mood diary info
                        _angerDiaryInfo.Answer_PhysicalSensation = answerInput.text;
                    }
                    else if (dialogueNode.nextNode.questionType == QuestionType.Situation)
                    {
                        // Add player's input into the mood diary info
                        _angerDiaryInfo.Answer_Situation = answerInput.text;
                    }
                }
            }

            // Generate Dialogue
            DialogueGenerator();
        }
    }

    public void Next()
    {
        // Check if previous node is null
        if (dialogueNode.nextNode == null)
        {   // If previous node is null,
            // go back to activity selection

            Save();
            // Open the activity selection menu
            moodCheckManager.OpenActivitySelection();
            // Remove the worry diary activity from activity selection
            moodCheckManager.activitySelection.GetComponent<ActivitySelection>().RemoveActivity(3);
        }
        else
        {
            // If previous node not null,
            // set to previous dialogue

            // Set dialogueNode to it's prev node
            dialogueNode = dialogueNode.nextNode;
            // Check the dialogue type
            if (dialogueNode.dialogueType == DialogueNode.DialogueType.Question)
            {   // If it is question type
                switch (dialogueNode.questionType)
                {
                    case QuestionType.ChallengeThoughts:
                        if (dialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question
                            && dialogueNode.prevNode.questionType == QuestionType.ChallengeThoughts)
                        {
                            // Add player's input into the mood diary info
                            _angerDiaryInfo.Answer_ChallengeThoughts[dialogueNode.prevNode.index] = answerInput.text;
                        }
                        // Set the input text as next question's answer
                        answerInput.text = _angerDiaryInfo.Answer_ChallengeThoughts[dialogueNode.index];
                        break;
                    case QuestionType.UnhelpfulThoughts:
                        if (dialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question)
                        {
                            if (dialogueNode.prevNode.questionType == QuestionType.PhysicalSensations)
                            {
                                // Add player's input into the mood diary info
                                _angerDiaryInfo.Answer_PhysicalSensation = answerInput.text;
                            }
                            else if (dialogueNode.prevNode.questionType == QuestionType.UnhelpfulThoughts)
                            {
                                // Add player's input into the mood diary info
                                _angerDiaryInfo.Answer_UnhelpfulThoughts[dialogueNode.prevNode.index] = answerInput.text;
                            }
                        }
                        // Set the input text as next question's answer
                        answerInput.text = _angerDiaryInfo.Answer_UnhelpfulThoughts[dialogueNode.index];
                        break;
                    case QuestionType.PhysicalSensations:
                        if (dialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question
                            && dialogueNode.prevNode.questionType == QuestionType.UnhelpfulThoughts)
                        {
                            // Add player's input into the mood diary info
                            _angerDiaryInfo.Answer_UnhelpfulThoughts[dialogueNode.prevNode.index] = answerInput.text;
                        }
                        // Set the input text as next question's answer
                        answerInput.text = _angerDiaryInfo.Answer_PhysicalSensation;
                        break;
                    case QuestionType.Situation:
                        // Set the input text as previous question's answer
                        answerInput.text = _angerDiaryInfo.Answer_Situation;
                        break;
                }
            }
            else if (dialogueNode.dialogueType == DialogueNode.DialogueType.Response)
            {   // If it is response type

                if (dialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question)
                {
                    if (dialogueNode.prevNode.questionType == QuestionType.ChallengeThoughts)
                    {
                        // Add player's input into the mood diary info
                        _angerDiaryInfo.Answer_ChallengeThoughts[dialogueNode.prevNode.index] = answerInput.text;
                    }
                    else if (dialogueNode.prevNode.questionType == QuestionType.UnhelpfulThoughts)
                    {
                        // Add player's input into the mood diary info
                        _angerDiaryInfo.Answer_UnhelpfulThoughts[dialogueNode.prevNode.index] = answerInput.text;
                    }
                    else if (dialogueNode.prevNode.questionType == QuestionType.PhysicalSensations)
                    {
                        // Add player's input into the mood diary info
                        _angerDiaryInfo.Answer_PhysicalSensation = answerInput.text;
                    }
                    else if (dialogueNode.prevNode.questionType == QuestionType.Situation)
                    {
                        // Add player's input into the mood diary info
                        _angerDiaryInfo.Answer_Situation = answerInput.text;
                    }
                }
            }

            // Generate Dialogue
            DialogueGenerator();
        }
    }

    public void Save()
    {
        moodCheckManager.moodCheckInfo.angerDiaryInfo = _angerDiaryInfo;
        moodCheckManager.moodCheckInfo.angerDiaryActive = true;
    }

    public void Reset()
    {
        for (int i = 0; i < _angerDiaryInfo.Answer_UnhelpfulThoughts.Length; ++i)
        {
            _angerDiaryInfo.Answer_UnhelpfulThoughts[i] = "";
            _angerDiaryInfo.Question_UnhelpfulThoughts[i] = "";
        }

        _angerDiaryInfo.Answer_PhysicalSensation = "";
        _angerDiaryInfo.Question_PhysicalSensation = "";

        _angerDiaryInfo.Answer_Situation = "";
        _angerDiaryInfo.Question_Situation = "";

        for (int i = 0; i < _angerDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            _angerDiaryInfo.Answer_ChallengeThoughts[i] = "";
            _angerDiaryInfo.Question_ChallengeThoughts[i] = "";
        }
        
        answerInput.text = "";
    }

}
