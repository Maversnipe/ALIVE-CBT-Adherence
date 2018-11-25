using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodPanel : MonoBehaviour {

    public GameObject qnaPrefab;
    public GameObject content;
    public GameObject diariesButtons;
    public GameObject diariesEntry;
    public Text date;
    public Text diaryName;
    
    [HideInInspector]
    public CalendarUnit calendarUnit;

    [HideInInspector]
    public MoodCheckInfo moodCheckInfo;

    // Timer for the spawning of the 
    // Questions and answers
    private float _timer = 0f;
    private float _bounceTime = 0f;
    private bool _loadingQna = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_loadingQna)
        {
            _timer += Time.deltaTime;
            if(_timer > _bounceTime)
            {
                _loadingQna = false;
                content.SetActive(true);
            }
        }
	}

    public void OpenDiary(int _diaryNum)
    {
        diariesButtons.SetActive(false);
        diariesEntry.SetActive(true);
        switch (_diaryNum)
        {
            case 0:
                // Depressed
                diaryName.text = "Mood Journal";

                break;
            case 1:
                // Anxious
                diaryName.text = "Worry Journal";
                OpenWorryDiary();

                break;
            case 2:
                // Angry
                diaryName.text = "Anger Journal";
                break;
            case 3:
                // Happy
                diaryName.text = "Count Your Blessings";
                break;
        }
    }

    public void OpenMoodDiary()
    {
        MoodDiaryInfo newDiaryInfo = moodCheckInfo.moodDiaryInfo;
        
    }

    public void OpenWorryDiary()
    {
        WorryDiaryInfo newDiaryInfo = moodCheckInfo.worryDiaryInfo;

        GameObject newQNA = Instantiate(qnaPrefab, content.transform);
        newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_Situation;
        newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_Situation;

        for(int i = 0; i < newDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_ChallengeThoughts[i];
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_ChallengeThoughts[i];
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
        content.SetActive(false);
        _bounceTime = 0.1f;
        _timer = 0f;
        _loadingQna = true;
    }

    public void CloseDiary()
    {
        for (int i = content.transform.childCount - 1; i > 0; i--)
        {
            Destroy(content.transform.GetChild(i));
        }
    }

    public void OpenAngerDiary()
    {

    }

    public void OpenHappyDiary()
    {

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
}
