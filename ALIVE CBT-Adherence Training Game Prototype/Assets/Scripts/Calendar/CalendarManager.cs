using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CalendarManager : MonoBehaviour {
    
    public Text yearText;
    public Text monthText;

    public GameObject monthlyCalendar;
    public GameObject weeklyCalendar;

    public Button switchMonthlyWeeklyButton;
    public Sprite[] daySprites = new Sprite[5];
    public Sprite[] currentDaySprites = new Sprite[5];

    const int _maxNumberOfDaysMonthly = 42;
    const int _numberOfDaysInAWeek = 7;

    private DateTime _focusedDateTime;

    private List<GameObject> _monthlyCalendarUnits = new List<GameObject>();
    private List<GameObject> _weeklyCalendarUnits = new List<GameObject>();

    private bool _isMonthly;

    void Start()
    {
        _monthlyCalendarUnits.Clear();
        _weeklyCalendarUnits.Clear();

        // Add the date units to the calendar unit list
        for (int i = 0; i < _maxNumberOfDaysMonthly; ++i)
        {
            _monthlyCalendarUnits.Add(monthlyCalendar.transform.GetChild(0).GetChild(i).gameObject);
        }

        // Add the date units to the calendar unit list
        for (int i = 0; i < _numberOfDaysInAWeek; ++i)
        {
            _weeklyCalendarUnits.Add(weeklyCalendar.transform.GetChild(0).GetChild(i).gameObject);
        }

        _focusedDateTime = DateTime.Now;

        _isMonthly = true;

        if (_isMonthly)
        {
            monthlyCalendar.SetActive(true);
            weeklyCalendar.SetActive(false);
            UpdateMonthlyCalendar();
            switchMonthlyWeeklyButton.GetComponentInChildren<Text>().text = "Monthly";
        }
        else
        {
            monthlyCalendar.SetActive(false);
            weeklyCalendar.SetActive(true);
            UpdateWeeklyCalendar();
            switchMonthlyWeeklyButton.GetComponentInChildren<Text>().text = "Weekly";
        }
    }

    // Creates the monthly calendar
    void UpdateMonthlyCalendar()
    {
        // Get the first day of the month
        DateTime firstDay = _focusedDateTime.AddDays(-(_focusedDateTime.Day - 1));
        int firstDayIndex = GetDay(firstDay.DayOfWeek);

        int date = 0;
        for(int i = 0; i < _maxNumberOfDaysMonthly; ++i)
        {
            Text label = _monthlyCalendarUnits[i].GetComponentInChildren<Text>();
            _monthlyCalendarUnits[i].SetActive(false);

            if(i >= firstDayIndex)
            {
                DateTime theDay = firstDay.AddDays(date);
                if(theDay.Month == firstDay.Month)
                {
                    _monthlyCalendarUnits[i].transform.GetChild(1).GetComponent<CalendarUnit>().SetDateTime(theDay.Date);
                                                       
                    _monthlyCalendarUnits[i].SetActive(true);

                    if (theDay.Date == DateTime.Today.Date)
                    {
                        _monthlyCalendarUnits[i].GetComponent<Image>().sprite = 
                            currentDaySprites[(int)_monthlyCalendarUnits[i].transform.GetChild(1).GetComponent<CalendarUnit>().dayType];
                    }
                    else
                    {
                        _monthlyCalendarUnits[i].GetComponent<Image>().sprite =
                            daySprites[(int)_monthlyCalendarUnits[i].transform.GetChild(1).GetComponent<CalendarUnit>().dayType];
                    }

                    label.text = (date + 1).ToString();
                    ++date;
                }
            }
        }

        yearText.text = _focusedDateTime.Year.ToString();
        monthText.text = GetMonth(_focusedDateTime.Month);
    }

    // Creates the weekly Calendar
    void UpdateWeeklyCalendar()
    {
        int dayOfWeek = GetDay(_focusedDateTime.DayOfWeek);
        // Get the first day of the month
        _focusedDateTime = _focusedDateTime.AddDays(-(dayOfWeek));
        DateTime firstDay = _focusedDateTime;

       for (int i = 0; i < _numberOfDaysInAWeek; ++i)
        {
            _weeklyCalendarUnits[i].transform.GetChild(0).GetComponent<Text>().text = firstDay.Day.ToString();
            _weeklyCalendarUnits[i].transform.GetChild(2).GetComponent<CalendarUnit>().SetDateTime(firstDay.Date);

            _monthlyCalendarUnits[i].SetActive(true);

            if (firstDay.Date == DateTime.Today.Date)
            {
                _weeklyCalendarUnits[i].GetComponent<Image>().sprite =
                    currentDaySprites[(int)_weeklyCalendarUnits[i].transform.GetChild(2).GetComponent<CalendarUnit>().dayType];
            }
            else
            {
                _weeklyCalendarUnits[i].GetComponent<Image>().sprite =
                    daySprites[(int)_weeklyCalendarUnits[i].transform.GetChild(2).GetComponent<CalendarUnit>().dayType];
            }

            firstDay = firstDay.AddDays(1);
       }

        _focusedDateTime = firstDay.AddDays(-1);
        yearText.text = _focusedDateTime.Year.ToString();
        monthText.text = GetMonth(_focusedDateTime.Month);
    }

    // Returns the day of the week in number form
    int GetDay(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 0;
            case DayOfWeek.Tuesday: return 1;
            case DayOfWeek.Wednesday: return 2;
            case DayOfWeek.Thursday: return 3;
            case DayOfWeek.Friday: return 4;
            case DayOfWeek.Saturday: return 5;
            case DayOfWeek.Sunday: return 6;
        }
        return 0;
    }

    // Returns the name of the month
    public string GetMonth(int monthNum)
    {
        switch(monthNum)
        {
            case 1:
                return "January";
            case 2:
                return "February";
            case 3:
                return "March";
            case 4:
                return "April";
            case 5:
                return "May";
            case 6:
                return "June";
            case 7:
                return "July";
            case 8:
                return "August";
            case 9:
                return "September";
            case 10:
                return "October";
            case 11:
                return "November";
            case 12:
                return "December";
        }
        return "";
    }

    // Change Month
    void ChangeMonth(int amountToAdd)
    {
        _focusedDateTime = _focusedDateTime.AddMonths(amountToAdd);
        UpdateMonthlyCalendar();
    }

    // Change Week
    void ChangeWeek(int amountToAdd)
    {
        _focusedDateTime = _focusedDateTime.AddDays(amountToAdd * _numberOfDaysInAWeek);

        UpdateWeeklyCalendar();
    }
    
    // Change Date
    public void ChangeDate(int amountToAdd)
    {
        if (_isMonthly)
            ChangeMonth(amountToAdd);
        else
            ChangeWeek(amountToAdd);
    }

    // Switch Monthly Weekly
    public void SwitchMonthlyWeekly()
    {
        if (_isMonthly)
        {   // Change to weekly calendar
            weeklyCalendar.SetActive(true);
            monthlyCalendar.SetActive(false);
            UpdateWeeklyCalendar();
            switchMonthlyWeeklyButton.GetComponentInChildren<Text>().text = "Monthly";
        }
        else
        {   // Change to monthly calendar
            weeklyCalendar.SetActive(false);
            monthlyCalendar.SetActive(true);
            UpdateMonthlyCalendar();
            switchMonthlyWeeklyButton.GetComponentInChildren<Text>().text = "Weekly";
        }

        _isMonthly = !_isMonthly;
    }
}
