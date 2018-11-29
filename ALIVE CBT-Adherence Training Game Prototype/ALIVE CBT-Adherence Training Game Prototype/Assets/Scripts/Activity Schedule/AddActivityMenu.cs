using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddActivityMenu : MonoBehaviour {

    public GameObject manager;
    public GameObject activitySchedule;
    public GameObject inputField;
    public GameObject dropdown;
    public GameObject description;
    public Button confirmationButton;
    public GameObject[] timeInput = new GameObject[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(CheckCanAdd())
        {
            confirmationButton.interactable = true;
        }
        else
        {
            confirmationButton.interactable = false;
        }
	}

    // Add activity to the day
    public void AddActivity()
    {
        // Create a new activity
        ActivityInfo toBeAdded = new ActivityInfo();

        if (inputField.activeSelf)
        {   // If it is in input field mode, get name from input field
            toBeAdded.name = inputField.GetComponent<InputField>().textComponent.text;
            inputField.GetComponent<InputField>().textComponent.text = "";
        }
        else if(dropdown.activeSelf)
        {   // If it is in dropdown mode, get name from dropdown
            toBeAdded.name = dropdown.GetComponent<Dropdown>().options[dropdown.GetComponent<Dropdown>().value].text;
            dropdown.GetComponent<Dropdown>().options[dropdown.GetComponent<Dropdown>().value].text = "";
        }
        else
        {
            Debug.Log("Both input field and dropdown are inactive");
        }

        // Add the description
        toBeAdded.description = description.GetComponent<InputField>().textComponent.text;
        description.GetComponent<InputField>().textComponent.text = "";
        // Add the timings
        toBeAdded.startTime.SetActivityTime(timeInput[0].GetComponent<TimeScroll>().GetTargetIndex(),
            timeInput[1].GetComponent<TimeScroll>().GetTargetIndex() * 5);
        toBeAdded.endTime.SetActivityTime(timeInput[2].GetComponent<TimeScroll>().GetTargetIndex(),
            timeInput[3].GetComponent<TimeScroll>().GetTargetIndex() * 5);

        // Set the date
        CalendarUnit calendarUnit = activitySchedule.GetComponent<ActivityScheduleMenu>().calendarUnit;
        toBeAdded.date = new System.DateTime(calendarUnit.dateTime.Year, calendarUnit.dateTime.Month, calendarUnit.dateTime.Day).ToString();

        manager.GetComponent<ActivityManager>().AddActivity(toBeAdded);
    }

    // Check if all activity fields are filled
    public bool CheckCanAdd()
    {
        // Check if the input field has something
        if (inputField.activeSelf && inputField.GetComponent<InputField>().textComponent.text == "")
            return false;

        // Check that the end time is after the start time
        ActivityTime startTime = new ActivityTime(timeInput[0].GetComponent<TimeScroll>().GetTargetIndex(),
            timeInput[1].GetComponent<TimeScroll>().GetTargetIndex());
        ActivityTime endTime = new ActivityTime(timeInput[2].GetComponent<TimeScroll>().GetTargetIndex(),
            timeInput[3].GetComponent<TimeScroll>().GetTargetIndex());

        if (startTime.Time >= endTime.Time)
            return false;

        return true;
    }

    public void ResetValues()
    {
        inputField.GetComponent<InputField>().text = string.Empty;
        description.GetComponent<InputField>().text = string.Empty;
        for (int i = 0; i < timeInput.Length; ++i)
            timeInput[i].GetComponent<TimeScroll>().ResetScroll();
    }
}
