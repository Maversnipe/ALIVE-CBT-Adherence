using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Linq;

public class EmotionsManager : MonoBehaviour {

    private DatabaseReference databaseRef;
    private List<MoodCheckInfo> listOfMoodCheck = new List<MoodCheckInfo>();
    private DataSnapshot _datasnapshot;

    // Use this for initialization
    void Start ()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // Set Persistence for mobile
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);
        }
        // Get the root reference location of the database.
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        LoadMood();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddMoodCheck(MoodCheckInfo _moodCheckInfo)
    {
        _moodCheckInfo.key = databaseRef.Child("moodCheck").Push().Key;
        string json = JsonUtility.ToJson(_moodCheckInfo);
        databaseRef.Child("moodCheck").Child(_moodCheckInfo.key).SetRawJsonValueAsync(json);
    }

    public void LoadMood(Action<bool> callBackFunction)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("moodCheck")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle Error
                }
                else if (task.IsCompleted)
                {
                    listOfMoodCheck.Clear();
                    _datasnapshot = task.Result;
                }
                callBackFunction(true);
            });
    }

    public void LoadMood()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("moodCheck")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle Error
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                }
            });
    }

    // Remove an activity to database
    public void RemoveMoodCheck(string _key)
    {
        databaseRef.Child("moodCheck").Child(_key).RemoveValueAsync();
    }

    public List<MoodCheckInfo> GetMoodCheck(DateTime _dateTime)
    {
        for (int i = 0; i < _datasnapshot.ChildrenCount; ++i)
        {
            MoodCheckInfo newMood = JsonUtility.FromJson<MoodCheckInfo>(_datasnapshot.Children.ToList()[i].GetRawJsonValue());
            
            DateTime currDate = Convert.ToDateTime(newMood.dateTime);
            if (currDate.Date == _dateTime)
            {
                listOfMoodCheck.Add(newMood);
            }
        }
        return listOfMoodCheck;
    }
}
