using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositiveThoughtsDiary : MonoBehaviour {

    public MoodCheckManager moodCheckManager;
    public GameObject optionsButtons;
    public GameObject optionsMenu;

    [HideInInspector]
    public PositiveOptionsType positiveOptionsType;

    private PositiveThoughtsJournalInfo positiveThoughtsJournalInfo;
    private bool _inOptions = false;

    private int _currMenu;
    private bool _completedOwnActivity;

    public enum PositiveOptionsType
    {
        None,
        People = 0,
        Food,
        Place,
        Events,
        Hobbies,
        CountYourBlessings
    }

    // Use this for initialization
    void Start()
    {
        // Setting up Mood Diary Info 
        positiveThoughtsJournalInfo = new PositiveThoughtsJournalInfo();
        positiveOptionsType = PositiveOptionsType.None;
        _completedOwnActivity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_inOptions)
        {
            if (optionsMenu.transform.GetChild(_currMenu).GetChild(1).GetComponent<InputField>().text == "")
                moodCheckManager.next.interactable = false;
            else
                moodCheckManager.next.interactable = true;
        }
        else
        {
            if (_completedOwnActivity)
                moodCheckManager.next.interactable = true;
            else
                moodCheckManager.next.interactable = false;
        }
    }

    public void ButtonClicked(int _selected)
    {
        positiveOptionsType = (PositiveOptionsType)_selected;
        OpenMenu(_selected);
    }

    public void OpenMenu(int _selected)
    {
        _currMenu = _selected;
        for(int i = 0; i < optionsMenu.transform.childCount; ++i)
        {
            if(i == _selected)
            {
                optionsMenu.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                optionsMenu.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        optionsButtons.SetActive(false);
        _inOptions = true;
    }

    public void OpenPosThoughts()
    {
        optionsButtons.SetActive(true);
        optionsMenu.transform.GetChild(_currMenu).gameObject.SetActive(false);

    }
    
    public void Back()
    {
        if (_inOptions)
        {
            optionsMenu.transform.GetChild(_currMenu).GetChild(1).GetComponent<InputField>().text = "";
            _inOptions = false;
            OpenPosThoughts();
        }
        else
        {
            Reset();
            moodCheckManager.OpenActivitySelection();
        }
    }

    public void Next()
    {
        if (_inOptions)
        {
            // Add the player's input to the mood diary info
            switch (positiveOptionsType)
            {
                case PositiveOptionsType.People:
                    positiveThoughtsJournalInfo.People = optionsMenu.transform.GetChild(0).GetChild(1).GetComponent<InputField>().text;
                    optionsButtons.transform.GetChild(0).GetComponent<Button>().interactable = false;
                    positiveThoughtsJournalInfo.People_Check = true;
                    break;
                case PositiveOptionsType.Food:
                    positiveThoughtsJournalInfo.Food = optionsMenu.transform.GetChild(1).GetChild(1).GetComponent<InputField>().text;
                    optionsButtons.transform.GetChild(1).GetComponent<Button>().interactable = false;
                    positiveThoughtsJournalInfo.Food_Check = true;
                    break;
                case PositiveOptionsType.Place:
                    positiveThoughtsJournalInfo.Place = optionsMenu.transform.GetChild(2).GetChild(1).GetComponent<InputField>().text;
                    optionsButtons.transform.GetChild(2).GetComponent<Button>().interactable = false;
                    positiveThoughtsJournalInfo.Place_Check = true;
                    break;
                case PositiveOptionsType.Events:
                    positiveThoughtsJournalInfo.Events = optionsMenu.transform.GetChild(3).GetChild(1).GetComponent<InputField>().text;
                    optionsButtons.transform.GetChild(3).GetComponent<Button>().interactable = false;
                    positiveThoughtsJournalInfo.Events_Check = true;
                    break;
                case PositiveOptionsType.Hobbies:
                    positiveThoughtsJournalInfo.Hobbies = optionsMenu.transform.GetChild(4).GetChild(1).GetComponent<InputField>().text;
                    optionsButtons.transform.GetChild(4).GetComponent<Button>().interactable = false;
                    positiveThoughtsJournalInfo.Hobbies_Check = true;
                    break;
                case PositiveOptionsType.CountYourBlessings:
                    positiveThoughtsJournalInfo.CountYourBlessings = optionsMenu.transform.GetChild(5).GetChild(1).GetComponent<InputField>().text;
                    optionsButtons.transform.GetChild(5).GetComponent<Button>().interactable = false;
                    positiveThoughtsJournalInfo.CountYourBlessings_Check = true;
                    break;
            }
            
            OpenPosThoughts();
            _completedOwnActivity = true;
            _inOptions = false;
        }
        else
        {
            Save();
            // Open the activity selection menu
            moodCheckManager.OpenActivitySelection();
            // Remove the worry diary activity from activity selection
            moodCheckManager.activitySelection.GetComponent<ActivitySelection>().RemoveActivity(1);
        }
    }

    public void Save()
    {
        moodCheckManager.moodCheckInfo.posThoughtsJournalInfo = positiveThoughtsJournalInfo;
        moodCheckManager.moodCheckInfo.posThoughtsJournalActive = true;
    }

    public void Reset()
    {
        positiveThoughtsJournalInfo.People = "";
        positiveThoughtsJournalInfo.Food = "";
        positiveThoughtsJournalInfo.Place = "";
        positiveThoughtsJournalInfo.Events = "";
        positiveThoughtsJournalInfo.Hobbies = "";
        positiveThoughtsJournalInfo.CountYourBlessings = "";

        positiveThoughtsJournalInfo.People_Check = false;
        positiveThoughtsJournalInfo.Food_Check = false;
        positiveThoughtsJournalInfo.Place_Check = false;
        positiveThoughtsJournalInfo.Events_Check = false;
        positiveThoughtsJournalInfo.Hobbies_Check = false;
        positiveThoughtsJournalInfo.CountYourBlessings_Check = false;

        for (int i = 0; i < optionsMenu.transform.childCount; ++i)
        {
            optionsMenu.transform.GetChild(i).GetChild(1).GetComponent<InputField>().text = "";
            optionsButtons.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }

        _inOptions = false;
        _completedOwnActivity = false;
    }

}
