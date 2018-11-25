using System.Collections.Generic;
using System;
using UnityEngine;
using Firebase.Database;
using System.Linq;

public class ActivityManager : MonoBehaviour {
    
    private DatabaseReference _databaseRef;
    private List<ActivityInfo> _listOfActivities = new List<ActivityInfo>();
    private DataSnapshot _datasnapshot;

    /*
     = = ActivityDatabase = =
     * 0 = name (string)
     * 1 = description (string)
     * 2 = startHour (int)
     * 3 = startMinute (int)
     * 4 = endHour (int)
     * 5 = endMinute (int)
     * 6 = date (DateTime)
     * 7 = id (int)
     */

    // Use this for initialization
    void Start ()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // Set Persistence for mobile
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);
        }
        // Get the root reference location of the database.
        _databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        LoadActivities();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Add activity to the database
    public void AddActivity(ActivityInfo _activity)
    {
        _activity.key = _databaseRef.Child("activities").Push().Key;
        string json = JsonUtility.ToJson(_activity);
        _databaseRef.Child("activities").Child(_activity.key).SetRawJsonValueAsync(json);
    }

    // Remove an activity to database
    public void RemoveActivity(string _key)
    {
        _databaseRef.Child("activities").Child(_key).RemoveValueAsync();
    }

    // Load activities from database based on date
    public void LoadActivities(Action<bool> callBackFunction)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("activities")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle Error
                }
                else if (task.IsCompleted)
                {
                    _listOfActivities.Clear();
                    _datasnapshot = task.Result;
                }
                
                callBackFunction(true);
            });
    }

    // Load activities from database based on date
    public void LoadActivities()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("activities")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle Error
                }
                else if (task.IsCompleted)
                {
                    _listOfActivities.Clear();
                    _datasnapshot = task.Result;
                }
            });
    }

    public List<ActivityInfo> GetActivities()
    {
        return _listOfActivities;
    }

    public List<ActivityInfo> GetActivities(DateTime _dateTime)
    {
        _listOfActivities.Clear();
        for (int i = 0; i < _datasnapshot.ChildrenCount; ++i)
        {
            ActivityInfo newActivity = JsonUtility.FromJson<ActivityInfo>(_datasnapshot.Children.ToList()[i].GetRawJsonValue());
            if (newActivity.date.Equals(_dateTime.ToString()))
            {
                _listOfActivities.Add(newActivity);
            }
        }
        return _listOfActivities;
    }
}