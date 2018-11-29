using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivitySelection : MonoBehaviour {

    // Images of the player emotion
    public Image depressed;
    public Image anxious;
    public Image angry;
    public Image happy;

    // Buttons for activity
    public Button moodDiary;
    public Button positiveThoughtsJournal;
    public Button worryDiary;
    public Button angerDiary;
    public Button calmRelaxationExercise;

    public MoodCheckManager moodCheckManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEmotion()
    {
        for(int i = 0; i < moodCheckManager.listOfPlayerEmotions.Count; ++i)
        {
            switch(moodCheckManager.listOfPlayerEmotions[i].emotionType)
            {
                case EmotionInfo.EmotionType.Depressed:
                    // Set emotion icon to be true
                    depressed.gameObject.SetActive(true);
                    // Set activities to be true
                    moodDiary.gameObject.SetActive(true);
                    positiveThoughtsJournal.gameObject.SetActive(true);
                    break;
                case EmotionInfo.EmotionType.Anxious:
                    // Set emotion icon to be true
                    anxious.gameObject.SetActive(true);
                    // Set activities to be true
                    worryDiary.gameObject.SetActive(true);
                    calmRelaxationExercise.gameObject.SetActive(true);
                    positiveThoughtsJournal.gameObject.SetActive(true);
                    break;
                case EmotionInfo.EmotionType.Angry:
                    // Set emotion icon to be true
                    angry.gameObject.SetActive(true);
                    // Set activities to be true
                    angerDiary.gameObject.SetActive(true);
                    calmRelaxationExercise.gameObject.SetActive(true);
                    break;
                case EmotionInfo.EmotionType.Happy:
                    // Set emotion icon to be true
                    happy.gameObject.SetActive(true);
                    // Set activities to be true
                    positiveThoughtsJournal.gameObject.SetActive(true);
                     break;

            }
        }
    }

    public void Open()
    {
        SetEmotion();
    }

    public void Back()
    {
        Reset();
        moodCheckManager.OpenMoodRating();
    }

    public void RemoveActivity(int _buttonIndex)
    {
        switch(_buttonIndex)
        {
            case 0:
                moodDiary.interactable = false;
                break;
            case 1:
                positiveThoughtsJournal.interactable = false;
                break;
            case 2:
                worryDiary.interactable = false;
                break;
            case 3:
                angerDiary.interactable = false;
                break;
            case 4:
                calmRelaxationExercise.interactable = false;
                break;
        }
    }

    public void Reset()
    {
        moodDiary.interactable = true;
        positiveThoughtsJournal.interactable = true;
        worryDiary.interactable = true;
        angerDiary.interactable = true;
        calmRelaxationExercise.interactable = true;
    }
}
