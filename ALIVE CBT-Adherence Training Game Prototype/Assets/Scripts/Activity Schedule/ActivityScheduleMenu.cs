using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityScheduleMenu : MonoBehaviour
{

    public Text date;
    public GameObject manager;
    // Prefab for one activity
    public GameObject activityPrefab;
    // Content of the scroll rect containing the activities
    public GameObject activityContent;
    // Activity panels
    public GameObject activityPanel;

    [HideInInspector]
    public CalendarUnit calendarUnit;

    private List<ActivityInfo> _listOfActivities = new List<ActivityInfo>();
    private string _activityKeyToBeDeleted = "";
    // TO check if activities can be loaded
    private bool _canLoadActivites;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        // Checks if able to load activities
        // since database will load activities async
        if (_canLoadActivites)
        {
            PrintActivities();
            _canLoadActivites = false;
        }
	}

    // Load Day
    public void LoadDay(CalendarUnit _calendarUnit)
    {
        date.text = "" + _calendarUnit.dateTime.Day.ToString() + " " + GetMonth(_calendarUnit.dateTime.Month) +
            " " + _calendarUnit.dateTime.Year.ToString();
        calendarUnit = _calendarUnit;

        // Do loading for activities
        PrintActivities();
    }

    public string GetMonth(int _month)
    {
        switch (_month)
        {
            case 1:
                return "Jan";
            case 2:
                return "Feb";
            case 3:
                return "Mar";
            case 4:
                return "Apr";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "Jul";
            case 8:
                return "Aug";
            case 9:
                return "Sep";
            case 10:
                return "Oct";
            case 11:
                return "Nov";
            case 12:
                return "Dec";
        }
        return "";
    }

    // Starts the process of loading activities
    public void StartLoadActivities()
    {
        manager.GetComponent<ActivityManager>().LoadActivities(SetCanLoadActivities);
    }

    // Prints out activities on screen
    public void PrintActivities()
    {
        for (int i = activityContent.transform.childCount - 1; i > 0; --i)
        {
            Destroy(activityContent.transform.GetChild(i).gameObject);
        }

        ActivityInfoComparer aiComparer = new ActivityInfoComparer();
        _listOfActivities = manager.GetComponent<ActivityManager>().GetActivities(calendarUnit.dateTime);
        _listOfActivities.Sort(aiComparer);

        for(int i = 0; i < _listOfActivities.Count; ++i)
        {
            GameObject newActivity = Instantiate(activityPrefab, transform);
            newActivity.GetComponent<Activity>().SetActivity(_listOfActivities[i]);
            newActivity.GetComponent<Activity>().LoadActivity();
            newActivity.transform.SetParent(activityContent.transform);
            newActivity.GetComponent<Activity>().manager = manager;
            newActivity.GetComponent<Activity>().activityPanel = activityPanel;
            newActivity.GetComponent<Activity>().activityScheduleMenu = this.gameObject;
        }
    }

    // Set 
    public void SetToBeDeleted(string _key)
    {
        _activityKeyToBeDeleted = _key;
    }

    public void RemoveActivity()
    {
        if(_activityKeyToBeDeleted == "")
        {
            Debug.Log("Activity Does Not Exist");
            return;
        }
        manager.GetComponent<ActivityManager>().RemoveActivity(_activityKeyToBeDeleted);
        StartLoadActivities();
    }

    public bool CanLoadActivities()
    {
        return _canLoadActivites;
    }

    public void SetCanLoadActivities(bool _checker)
    {
       _canLoadActivites = _checker;
    }
}
