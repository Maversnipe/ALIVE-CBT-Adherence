using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarUnit : MonoBehaviour {

    // Type of day
    public enum DayType
    {
        Normal = 0,
        Happy,
        Sad,
        Angry,
        Anxious,
    }
    
    // The name of the day
    public string dayName;
    // The type of day
    public DayType dayType;
    // Date
    public DateTime dateTime;
    
    // Set the Date Time for this class
    public void SetDateTime(DateTime _dateTime)
    {
        switch(_dateTime.DayOfWeek)
        {
            case DayOfWeek.Monday:
                dayName = "Monday";
                break;
            case DayOfWeek.Tuesday:
                dayName = "Tuesday";
                break;
            case DayOfWeek.Wednesday:
                dayName = "Wednesday";
                break;
            case DayOfWeek.Thursday:
                dayName = "Thursday";
                break;
            case DayOfWeek.Friday:
                dayName = "Friday";
                break;
            case DayOfWeek.Saturday:
                dayName = "Saturday";
                break;
            case DayOfWeek.Sunday:
                dayName = "Sunday";
                break;
        }
        
        dayType = (DayType)UnityEngine.Random.Range(0, 4);
        dateTime = _dateTime;   
    }
}
