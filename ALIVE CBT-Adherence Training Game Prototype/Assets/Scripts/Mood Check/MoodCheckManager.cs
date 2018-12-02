using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Linq;
using UnityEngine.UI;

public class MoodCheckManager : MonoBehaviour
{

    public GameObject manager;

    public GameObject moodRating;
    public GameObject activitySelection;
    public GameObject moodDiary;
    public GameObject positiveThoughtsJournal;
    public GameObject worryDiary;
    public GameObject angerDiary;
    public GameObject moodRatingSecond;

    public GameObject moodCheckMenu;

    public Button next;

    public List<EmotionInfo> listOfPlayerEmotions = new List<EmotionInfo>();

    public MoodCheckInfo moodCheckInfo;

    private MoodCheckPanels _currentPanel;

    private DatabaseReference databaseRef;

    public enum MoodCheckPanels
    {
        MoodRating,
        ActivitySelection,
        MoodDiary,
        PositiveThoughtsJournal,
        WorryDiary,
        AngerDiary,
        CalmRelaxationExercise,
        MoodRatingSecond
    }

    // This will be used to keep track of all the
    // mood check menus the player goes through
    private Stack<MoodCheckPanels> _prevIndexes = new Stack<MoodCheckPanels>();

    // Use this for initialization
    void Start()
    {
        _currentPanel = MoodCheckPanels.MoodRating;
        moodCheckInfo.emotionsFeltBefore = new EmotionInfo[4];
        moodCheckInfo.emotionsFeltAfter = new EmotionInfo[4];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save()
    {
        for (int i = 0; i < listOfPlayerEmotions.Count; ++i)
        {
            moodCheckInfo.emotionsFeltAfter[i] = listOfPlayerEmotions[i];
        }
        moodCheckInfo.dateTime = DateTime.Now.ToString();
        manager.GetComponent<EmotionsManager>().AddMoodCheck(moodCheckInfo);
        moodCheckInfo = new MoodCheckInfo();
        moodCheckInfo.emotionsFeltBefore = new EmotionInfo[4];
        moodCheckInfo.emotionsFeltAfter = new EmotionInfo[4];
    }

    // When the player presses the back button
    public void Back()
    {
        switch (_currentPanel)
        {
            case MoodCheckPanels.MoodRating:
                Reset();
                manager.GetComponent<SceneManager_Calendar>().OpenDayMenu();
                this.gameObject.SetActive(false);
                moodCheckMenu.GetComponent<MoodCheckMenu>().StartLoadActivities();
                break;
            case MoodCheckPanels.ActivitySelection:
                activitySelection.GetComponent<ActivitySelection>().Back();
                moodCheckInfo = new MoodCheckInfo();
                moodCheckInfo.emotionsFeltBefore = new EmotionInfo[4];
                moodCheckInfo.emotionsFeltAfter = new EmotionInfo[4];
                break;
            case MoodCheckPanels.MoodDiary:
                moodDiary.GetComponent<MoodDiary>().Back();
                break;
            case MoodCheckPanels.PositiveThoughtsJournal:
                positiveThoughtsJournal.GetComponent<PositiveThoughtsDiary>().Back();
                break;
            case MoodCheckPanels.WorryDiary:
                worryDiary.GetComponent <WorryDiary> ().Back();
                break;
            case MoodCheckPanels.AngerDiary:
                angerDiary.GetComponent<AngerDiary>().Back();
                break;
            case MoodCheckPanels.MoodRatingSecond:
                OpenActivitySelection();
                break;
        }
    }

    public void Next()
    {
        switch(_currentPanel)
        {
            case MoodCheckPanels.MoodRating:
                OpenActivitySelection();
                for (int i = 0; i < listOfPlayerEmotions.Count; ++i)
                {
                    EmotionInfo newInfo = new EmotionInfo((int)listOfPlayerEmotions[i].emotionType);
                    newInfo.intensity = listOfPlayerEmotions[i].intensity; 
                    moodCheckInfo.emotionsFeltBefore[i] = newInfo;
                }
                activitySelection.GetComponent<ActivitySelection>().Open();
                break;
            case MoodCheckPanels.ActivitySelection:
                OpenMoodRatingSecond();
                moodRatingSecond.GetComponent<MoodRatingSecond>().SetEmotions();
                break;
            case MoodCheckPanels.MoodDiary:
                moodDiary.GetComponent<MoodDiary>().Next();
                break;
            case MoodCheckPanels.PositiveThoughtsJournal:
                positiveThoughtsJournal.GetComponent<PositiveThoughtsDiary>().Next();
                break;
            case MoodCheckPanels.WorryDiary:
                worryDiary.GetComponent<WorryDiary>().Next();
                break;
            case MoodCheckPanels.AngerDiary:
                angerDiary.GetComponent<AngerDiary>().Next();
                break;
            case MoodCheckPanels.MoodRatingSecond:
                Save();
                Reset();
                manager.GetComponent<SceneManager_Calendar>().OpenDayMenu();
                moodCheckMenu.GetComponent<MoodCheckMenu>().StartLoadActivities();
                this.gameObject.SetActive(false);
                break;
        }
    }

    public void OpenMoodRating()
    {
        // Set the activity selection panel active 
        // and the other panels as not active
        moodRating.SetActive(true);
        activitySelection.SetActive(false);
        moodDiary.SetActive(false);
        positiveThoughtsJournal.SetActive(false);
        worryDiary.SetActive(false);
        angerDiary.SetActive(false);
        moodRatingSecond.SetActive(false);

        _prevIndexes.Push(_currentPanel);
        _currentPanel = MoodCheckPanels.MoodRating;
    }

    public void OpenActivitySelection()
    {
        // Set the activity selection panel active 
        // and the other panels as not active
        moodRating.SetActive(false);
        activitySelection.SetActive(true);
        moodDiary.SetActive(false);
        positiveThoughtsJournal.SetActive(false);
        worryDiary.SetActive(false);
        angerDiary.SetActive(false);
        moodRatingSecond.SetActive(false);

        _prevIndexes.Push(_currentPanel);
        _currentPanel = MoodCheckPanels.ActivitySelection;
    }

    public void OpenMoodDiary()
    {
        // Set the activity selection panel active 
        // and the other panels as not active
        moodRating.SetActive(false);
        activitySelection.SetActive(false);
        moodDiary.SetActive(true);
        positiveThoughtsJournal.SetActive(false);
        worryDiary.SetActive(false);
        angerDiary.SetActive(false);
        moodRatingSecond.SetActive(false);

        _prevIndexes.Push(_currentPanel);
        _currentPanel = MoodCheckPanels.MoodDiary;
    }

    public void OpenPosThoughts()
    {
        // Set the activity selection panel active 
        // and the other panels as not active
        moodRating.SetActive(false);
        activitySelection.SetActive(false);
        moodDiary.SetActive(false);
        positiveThoughtsJournal.SetActive(true);
        worryDiary.SetActive(false);
        angerDiary.SetActive(false);
        moodRatingSecond.SetActive(false);

        _prevIndexes.Push(_currentPanel);
        _currentPanel = MoodCheckPanels.PositiveThoughtsJournal;
    }

    public void OpenWorryDiary()
    {
        // Set the activity selection panel active 
        // and the other panels as not active
        moodRating.SetActive(false);
        activitySelection.SetActive(false);
        moodDiary.SetActive(false);
        positiveThoughtsJournal.SetActive(false);
        worryDiary.SetActive(true);
        angerDiary.SetActive(false);
        moodRatingSecond.SetActive(false);

        _prevIndexes.Push(_currentPanel);
        _currentPanel = MoodCheckPanels.WorryDiary;
    }

    public void OpenAngerDiary()
    {
        // Set the activity selection panel active 
        // and the other panels as not active
        moodRating.SetActive(false);
        activitySelection.SetActive(false);
        moodDiary.SetActive(false);
        positiveThoughtsJournal.SetActive(false);
        worryDiary.SetActive(false);
        angerDiary.SetActive(true);
        moodRatingSecond.SetActive(false);

        _prevIndexes.Push(_currentPanel);
        _currentPanel = MoodCheckPanels.AngerDiary;
    }

    public void OpenMoodRatingSecond()
    {
        // Set the activity selection panel active 
        // and the other panels as not active
        moodRating.SetActive(false);
        activitySelection.SetActive(false);
        moodDiary.SetActive(false);
        positiveThoughtsJournal.SetActive(false);
        worryDiary.SetActive(false);
        angerDiary.SetActive(false);
        moodRatingSecond.SetActive(true);

        _prevIndexes.Push(_currentPanel);
        _currentPanel = MoodCheckPanels.MoodRatingSecond;
    }

    // Reset the Mood check panel
    public void Reset()
    {
        _currentPanel = MoodCheckPanels.MoodRating;
        _prevIndexes.Clear();

        activitySelection.GetComponent<ActivitySelection>().Reset();
        moodRating.GetComponent<MoodRating>().Reset();
        moodRatingSecond.GetComponent<MoodRatingSecond>().Reset();

        OpenMoodRating();
    }

    public void SetCurrentPanel(MoodCheckPanels _newCurrent)
    {
        _currentPanel = _newCurrent;
    }

    public MoodCheckPanels GetCurrentPanel()
    {
        return _currentPanel;
    }
}
