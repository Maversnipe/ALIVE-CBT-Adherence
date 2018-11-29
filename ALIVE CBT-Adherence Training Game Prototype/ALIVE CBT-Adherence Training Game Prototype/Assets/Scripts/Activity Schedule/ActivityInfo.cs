using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActivityInfo
{
    // Activity name
    public string name;
    // Activity description
    public string description;
    // Start time of activity
    public ActivityTime startTime;
    // End time of actvity
    public ActivityTime endTime;
    // Date of activity
    public string date;
    // This represents the key to get the activity
    public string key;

    public ActivityInfo()
    {
        name = "Default";
        description = "Default Activity";
        startTime = new ActivityTime(0, 0);
        endTime = new ActivityTime(1, 0);
        date = DateTime.Now.ToString();
        key = "";
    }
}

public class ActivityInfoComparer : IComparer<ActivityInfo>
{
    public int Compare(ActivityInfo x, ActivityInfo y)
    {
        if (x.startTime.Time > y.startTime.Time)
            return 1;
        else if (x.startTime.Time < y.startTime.Time)
            return -1;
        else
            return 0;
    }
}
