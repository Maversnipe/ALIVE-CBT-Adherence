using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activity : MonoBehaviour {

    public Text activityName;
    public Text time;

    [HideInInspector]
    public GameObject manager;
    [HideInInspector]
    public GameObject activityPanel;
    [HideInInspector]
    public GameObject activityScheduleMenu;

    private ActivityInfo _activityInfo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetActivity(ActivityInfo _activity)
    {
        _activityInfo = _activity;
    }

    public void LoadActivity()
    {
        activityName.text = _activityInfo.name;
        string[] allTime = new string[4];

        // Start hour
        if (_activityInfo.startTime.Hour < 10)
            allTime[0] = "0" + _activityInfo.startTime.Hour.ToString();
        else
            allTime[0] = _activityInfo.startTime.Hour.ToString();
        // Start Minute
        if (_activityInfo.startTime.Minute < 10)
            allTime[1] = "0" + _activityInfo.startTime.Minute.ToString();
        else
            allTime[1] = _activityInfo.startTime.Minute.ToString();
        // End Hour
        if (_activityInfo.endTime.Hour < 10)
            allTime[2] = "0" + _activityInfo.endTime.Hour.ToString();
        else
            allTime[2] = _activityInfo.endTime.Hour.ToString();
        // End Minute
        if (_activityInfo.endTime.Minute < 10)
            allTime[3] = "0" + _activityInfo.endTime.Minute.ToString();
        else
            allTime[3] = _activityInfo.endTime.Minute.ToString();

        time.text = allTime[0] + allTime[1]
            + " - " + allTime[2] + allTime[3];
    }

    public void OpenActivityMenu()
    {
        // Open the activity menu
        manager.GetComponent<SceneManager_Calendar>().OpenActivityMenu();
        activityPanel.GetComponent<SelectedActivityManager>().OpenActivityMenu();
        // Load the activity menu's day
        activityPanel.transform.GetChild(1).GetChild(0).GetComponent<ActivityMenu>().SetActivity(_activityInfo);
        activityPanel.transform.GetChild(1).GetChild(0).GetComponent<ActivityMenu>().LoadDay();
    }

    public void OpenDeleteConfirmationPopup()
    {
        manager.GetComponent<SceneManager_Calendar>().OpenDeleteConfirmationPopup();
    }

    public ActivityInfo GetActvitiyInfo()
    {
        return _activityInfo;
    }

    public void SetActivityToBeDeleted()
    {
        activityScheduleMenu.GetComponent<ActivityScheduleMenu>().SetToBeDeleted(_activityInfo.key);
    }
}
