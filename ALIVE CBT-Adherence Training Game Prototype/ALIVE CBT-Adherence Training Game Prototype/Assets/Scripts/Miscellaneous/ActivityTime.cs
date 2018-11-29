using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActivityTime {

    // Hour
    public int Hour;
    // Minute
    public int Minute;
    // Time
    public int Time;

    public ActivityTime()
    {
        Hour = 0;
        Minute = 0;
        Time = 0;
    }

    public ActivityTime(int _hour, int _minute)
    {
        Hour = _hour;
        Minute = _minute;
        Time = (Hour * 100) + Minute;
    }

    public void SetActivityTime(int _hour, int _minute)
    {
        Hour = _hour;
        Minute = _minute;
        Time = (Hour * 100) + Minute;
    }
}
