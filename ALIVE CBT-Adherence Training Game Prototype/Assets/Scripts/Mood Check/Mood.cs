using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mood : MonoBehaviour {

    [HideInInspector]
    public GameObject manager;
    [HideInInspector]
    public GameObject moodCheckPanel;
    [HideInInspector]
    public GameObject moodCheckMenu;
    [HideInInspector]
    public GameObject moodPanel;
    [HideInInspector]
    public CalendarUnit calendarUnit;

    private MoodCheckInfo _moodCheckInfo;
 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMoodCheckInfo(MoodCheckInfo _moodCheckInfo)
    {
        this._moodCheckInfo = _moodCheckInfo;
        calendarUnit.dateTime = Convert.ToDateTime(_moodCheckInfo.dateTime);
    }

    public void OpenMoodPanel()
    {
        // Open the activity menu
        manager.GetComponent<SceneManager_Calendar>().OpenMoodMenu();
        moodPanel.GetComponent<MoodPanel>().moodCheckInfo = _moodCheckInfo;
    }

    public MoodCheckInfo GetMoodCheckInfo()
    {
        return _moodCheckInfo;
    }

    public void LoadMood()
    {
        if (_moodCheckInfo.emotionsFeltBefore == null)
            return;

        string time = "";
        if(calendarUnit.dateTime.Hour < 10)
        {
            time += "0";
        }
        time += calendarUnit.dateTime.Hour;

        if(calendarUnit.dateTime.Minute < 10)
        {
            time += "0";
        }
        time += calendarUnit.dateTime.Minute;

        // Set the time
        this.transform.GetChild(0).GetComponent<Text>().text = time;

        for(int i = 0; i < 4; ++i)
        {
            // Set all the mood images to false
            this.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }

        if(_moodCheckInfo.moodDiaryActive)
            this.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        if (_moodCheckInfo.worryDiaryActive)
            this.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        if (_moodCheckInfo.angerDiaryActive)
            this.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
        if (_moodCheckInfo.posThoughtsJournalActive)
            this.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);        
    }
}
