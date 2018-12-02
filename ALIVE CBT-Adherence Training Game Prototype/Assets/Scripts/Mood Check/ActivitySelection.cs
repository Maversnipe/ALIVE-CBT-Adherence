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

    public GameObject informationsPanel;

    // Buttons for activity
    public Button moodDiary;
    public Button positiveThoughtsJournal;
    public Button worryDiary;
    public Button angerDiary;
    public MoodCheckManager moodCheckManager;

    [HideInInspector]
    public bool completedActivity = false;

    private bool _inInfo = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_inInfo || !completedActivity)
            moodCheckManager.GetComponent<MoodCheckManager>().next.interactable = false;
        else
            moodCheckManager.GetComponent<MoodCheckManager>().next.interactable = true;
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
                    positiveThoughtsJournal.gameObject.SetActive(true);
                    break;
                case EmotionInfo.EmotionType.Angry:
                    // Set emotion icon to be true
                    angry.gameObject.SetActive(true);
                    // Set activities to be true
                    angerDiary.gameObject.SetActive(true);
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
        if (_inInfo)
        {
            CloseInfo();
        }
        else
        {
            Reset();
            moodCheckManager.OpenMoodRating();
        }
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
        }
        completedActivity = true;
    }

    public void OpenInfo(int _selected)
    {
        informationsPanel.SetActive(true);
        switch(_selected)
        {
            case 0:
                // Mood Diary
                informationsPanel.transform.GetChild(0).GetComponent<Text>().text = "Mood Diary";
                informationsPanel.transform.GetChild(1).GetComponent<Text>().text = "If you feel sad about something, you can click on this diary.";
                break;
            case 1:
                // Worry Diary
                // Mood Diary
                informationsPanel.transform.GetChild(0).GetComponent<Text>().text = "Worry Diary";
                informationsPanel.transform.GetChild(1).GetComponent<Text>().text = "If you feel anxious or worried about something, you can click on this diary.";
                break;
            case 2:
                // Anger Diary
                // Mood Diary
                informationsPanel.transform.GetChild(0).GetComponent<Text>().text = "Anger Diary";
                informationsPanel.transform.GetChild(1).GetComponent<Text>().text = "If you feel angry about something, you can click on this diary.";
                break;
            case 3:
                // Positive Diary
                // Mood Diary
                informationsPanel.transform.GetChild(0).GetComponent<Text>().text = "Positive Thoughts Diary";
                informationsPanel.transform.GetChild(1).GetComponent<Text>().text = "If you feel good or feel happy, you can click on this diary.";
                break;
        }

        _inInfo = true;
    }

    public void CloseInfo()
    {
        informationsPanel.SetActive(false);
    }

    public void Reset()
    {
        moodDiary.interactable = true;
        moodDiary.gameObject.SetActive(false);
        depressed.gameObject.SetActive(false);


        positiveThoughtsJournal.interactable = true;
        positiveThoughtsJournal.gameObject.SetActive(false);
        happy.gameObject.SetActive(false);

        worryDiary.interactable = true;
        worryDiary.gameObject.SetActive(false);
        anxious.gameObject.SetActive(false);

        angerDiary.interactable = true;
        angerDiary.gameObject.SetActive(false);
        angry.gameObject.SetActive(false);
    }
}
