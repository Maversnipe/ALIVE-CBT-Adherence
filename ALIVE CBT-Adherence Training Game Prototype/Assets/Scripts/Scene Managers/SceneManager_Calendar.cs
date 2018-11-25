using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_Calendar : MonoBehaviour {

    // Calendar Panel
    public GameObject calendarPanel;
    // Day Menu Panel
    public GameObject dayMenuPanel;
    // Add Activity Panel
    public GameObject activityPanel;
    // Delete Confirmation Panel
    public GameObject deleteConfirmation;
    // Mood Check Panel
    public GameObject moodCheckPanel;
    // Mood Panel
    public GameObject moodPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenDayMenu()
    {
        // Set Calendar Page to be inactive
        calendarPanel.SetActive(false);
        // Set Day Menu to be active
        dayMenuPanel.SetActive(true);
        // Set Add Activity Menu to be inactive
        activityPanel.SetActive(false);
        // Set Mood Check Menu to be inactive
        moodCheckPanel.SetActive(false);
        // Set Mood Menu to be inactive
        moodPanel.SetActive(false);
    }

    public void OpenCalendarMenu()
    {
        // Set Calendar Page to be active
        calendarPanel.SetActive(true);
        // Set Day Menu to be inactive
        dayMenuPanel.SetActive(false);
        // Set Add Activity Menu to be inactive
        activityPanel.SetActive(false);
        // Set Mood Check Menu to be inactive
        moodCheckPanel.SetActive(false);
        // Set Mood Menu to be inactive
        moodPanel.SetActive(false);
    }

    public void OpenActivityMenu()
    {
        // Set Calendar Page to be inactive
        calendarPanel.SetActive(false);
        // Set Day Menu to be inactive
        dayMenuPanel.SetActive(false);
        // Set Add Activity Menu to be active
        activityPanel.SetActive(true);
        // Set Mood Check Menu to be inactive
        moodCheckPanel.SetActive(false);
        // Set Mood Menu to be inactive
        moodPanel.SetActive(false);
    }

    public void OpenMoodCheckMenu()
    {
        // Set Calendar Page to be inactive
        calendarPanel.SetActive(false);
        // Set Day Menu to be inactive
        dayMenuPanel.SetActive(false);
        // Set Add Activity Menu to be active
        activityPanel.SetActive(false);
        // Set Mood Check Menu to be inactive
        moodCheckPanel.SetActive(true);
        // Set Mood Menu to be inactive
        moodPanel.SetActive(false);
    }

    public void OpenMoodMenu()
    {
        // Set Calendar Page to be inactive
        calendarPanel.SetActive(false);
        // Set Day Menu to be inactive
        dayMenuPanel.SetActive(false);
        // Set Add Activity Menu to be active
        activityPanel.SetActive(false);
        // Set Mood Check Menu to be inactive
        moodCheckPanel.SetActive(false);
        // Set Mood Menu to be inactive
        moodPanel.SetActive(true);
    }

    // Open the popup
    public void OpenDeleteConfirmationPopup()
    {
        deleteConfirmation.SetActive(true);
    }

    // Close the popup
    public void CloseDeleteConfirmationPopup()
    {
        deleteConfirmation.SetActive(false);
    }
}
