using System;
using System.Collections.Generic;
using UnityEngine;

public class MoodCheckMenu : MonoBehaviour {
    public GameObject manager;
    public GameObject moodCheckPanel;
    public GameObject moodPanel;
    // Prefab for one activity
    public GameObject moodPrefab;
    // Content of the scroll rect containing the activities
    public GameObject moodContent;
    public GameObject addMoodButton;
    public GameObject noEntryText;

    [HideInInspector]
    public CalendarUnit calendarUnit;

    // List of the mood
    private List<MoodCheckInfo> _listOfMood = new List<MoodCheckInfo>();
    // To check if mood can be loaded
    private bool _canLoadMood = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Checks if able to load activities
        // since database will load activities async
        if (_canLoadMood)
        {
            PrintMood();
            _canLoadMood = false;
        }
    }

    // Prints out activities on screen
    public void PrintMood()
    {
        for (int i = moodContent.transform.childCount - 1; i > 0; --i)
        {
            Destroy(moodContent.transform.GetChild(i).gameObject);
        }

        // Check if the date is today
        if (calendarUnit.dateTime.Date != DateTime.Today)
        {   // If not today, remove add mood button
            addMoodButton.SetActive(false);
        }
        else
        {   // If is today, put back add mood button
            addMoodButton.SetActive(true);
        }

        MoodCheckInfoComparer mciComparer = new MoodCheckInfoComparer();
        _listOfMood = manager.GetComponent<EmotionsManager>().GetMoodCheck(calendarUnit.dateTime.Date);
        _listOfMood.Sort(mciComparer);

        // Check if there are any mood entries
        if (_listOfMood.Count > 0)
        {   // If there are mood entries, hide "No entries Text"
            // And add all the entries
            noEntryText.SetActive(false);
            for (int i = 0; i < _listOfMood.Count; ++i)
            {
                GameObject newMood = Instantiate(moodPrefab, transform);
                newMood.GetComponent<Mood>().calendarUnit = calendarUnit;
                newMood.GetComponent<Mood>().moodCheckPanel = moodCheckPanel;
                newMood.GetComponent<Mood>().moodCheckMenu = this.gameObject;
                newMood.GetComponent<Mood>().moodPanel = moodPanel;
                newMood.GetComponent<Mood>().SetMoodCheckInfo(_listOfMood[i]);
                newMood.GetComponent<Mood>().LoadMood();
                newMood.GetComponent<Mood>().manager = manager;
                newMood.transform.SetParent(moodContent.transform);
            }
        }
        else
        {   // If there are no mood entries, show "No entries Text"
            noEntryText.SetActive(true);
        }


    }

    // Load Day
    public void LoadDay(CalendarUnit _calendarUnit)
    {
        calendarUnit = _calendarUnit;
        // Do loading for activities
        StartLoadActivities();
    }

    // Starts the process of loading activities
    public void StartLoadActivities()
    {
        manager.GetComponent<EmotionsManager>().LoadMood(SetCanLoadEmotions);

    }

    public void SetCanLoadEmotions(bool _checker)
    {
        _canLoadMood = _checker;
    }
}
