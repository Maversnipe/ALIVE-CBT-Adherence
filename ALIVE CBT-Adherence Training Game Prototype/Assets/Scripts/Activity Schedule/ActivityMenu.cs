using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityMenu : MonoBehaviour {

    public Text date;
    public Text activityName;
    public Text description;
    public Text[] time = new Text[4];
    public GameObject manager;
    public GameObject activitySchedule;

    private ActivityInfo _activityInfo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadDay()
    {
        date.text = activitySchedule.GetComponent<ActivityScheduleMenu>().date.text;
        activityName.text = _activityInfo.name;
        description.text = _activityInfo.description;
        
        // Start hour
        if (_activityInfo.startTime.Hour < 10)
            time[0].text = "0" + _activityInfo.startTime.Hour.ToString();
        else
            time[0].text = _activityInfo.startTime.Hour.ToString();
        // Start Minute
        if (_activityInfo.startTime.Minute < 10)
            time[1].text = "0" + _activityInfo.startTime.Minute.ToString();
        else
            time[1].text = _activityInfo.startTime.Minute.ToString();
        // End Hour
        if (_activityInfo.endTime.Hour < 10)
            time[2].text = "0" + _activityInfo.endTime.Hour.ToString();
        else
            time[2].text = _activityInfo.endTime.Hour.ToString();
        // End Minute
        if (_activityInfo.endTime.Minute < 10)
            time[3].text = "0" + _activityInfo.endTime.Minute.ToString();
        else
            time[3].text = _activityInfo.endTime.Minute.ToString();
    }

    public void SetActivity(ActivityInfo _activity)
    {
        _activityInfo = _activity;
    }
}
