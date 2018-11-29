using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodCheckMenu : MonoBehaviour {
    public GameObject manager;
    public GameObject moodCheckPanel;

    private bool _canLoadMood;
    // Prefab for one activity
    public GameObject moodPrefab;
    // Content of the scroll rect containing the activities
    public GameObject moodContent;
    private List<MoodCheckInfo> _listOfMood = new List<MoodCheckInfo>();

    [HideInInspector]
    public CalendarUnit calendarUnit;

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
        MoodCheckInfoComparer mciComparer = new MoodCheckInfoComparer();
        _listOfMood = manager.GetComponent<EmotionsManager>().GetMoodCheck(calendarUnit.dateTime);
        _listOfMood.Sort(mciComparer);
        Debug.Log(_listOfMood.Count);

        for (int i = 0; i < _listOfMood.Count; ++i)
        {
            GameObject newMood = Instantiate(moodPrefab, transform);
            newMood.GetComponent<Mood>().SetMoodCheckInfo(_listOfMood[i]);
            newMood.GetComponent<Mood>().LoadMood();
            newMood.transform.SetParent(moodContent.transform);
            newMood.GetComponent<Mood>().manager = manager;
            newMood.GetComponent<Mood>().moodCheckPanel = moodCheckPanel;
            newMood.GetComponent<Mood>().moodCheckMenu = this.gameObject;
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
        manager.GetComponent<EmotionsManager>().LoadMood(calendarUnit.dateTime, SetCanLoadEmotions);

        for (int i = moodContent.transform.childCount - 1; i > 0; --i)
        {
            Destroy(moodContent.transform.GetChild(i).gameObject);
        }
    }

    public void SetCanLoadEmotions(bool _checker)
    {
        _canLoadMood = _checker;
    }
}
