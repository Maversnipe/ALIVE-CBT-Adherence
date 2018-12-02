using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodRatingSecond : MonoBehaviour {
    public Button[] intensityButtons = new Button[4];
    public Slider intensitySlider;
    public Text intensityValue;
    public MoodCheckManager emotionsManager;
    public Button next;

    private int selectedEmotionIndex = -1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SetEmotions()
    {
        
        for(int i = 0; i < emotionsManager.listOfPlayerEmotions.Count; ++i)
        {
            emotionsManager.listOfPlayerEmotions[i].intensity = 0;
            switch (emotionsManager.listOfPlayerEmotions[i].emotionType)
            {
                case EmotionInfo.EmotionType.Depressed:
                    intensityButtons[0].gameObject.SetActive(true);
                    if(i == 0)
                    {
                        intensityButtons[0].GetComponent<Image>().color = intensityButtons[0].colors.disabledColor;
                    }
                    break;
                case EmotionInfo.EmotionType.Anxious:
                    intensityButtons[1].gameObject.SetActive(true);
                    if (i == 0)
                    {
                        intensityButtons[1].GetComponent<Image>().color = intensityButtons[1].colors.disabledColor;
                    }
                    break;
                case EmotionInfo.EmotionType.Angry:
                    intensityButtons[2].gameObject.SetActive(true);
                    if (i == 0)
                    {
                        intensityButtons[2].GetComponent<Image>().color = intensityButtons[2].colors.disabledColor;
                    }
                    break;
                case EmotionInfo.EmotionType.Happy:
                    intensityButtons[3].gameObject.SetActive(true);
                    if (i == 0)
                    {
                        intensityButtons[3].GetComponent<Image>().color = intensityButtons[3].colors.disabledColor;
                    }
                    break;
            }
        }

        selectedEmotionIndex = 0;
        intensitySlider.value = 0;
        intensityValue.text = intensitySlider.value.ToString();
    }


    public void UpdateIntensityValue()
    {
        intensityValue.text = intensitySlider.value.ToString();
        emotionsManager.listOfPlayerEmotions[selectedEmotionIndex].intensity = (int)intensitySlider.value;
    }
    
    public void IntensityButtonClicked(int _selected)
    {
        for (int i = 0; i < emotionsManager.listOfPlayerEmotions.Count; ++i)
        {
            if (emotionsManager.listOfPlayerEmotions[i].emotionType == (EmotionInfo.EmotionType)_selected)
            {
                selectedEmotionIndex = i;
                break;
            }
        }

        if (selectedEmotionIndex == -1)
        {
            Debug.Log("Selected Emotion Index has no value!");
            return;
        }

        for (int i = 0; i < intensityButtons.Length; ++i)
        {
            if (i != _selected)
            {
                // Set button selected
                intensityButtons[i].GetComponent<Image>().color = intensityButtons[i].colors.normalColor;
            }
            else
            {
                // Set button not selected
                intensityButtons[i].GetComponent<Image>().color = intensityButtons[i].colors.disabledColor;
                // Set the intensity value
                intensitySlider.value = emotionsManager.listOfPlayerEmotions[selectedEmotionIndex].intensity;
                intensityValue.text = intensitySlider.value.ToString();
            }
        }
    }
    
    public void Reset()
    {
        intensitySlider.value = 0;
        intensityValue.text = intensitySlider.value.ToString();
        for (int i = 0; i < intensityButtons.Length; ++i)
        {
            intensityButtons[i].gameObject.SetActive(false);
        }
        intensitySlider.gameObject.SetActive(false);
    }
}
