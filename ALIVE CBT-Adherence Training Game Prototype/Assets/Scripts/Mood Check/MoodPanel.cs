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

    public GameObject manager;
    public GameObject moodCheckMenu;
    
    [HideInInspector]
    public CalendarUnit calendarUnit;

    [HideInInspector]
    public MoodCheckInfo moodCheckInfo;

    // Timer for the spawning of the 
    // Questions and answers
    private float _timer = 0f;
    private float _bounceTime = 0f;
    private bool _loadingQna = false;

    private bool _isDiaryOpen = false;

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
        _isDiaryOpen = true;
        diariesButtons.SetActive(false);
        diariesEntry.SetActive(true);
        switch (_diaryNum)
        {
            case 0:
                // Depressed
                diaryName.text = "Mood Journal";
                OpenMoodDiary();
                break;
            case 1:
                // Anxious
                diaryName.text = "Worry Journal";
                OpenWorryDiary();
                break;
            case 2:
                // Angry
                diaryName.text = "Anger Journal";
                OpenAngerDiary();
                break;
            case 3:
                // Happy
                diaryName.text = "Positive Thoughts Journal";
                OpenHappyDiary();
                break;
        }
    }

    public void OpenMoodDiary()
    {
        MoodDiaryInfo newDiaryInfo = moodCheckInfo.moodDiaryInfo;

        GameObject newQNA = Instantiate(qnaPrefab, content.transform);
        newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_Situation;
        newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_Situation;

        newQNA = Instantiate(qnaPrefab, content.transform);
        newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_PhysicalSensation;
        newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_PhysicalSensation;

        for (int i = 0; i < newDiaryInfo.Question_UnhelpfulThoughts.Length; ++i)
        {
            if (newDiaryInfo.Question_UnhelpfulThoughts[i] == "")
                continue;
            newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_UnhelpfulThoughts[i];
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_UnhelpfulThoughts[i];
        }

        for (int i = 0; i < newDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            if (newDiaryInfo.Question_ChallengeThoughts[i] == "")
                continue;
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

    public void OpenWorryDiary()
    {
        WorryDiaryInfo newDiaryInfo = moodCheckInfo.worryDiaryInfo;

        GameObject newQNA = Instantiate(qnaPrefab, content.transform);
        newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_Situation;
        newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_Situation;

        for(int i = 0; i < newDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            if (newDiaryInfo.Question_ChallengeThoughts[i] == "")
                continue;
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
        _isDiaryOpen = false;
        diariesButtons.SetActive(true);
        diariesEntry.SetActive(false);

        for (int i = content.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    public void OpenAngerDiary()
    {
        AngerDiaryInfo newDiaryInfo = moodCheckInfo.angerDiaryInfo;

        GameObject newQNA = Instantiate(qnaPrefab, content.transform);
        newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_Situation;
        newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_Situation;

        newQNA = Instantiate(qnaPrefab, content.transform);
        newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_PhysicalSensation;
        newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_PhysicalSensation;

        for (int i = 0; i < newDiaryInfo.Question_UnhelpfulThoughts.Length; ++i)
        {
            if (newDiaryInfo.Question_UnhelpfulThoughts[i] == "")
                continue;
            newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = newDiaryInfo.Question_UnhelpfulThoughts[i];
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Answer_UnhelpfulThoughts[i];
        }

        for (int i = 0; i < newDiaryInfo.Question_ChallengeThoughts.Length; ++i)
        {
            if (newDiaryInfo.Question_ChallengeThoughts[i] == "")
                continue;
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

    public void OpenHappyDiary()
    {
        PositiveThoughtsJournalInfo newDiaryInfo = moodCheckInfo.posThoughtsJournalInfo;

        if(newDiaryInfo.People_Check)
        {
            GameObject newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = "Share about a person that made you feel good!";
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.People;
        }

        if (newDiaryInfo.Food_Check)
        {
            GameObject newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = "Share about a food that made you feel good after eating!";
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Food;
        }

        if(newDiaryInfo.Place_Check)
        {
            GameObject newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = "Share about a place that made you feel safe or calm!";
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Place;
        }

        if(newDiaryInfo.Events_Check)
        {
            GameObject newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = "Share about an event that made you feel good!";
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Events;
        }

        if(newDiaryInfo.Hobbies_Check)
        {
            GameObject newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = "Share about something that you enjoy doing!";
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.Hobbies;
        }

        if(newDiaryInfo.CountYourBlessings_Check)
        {
            GameObject newQNA = Instantiate(qnaPrefab, content.transform);
            newQNA.transform.GetChild(0).GetComponent<Text>().text = "Share about something that you would like to be grateful or thankful for!";
            newQNA.transform.GetChild(1).GetComponent<Text>().text = newDiaryInfo.CountYourBlessings;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
        content.SetActive(false);
        _bounceTime = 0.1f;
        _timer = 0f;
        _loadingQna = true;
    }

    public void Back()
    {
        if (_isDiaryOpen)
        {
            CloseDiary();
        }
        else
        {
            manager.GetComponent<SceneManager_Calendar>().OpenDayMenu();
            this.gameObject.SetActive(false);
            moodCheckMenu.GetComponent<MoodCheckMenu>().StartLoadActivities();
        }
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

    public void SetMoodPanel(MoodCheckInfo _moodCheckInfo)
    {
        this.moodCheckInfo = _moodCheckInfo;

        if (!this.moodCheckInfo.moodDiaryActive)
            diariesButtons.transform.GetChild(0).GetComponent<Button>().interactable = false;

        if (!this.moodCheckInfo.worryDiaryActive)
            diariesButtons.transform.GetChild(1).GetComponent<Button>().interactable = false;

        if (!this.moodCheckInfo.angerDiaryActive)
            diariesButtons.transform.GetChild(2).GetComponent<Button>().interactable = false;

        if (!this.moodCheckInfo.posThoughtsJournalActive)
            diariesButtons.transform.GetChild(3).GetComponent<Button>().interactable = false;
    }
}
