using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MoodCheckInfo
{
    public string key;
    public string dateTime;
    public EmotionInfo[] emotionsFeltBefore;
    public EmotionInfo[] emotionsFeltAfter;

    public bool moodDiaryActive;
    public bool posThoughtsJournalActive;
    public bool worryDiaryActive;
    public bool angerDiaryActive;

    public MoodDiaryInfo moodDiaryInfo;
    public WorryDiaryInfo worryDiaryInfo;
    public AngerDiaryInfo angerDiaryInfo;
    public PositiveThoughtsJournalInfo posThoughtsJournalInfo;

    public void SetMoodCheckInfo(MoodCheckInfo _moodCheckInfo)
    {
        key = _moodCheckInfo.key;
        dateTime = _moodCheckInfo.dateTime;
        emotionsFeltBefore = _moodCheckInfo.emotionsFeltBefore;
        emotionsFeltAfter = _moodCheckInfo.emotionsFeltAfter;

        moodDiaryActive = _moodCheckInfo.moodDiaryActive;
        posThoughtsJournalActive = _moodCheckInfo.posThoughtsJournalActive;
        worryDiaryActive = _moodCheckInfo.worryDiaryActive;
        angerDiaryActive = _moodCheckInfo.angerDiaryActive;

        moodDiaryInfo = _moodCheckInfo.moodDiaryInfo;
        worryDiaryInfo = _moodCheckInfo.worryDiaryInfo;
        angerDiaryInfo = _moodCheckInfo.angerDiaryInfo;
        posThoughtsJournalInfo = _moodCheckInfo.posThoughtsJournalInfo;
    }
}

public class MoodCheckInfoComparer : IComparer<MoodCheckInfo>
{
    public int Compare(MoodCheckInfo x, MoodCheckInfo y)
    {
        DateTime xDateTime = Convert.ToDateTime(x.dateTime);
        DateTime yDateTime = Convert.ToDateTime(y.dateTime);
        if (xDateTime > yDateTime)
            return 1;
        else if (xDateTime < yDateTime)
            return -1;
        else
            return 0;
    }
}

[Serializable]
public class MoodDiaryInfo
{
    // Questions
    public string Question_Situation;
    public string Question_PhysicalSensation;
    public string Question_UnhelpfulThoughts;
    public string[] Question_ChallengeThoughts;

    // Answers
    public string Answer_Situation;
    public string Answer_PhysicalSensation;
    public string Answer_UnhelpfulThoughts;
    public string[] Answer_ChallengeThoughts;
}

[Serializable]
public class AngerDiaryInfo
{
    // Questions
    public string[] Question_Situation;
    public string Question_PhysicalSensation;
    public string Question_UnhelpfulThoughts;
    public string[] Question_ChallengeThoughts;

    // Answers
    public string[] Answer_Situation;
    public string Answer_PhysicalSensation;
    public string Answer_UnhelpfulThoughts;
    public string[] Answer_ChallengeThoughts;
}

[Serializable]
public class WorryDiaryInfo
{
    // Questions
    public string Question_Situation;
    public string[] Question_ChallengeThoughts;

    // Answers
    public string Answer_Situation;
    public string[] Answer_ChallengeThoughts;
}

[Serializable]
public class PositiveThoughtsJournalInfo
{
    // Questions
    public string Question_Situation;

    // Answers
    public string Answer_Situation;
}

[Serializable]
public class EmotionInfo
{

    public EmotionType emotionType;
    public int intensity = 0;

    public enum EmotionType
    {
        Depressed = 0,
        Anxious,
        Angry,
        Happy
    }

    public EmotionInfo(int _emotionIndex)
    {
        switch (_emotionIndex)
        {
            case 0:
                emotionType = EmotionType.Depressed;
                break;
            case 1:
                emotionType = EmotionType.Anxious;
                break;
            case 2:
                emotionType = EmotionType.Angry;
                break;
            case 3:
                emotionType = EmotionType.Happy;
                break;
        }
    }
}
