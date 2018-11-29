using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodDiary : MonoBehaviour, IMoodCheckActivity_Generic
{
    public DialogueNode dialogueNode;
    public DialogueNode dialogueNode_Last;
    public DialogueNode dialogueNode_Yes;
    public DialogueNode dialogueNode_No;

    public MoodCheckManager moodCheckManager;
    public Text question;
    public InputField answerInput;
    public GameObject YesNo;
    
    private MoodDiaryInfo _moodDiaryInfo;
    private bool _isChallengingQuestions = false;
    private DialogueNode _currDialogueNode;
    private bool _yesNoSelected;

    // Use this for initialization
    void Start () {
        _moodDiaryInfo = new MoodDiaryInfo();

        DialogueNode tempNode = dialogueNode_No;
        int counter = 0;
        while (tempNode != null)
        {
            if (tempNode.dialogueType == DialogueNode.DialogueType.Question)
            {
                counter++;
            }
            tempNode = tempNode.nextNode;
        }

        // Setting up Mood Diary Info 
        _moodDiaryInfo = new MoodDiaryInfo();
        _moodDiaryInfo.Question_ChallengeThoughts = new string[counter];
        _moodDiaryInfo.Answer_ChallengeThoughts = new string[counter];

        tempNode = dialogueNode;
        counter = 0;
        while (tempNode != null)
        {
            if (tempNode.dialogueType == DialogueNode.DialogueType.Question 
                && tempNode.questionType == QuestionType.UnhelpfulThoughts)
            {
                counter++;
            }
            tempNode = tempNode.nextNode;
        }

        _moodDiaryInfo.Question_UnhelpfulThoughts = new string[counter];
        _moodDiaryInfo.Answer_UnhelpfulThoughts = new string[counter];

        _currDialogueNode = dialogueNode;
        _yesNoSelected = false;

        DialogueGenerator();
    }
	
	// Update is called once per frame
	void Update () {
        if(answerInput.gameObject.activeSelf && answerInput.text == "")
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
        if (_currDialogueNode.content.Length == 1)
            question.text = _currDialogueNode.content[0];
        else
        {
            int randNum = Random.Range(0, _currDialogueNode.content.Length);
            question.text = _currDialogueNode.content[randNum];
        }

        if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Question)
        {   // If the dialogue type is question 

            // Set input text as active
            if(!YesNo.activeSelf)
                answerInput.gameObject.SetActive(true);
        }
        else if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Response)
        {   // If the dialogue type is question 

            // Set input text as not active
            answerInput.gameObject.SetActive(false);
        }
    }

    public void Back()
    {
        if (_isChallengingQuestions)
        {
            // Check if previous node is null
            if (_currDialogueNode.prevNode == null)
            {   
                YesNo.SetActive(true);

                _currDialogueNode = dialogueNode_Last;

                _isChallengingQuestions = false;

                answerInput.gameObject.SetActive(false);

                for(int i = 0; i < _moodDiaryInfo.Answer_ChallengeThoughts.Length;  ++i)
                {
                    _moodDiaryInfo.Answer_ChallengeThoughts[i] = "";
                    _moodDiaryInfo.Question_ChallengeThoughts[i] = "";
                }
            }
            else
            {
                // If previous node not null,
                // set to previous dialogue

                // Set _currDialogueNode to it's prev node
                _currDialogueNode = _currDialogueNode.prevNode;
                // Check the dialogue type
                if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Question)
                {   // If it is question type
                    switch (_currDialogueNode.questionType)
                    {
                        case QuestionType.ChallengeThoughts:
                            if (_currDialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question
                                && _currDialogueNode.nextNode.questionType == QuestionType.ChallengeThoughts)
                            {
                                // Add player's input into the mood diary info
                                _moodDiaryInfo.Answer_ChallengeThoughts[_currDialogueNode.nextNode.index] = answerInput.text;
                            }
                            // Set the input text as next question's answer
                            answerInput.text = _moodDiaryInfo.Answer_ChallengeThoughts[_currDialogueNode.index];
                            break;
                    }
                }
                else if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Response)
                {   // If it is response type

                    if (_currDialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question)
                    {
                        if (_currDialogueNode.nextNode.questionType == QuestionType.ChallengeThoughts)
                        {
                            // Add player's input into the mood diary info
                            _moodDiaryInfo.Answer_ChallengeThoughts[_currDialogueNode.nextNode.index] = answerInput.text;
                        }
                    }
                }
            }

            // Generate Dialogue
            DialogueGenerator();
        }
        else
        {
            // Check if previous node is null
            if (_currDialogueNode.prevNode == null)
            {   // If previous node is null,
                // go back to activity selection

                Reset();
                moodCheckManager.OpenActivitySelection();
            }
            else
            {
                // If previous node not null,
                // set to previous dialogue

                // Set _currDialogueNode to it's prev node
                _currDialogueNode = _currDialogueNode.prevNode;
                // Check the dialogue type
                if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Question)
                {   // If it is question type
                    switch (_currDialogueNode.questionType)
                    {                       
                        case QuestionType.UnhelpfulThoughts:
                            if (_currDialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question)
                            {
                                if (_currDialogueNode.nextNode.questionType == QuestionType.UnhelpfulThoughts)
                                {
                                    // Add player's input into the mood diary info
                                    _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.nextNode.index] = answerInput.text;
                                }
                                else if(_currDialogueNode.nextNode.questionType == QuestionType.UnhelpfulThoughts)
                                {
                                    // Add player's input into the mood diary info
                                    _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.nextNode.index] = answerInput.text;
                                }
                            }
                            // Set the input text as next question's answer
                            answerInput.text = _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.index];
                            break;
                        case QuestionType.PhysicalSensations:
                            if (_currDialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question
                                && _currDialogueNode.nextNode.questionType == QuestionType.UnhelpfulThoughts)
                            {
                                // Add player's input into the mood diary info
                                _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.nextNode.index] = answerInput.text;
                            }
                            // Set the input text as next question's answer
                            answerInput.text = _moodDiaryInfo.Answer_PhysicalSensation;
                            break;
                        case QuestionType.Situation:
                            // Set the input text as previous question's answer
                            answerInput.text = _moodDiaryInfo.Answer_Situation;
                            break;
                    }
                }
                else if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Response)
                {   // If it is response type

                    if (_currDialogueNode.nextNode.dialogueType == DialogueNode.DialogueType.Question)
                    {
                        if (_currDialogueNode.nextNode.questionType == QuestionType.UnhelpfulThoughts)
                        {
                            // Add player's input into the mood diary info
                            _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.nextNode.index] = answerInput.text;
                        }
                        else if (_currDialogueNode.nextNode.questionType == QuestionType.PhysicalSensations)
                        {
                            // Add player's input into the mood diary info
                            _moodDiaryInfo.Answer_PhysicalSensation = answerInput.text;
                        }
                        else if (_currDialogueNode.nextNode.questionType == QuestionType.Situation)
                        {
                            // Add player's input into the mood diary info
                            _moodDiaryInfo.Answer_Situation = answerInput.text;
                        }
                    }
                }

                if (YesNo.activeSelf)
                    YesNo.SetActive(false);

                // Generate Dialogue
                DialogueGenerator();
            }
        }
    }

    public void Next()
    {
        if (_isChallengingQuestions)
        {
            // Check if previous node is null
            if (_currDialogueNode.nextNode == null)
            {   // If previous node is null,
                // go back to activity selection

                Save();
                // Open the activity selection menu
                moodCheckManager.OpenActivitySelection();
                // Remove the worry diary activity from activity selection
                moodCheckManager.activitySelection.GetComponent<ActivitySelection>().RemoveActivity(1);
            }
            else
            {
                // If previous node not null,
                // set to previous dialogue

                // Set _currDialogueNode to it's prev node
                _currDialogueNode = _currDialogueNode.nextNode;
                // Check the dialogue type
                if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Question)
                {   // If it is question type
                    if (_currDialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question
                        && _currDialogueNode.prevNode.questionType == QuestionType.ChallengeThoughts)
                    {
                        // Add player's input into the mood diary info
                        _moodDiaryInfo.Answer_ChallengeThoughts[_currDialogueNode.prevNode.index] = answerInput.text;
                    }
                    // Set the input text as next question's answer
                    answerInput.text = _moodDiaryInfo.Answer_ChallengeThoughts[_currDialogueNode.index];
                    
                }
                else if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Response)
                {   // If it is response type

                    if (_currDialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question)
                    {
                        // Add player's input into the mood diary info
                        _moodDiaryInfo.Answer_ChallengeThoughts[_currDialogueNode.prevNode.index] = answerInput.text;
                    }
                }

                // Generate Dialogue
                DialogueGenerator();
            }
        }
        else
        {
            // Check if next node is null
            if (_currDialogueNode.nextNode == null)
            {   // Next Node is null

                if(YesNo.transform.GetChild(0).GetComponent<Image>().color 
                    == YesNo.transform.GetChild(0).GetComponent<Button>().colors.disabledColor)
                {
                    _currDialogueNode = dialogueNode_No;

                    _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.index] = "No";
                }
                else if (YesNo.transform.GetChild(1).GetComponent<Image>().color
                    == YesNo.transform.GetChild(1).GetComponent<Button>().colors.disabledColor)
                {
                    _currDialogueNode = dialogueNode_Yes;

                    _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.index] = "Yes";
                }

                YesNo.SetActive(false);

                _isChallengingQuestions = true;

                answerInput.text = _moodDiaryInfo.Answer_ChallengeThoughts[_currDialogueNode.index];
            }
            else
            {
                // If next node not null,
                // set to next dialogue

                // Set _currDialogueNode to it's prev node
                _currDialogueNode = _currDialogueNode.nextNode;
                // Check the dialogue type
                if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Question)
                {   // If it is question type
                    switch (_currDialogueNode.questionType)
                    {
                        case QuestionType.UnhelpfulThoughts:
                            if (_currDialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question)
                            {
                                if (_currDialogueNode.prevNode.questionType == QuestionType.PhysicalSensations)
                                {
                                    // Add player's input into the mood diary info
                                    _moodDiaryInfo.Answer_PhysicalSensation = answerInput.text;
                                }
                                else if (_currDialogueNode.prevNode.questionType == QuestionType.UnhelpfulThoughts)
                                {
                                    // Add player's input into the mood diary info
                                    _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.prevNode.index] = answerInput.text;
                                }
                            }

                            if (_currDialogueNode.nextNode == null)
                            {   // If the node after the next is null

                                // Set Yes / No buttons as true
                                YesNo.SetActive(true);
                                // Set input as false
                                answerInput.gameObject.SetActive(false);
                            }
                            else
                            {
                                // Set the input text as next question's answer
                                answerInput.text = _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.index];
                            }
                            break;
                        case QuestionType.PhysicalSensations:
                            // Set the input text as next question's answer
                            answerInput.text = _moodDiaryInfo.Answer_PhysicalSensation;
                            break;
                        case QuestionType.Situation:
                            // Set the input text as previous question's answer
                            answerInput.text = _moodDiaryInfo.Answer_Situation;
                            break;
                    }
                }
                else if (_currDialogueNode.dialogueType == DialogueNode.DialogueType.Response)
                {   // If it is response type

                    if (_currDialogueNode.prevNode.dialogueType == DialogueNode.DialogueType.Question)
                    {
                        if (_currDialogueNode.prevNode.questionType == QuestionType.UnhelpfulThoughts)
                        {
                            // Add player's input into the mood diary info
                            _moodDiaryInfo.Answer_UnhelpfulThoughts[_currDialogueNode.prevNode.index] = answerInput.text;
                        }
                        else if (_currDialogueNode.prevNode.questionType == QuestionType.PhysicalSensations)
                        {
                            // Add player's input into the mood diary info
                            _moodDiaryInfo.Answer_PhysicalSensation = answerInput.text;
                        }
                        else if (_currDialogueNode.prevNode.questionType == QuestionType.Situation)
                        {
                            // Add player's input into the mood diary info
                            _moodDiaryInfo.Answer_Situation = answerInput.text;
                        }
                    }
                }
            }

            // Generate Dialogue
            DialogueGenerator();
        }
    }

    public void YesNoClicked(int _selected)
    {
        if(YesNo.transform.GetChild(_selected).GetComponent<Image>().color 
            == YesNo.transform.GetChild(_selected).GetComponent<Button>().colors.normalColor
            && !_yesNoSelected)
        {
            YesNo.transform.GetChild(_selected).GetComponent<Image>().color =
                YesNo.transform.GetChild(_selected).GetComponent<Button>().colors.disabledColor;
            _yesNoSelected = true;
        }
        else if(YesNo.transform.GetChild(_selected).GetComponent<Image>().color
            == YesNo.transform.GetChild(_selected).GetComponent<Button>().colors.disabledColor)
        {
            YesNo.transform.GetChild(_selected).GetComponent<Image>().color =
                YesNo.transform.GetChild(_selected).GetComponent<Button>().colors.normalColor;
            _yesNoSelected = false;
        }
    }

    public void Save()
    {
        moodCheckManager.moodCheckInfo.moodDiaryInfo = _moodDiaryInfo;
        moodCheckManager.moodCheckInfo.moodDiaryActive = true;
    }

    public void Reset()
    {
        _moodDiaryInfo.Answer_Situation = "";
        _moodDiaryInfo.Question_Situation = "";

        _moodDiaryInfo.Answer_PhysicalSensation = "";
        _moodDiaryInfo.Question_PhysicalSensation = "";
        
        for(int i = 0; i < _moodDiaryInfo.Question_UnhelpfulThoughts.Length; ++i)
        {
            _moodDiaryInfo.Answer_UnhelpfulThoughts[i] = "";
            _moodDiaryInfo.Question_UnhelpfulThoughts[i] = "";
        }

        for(int i = 0; i < _moodDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            _moodDiaryInfo.Answer_ChallengeThoughts[i] = "";
            _moodDiaryInfo.Question_ChallengeThoughts[i] = "";
        }

        _yesNoSelected = false;
        answerInput.text = "";
        _isChallengingQuestions = false;
    }

}