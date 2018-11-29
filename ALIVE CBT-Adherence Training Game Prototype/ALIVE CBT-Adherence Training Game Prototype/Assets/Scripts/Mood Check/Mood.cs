using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mood : MonoBehaviour {

    [HideInInspector]
    public GameObject manager;
    [HideInInspector]
    public GameObject moodCheckPanel;
    [HideInInspector]
    public GameObject moodCheckMenu;

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
    }

    public void OpenMoodMenu()
    {
        //// Open the activity menu
        //manager.GetComponent<SceneManager_Calendar>().OpenActivityMenu();
        //moodCheckPanel.GetComponent<SelectedActivityManager>().OpenActivityMenu();
        //// Load the activity menu's day
        //moodCheckPanel.transform.GetChild(1).GetChild(0).GetComponent<ActivityMenu>().SetActivity(moodCheckMenu);
        //moodCheckPanel.transform.GetChild(1).GetChild(0).GetComponent<ActivityMenu>().LoadDay();
    }

    public MoodCheckInfo GetMoodCheckInfo()
    {
        return _moodCheckInfo;
    }

    public void LoadMood()
    {
        if (_moodCheckInfo.emotionsFeltBefore == null)
            return;

        if(_moodCheckInfo.moodDiaryActive)
            this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        if (_moodCheckInfo.worryDiaryActive)
            this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        if (_moodCheckInfo.angerDiaryActive)
            this.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        if (_moodCheckInfo.posThoughtsJournalActive)
            this.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
    }
}
