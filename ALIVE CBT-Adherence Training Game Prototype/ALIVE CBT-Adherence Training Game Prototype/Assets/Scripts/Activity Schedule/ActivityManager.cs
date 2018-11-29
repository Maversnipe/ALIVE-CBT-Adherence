using System.Collections.Generic;
using System;
using UnityEngine;
using Firebase.Database;
using System.Linq;

public class ActivityManager : MonoBehaviour {

    private string _connectionString;
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


///////////////////
//// SQL Stuff ////
///////////////////

//// Path to the database
//connectionString = "URI=file:" + Application.persistentDataPath + "/Databases.db";

//        // Checks if the table exists
//        // Establish connection with database
//        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
//        {
//            dbConnection.Open();

//            // Start a command
//            using (IDbCommand dbCommand = dbConnection.CreateCommand())
//            {
//                // This checks if the table exists
//                // If table doesn't exist, create new table
//                // Query that will be sent to sqlite
//                string sqlQuery = String.Format("create table if not exists ActivityDatabase (" +
//                    "'name' TEXT, 'description' TEXT, 'startHour' INTEGER, 'startMinute' INTEGER," +
//                    "'endHour' INTEGER, 'endMinute' INTEGER, 'date' DATETIME, 'activityID' INTEGER);");

//// Set the command to be the query
//dbCommand.CommandText = sqlQuery;
//                // Execute query
//                dbCommand.ExecuteScalar();
//                dbConnection.Close();
//            }
//        }

//// Add activity to database
//public void AddActivity(ActivityInfo _activity)
//{
//    // Establish connection with database
//    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
//    {
//        dbConnection.Open();

//        // Start a command
//        using (IDbCommand dbCommand = dbConnection.CreateCommand())
//        {
//            // Query that will be sent to sqlite
//            string sqlQuery = String.Format("insert into ActivityDatabase(name, description, startHour, startMinute, endHour, endMinute, date, activityID) "
//                + "values(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\")",
//                _activity.name, _activity.description, _activity.startTime.Hour, _activity.startTime.Minute,
//                _activity.endTime.Hour, _activity.endTime.Minute, _activity.date, _activity.activityID);

//            // Set the command to be the query
//            dbCommand.CommandText = sqlQuery;
//            // Execute query
//            dbCommand.ExecuteScalar();
//            dbConnection.Close();
//        }
//    }
//}

//// Remove an activity to database
//public void RemoveActivity(int _id)
//{
//    // Establish connection with database
//    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
//    {
//        dbConnection.Open();

//        // Start a command
//        using (IDbCommand dbCommand = dbConnection.CreateCommand())
//        {
//            // Query that will be sent to sqlite
//            string sqlQuery = String.Format("delete from ActivityDatabase where activityID = \"{0}\"", _id);

//            // Set the command to be the query
//            dbCommand.CommandText = sqlQuery;
//            // Execute query
//            dbCommand.ExecuteScalar();
//            dbConnection.Close();
//        }
//    }
//}

//// Get activities from database based on date
//public List<ActivityInfo> GetActivities(DateTime _dateTime)
//{
//    List<ActivityInfo> listOfActivities = new List<ActivityInfo>();
//    listOfActivities.Clear();
//    // Establish connection with database
//    using (IDbConnection dbConnection = new SqliteConnection(connectionString))
//    {
//        dbConnection.Open();

//        // Start a command
//        using (IDbCommand dbCommand = dbConnection.CreateCommand())
//        {
//            // Query that will be sent to sqlite
//            string sqlQuery = "select * from ActivityDatabase";

//            // Set the command to be the query
//            dbCommand.CommandText = sqlQuery;

//            // Execute the reading command
//            using (IDataReader reader = dbCommand.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    DateTime toCheck = DateTime.Parse(reader.GetString(6));
//                    // Checks if the date is the same as the required date
//                    if (toCheck.Date == _dateTime.Date)
//                    {
//                        // Create activity
//                        ActivityInfo newActivity = new ActivityInfo();
//                        newActivity.name = reader.GetString(0);
//                        newActivity.description = reader.GetString(1);
//                        newActivity.startTime = new ActivityTime(reader.GetInt32(2), reader.GetInt32(3));
//                        newActivity.endTime = new ActivityTime(reader.GetInt32(4), reader.GetInt32(5));
//                        newActivity.date = _dateTime;
//                        newActivity.activityID = reader.GetInt32(7);

//                        // Add activity to list
//                        listOfActivities.Add(newActivity);
//                    }
//                }

//                dbConnection.Close();
//                reader.Close();
//            }
//        }
//    }

//    return listOfActivities;
//}
