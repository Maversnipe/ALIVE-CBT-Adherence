using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorryDiary : MonoBehaviour, IMoodCheckActivity_Generic
{
    public DialogueNode dialogueNode;
    public DialogueNode originalNode;

    public MoodCheckManager moodCheckManager;
    public Text question;
    public InputField answerInput;

    private WorryDiaryInfo _worryDiaryInfo;

    // Use this for initialization
    void Start()
    {
        // Setting up Mood Diary Info 
        _worryDiaryInfo = new WorryDiaryInfo();

        DialogueNode tempNode = dialogueNode;
        int counter = 0;
        while(tempNode != null)
        {
            if(tempNode.questionType == QuestionType.ChallengeThoughts
                && tempNode.dialogueType == DialogueNode.DialogueType.Question)
            {
                counter++;
            }
            tempNode = tempNode.nextNode;
        }
        _worryDiaryInfo.Question_ChallengeThoughts = new string[counter];
        _worryDiaryInfo.Answer_ChallengeThoughts = new string[counter];

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

            switch (dialogueNode.questionType)
            {
                case QuestionType.Situation:
                    _worryDiaryInfo.Question_Situation = question.text;
                    break;
                case QuestionType.ChallengeThoughts:
                    _worryDiaryInfo.Question_ChallengeThoughts[dialogueNode.index] = question.text;
                    break;
            }
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
        {   // If previous node not null,
            // set to previous dialogue

            // Set dialogueNode to it's prev node
            dialogueNode = dialogueNode.prevNode;
            // Check the dialogue type
            if (dialogueNode.dialogueType == DialogueNode.DialogueType.Question)
            {   // If it is question type

                // Check if the question type is Challenge Thoughts Type
                if (dialogueNode.nextNode.questionType == QuestionType.ChallengeThoughts)
                {
                    if (dialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question
                        && dialogueNode.nextNode.questionType == QuestionType.ChallengeThoughts)
                    {
                        // Add player's input into the mood diary info
                        _worryDiaryInfo.Answer_ChallengeThoughts[dialogueNode.nextNode.index] = answerInput.text;
                    }
                    // Set the input text as previous question's answer
                    answerInput.text = _worryDiaryInfo.Answer_ChallengeThoughts[dialogueNode.index];
                }
                else
                {
                    // Set the input text as previous question's answer
                    answerInput.text = _worryDiaryInfo.Answer_Situation;
                }
            }
            else if (dialogueNode.dialogueType == DialogueNode.DialogueType.Response)
            {   // If it is response type

                if (dialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question)
                {
                    if (dialogueNode.nextNode.questionType == QuestionType.ChallengeThoughts)
                    {
                        // Add player's input into the mood diary info
                        _worryDiaryInfo.Answer_ChallengeThoughts[dialogueNode.nextNode.index] = answerInput.text;
                    }
                }
            }

            // Generate Dialogue
            DialogueGenerator();
        }
    }

    public void Next()
    {
        // Check if next node is null
        if (dialogueNode.nextNode == null)
        {   // If next node is null,
            // go back to activity selection
            
            Save();
            // Open the activity selection menu
            moodCheckManager.OpenActivitySelection();
            // Remove the worry diary activity from activity selection
            moodCheckManager.activitySelection.GetComponent<ActivitySelection>().RemoveActivity(2);
        }
        else
        {   // If previous node not null,
            // set to previous dialogue

            // Set dialogueNode to it's next node
            dialogueNode = dialogueNode.nextNode;

            // Check the dialogue type
            if (dialogueNode.dialogueType == DialogueNode.DialogueType.Question)
            {   // If it is question type

                // Check if the question type is Challenge Thoughts Type or Situation type
                if (dialogueNode.questionType == QuestionType.ChallengeThoughts)
                {
                    if (dialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question
                        && dialogueNode.prevNode.questionType == QuestionType.ChallengeThoughts)
                    {
                        // Add player's input into the mood diary info
                        _worryDiaryInfo.Answer_ChallengeThoughts[dialogueNode.prevNode.index] = answerInput.text;
                    }
                    // Set the input text as next question's answer
                    answerInput.text = _worryDiaryInfo.Answer_ChallengeThoughts[dialogueNode.index];
                }
            }
            else if (dialogueNode.dialogueType == DialogueNode.DialogueType.Response)
            {   // If it is response type

                if (dialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question)
                {
                    if (dialogueNode.prevNode.questionType == QuestionType.ChallengeThoughts)
                    {
                        // Add player's input into the mood diary info
                        _worryDiaryInfo.Answer_ChallengeThoughts[dialogueNode.prevNode.index] = answerInput.text;
                    }
                    else if(dialogueNode.prevNode.questionType == QuestionType.Situation)
                    {
                        // Add player's input into the mood diary info
                        _worryDiaryInfo.Answer_Situation = answerInput.text;
                    }
                }
            }

            // Generate Dialogue
            DialogueGenerator();
        }
    }

    public void Save()
    {
        moodCheckManager.moodCheckInfo.worryDiaryInfo = _worryDiaryInfo;
        moodCheckManager.moodCheckInfo.worryDiaryActive = true;
        Reset();
    }

    public void Reset()
    {
        dialogueNode = originalNode;
        _worryDiaryInfo.Answer_Situation = "";
        _worryDiaryInfo.Question_Situation = "";

        for (int i = 0; i < _worryDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            _worryDiaryInfo.Answer_ChallengeThoughts[i] = "";
            _worryDiaryInfo.Question_ChallengeThoughts[i] = "";
        }
        
        answerInput.text = "";
    }

}