using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodRating : MonoBehaviour {

    public Button[] emotions = new Button[4];
    public Button[] intensityButtons = new Button[4];
    public Slider intensitySlider;
    public Text intensityValue;
    public MoodCheckManager emotionsManager;
    public Button next;

    private int selectedEmotionIndex = -1;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (selectedEmotionIndex != -1)
        {
            emotionsManager.next.interactable = true;
        }
        else
        {
            emotionsManager.next.interactable = false;
        }
	}

    public void ButtonClicked(int _selected)
    {
        // Check if the button is selected
        if (emotions[_selected].GetComponent<Image>().color == emotions[_selected].colors.normalColor)
        {   // If not selected
            // Prevent from selecting more than 2 buttons
            if (emotionsManager.listOfPlayerEmotions.Count >= 2)
                return;
            // Set button selected
            emotions[_selected].GetComponent<Image>().color = emotions[_selected].colors.disabledColor;
            // Set intensity button as active
            intensityButtons[_selected].gameObject.SetActive(true);
            // Add to selected list
            EmotionInfo newEmotion = new EmotionInfo(_selected);
            emotionsManager.listOfPlayerEmotions.Add(newEmotion);
            // Set Slider as active
            intensitySlider.gameObject.SetActive(true);
            // Check if this is the first emotion
            if(emotionsManager.listOfPlayerEmotions.Count == 1)
            {
                // Highlight button
                intensityButtons[_selected].GetComponent<Image>().color = intensityButtons[_selected].colors.disabledColor;
                // Set the intensity value
                intensitySlider.value = newEmotion.intensity;
                intensityValue.text = intensitySlider.value.ToString();
                next.interactable = true;
                selectedEmotionIndex = 0;
            }
        }
        else
        {   // If selected
            // Set button not selected by changing colour
            emotions[_selected].GetComponent<Image>().color = emotions[_selected].colors.normalColor;
            // Set intensity button as active
            intensityButtons[_selected].gameObject.SetActive(false);
            // Remove from selected list
            for(int i = emotionsManager.listOfPlayerEmotions.Count - 1; i >= 0; --i)
            {
                if (emotionsManager.listOfPlayerEmotions[i].emotionType == (EmotionInfo.EmotionType)_selected)
                { 
                    emotionsManager.listOfPlayerEmotions.RemoveAt(i);
                    intensityButtons[_selected].GetComponent<Image>().color = intensityButtons[_selected].colors.normalColor;
                    break;
                }
            }
            
            // Check if there is still another intensity button
            if (emotionsManager.listOfPlayerEmotions.Count == 0)
            {   // If there are no more intensity buttons, remove slider
                intensitySlider.gameObject.SetActive(false);
                next.interactable = false;
                selectedEmotionIndex = -1;
            }
            else
            {
                // Light up the remaining emotion
                switch(emotionsManager.listOfPlayerEmotions[0].emotionType)
                {
                    case EmotionInfo.EmotionType.Depressed:
                        intensityButtons[0].GetComponent<Image>().color = intensityButtons[0].colors.disabledColor;
                        break;
                    case EmotionInfo.EmotionType.Anxious:
                        intensityButtons[1].GetComponent<Image>().color = intensityButtons[1].colors.disabledColor;
                        break;
                    case EmotionInfo.EmotionType.Angry:
                        intensityButtons[2].GetComponent<Image>().color = intensityButtons[2].colors.disabledColor;
                        break;
                    case EmotionInfo.EmotionType.Happy:
                        intensityButtons[3].GetComponent<Image>().color = intensityButtons[3].colors.disabledColor;
                        break;
                }
            }
        }
    }

    public void IntensityButtonClicked(int _selected)
    {
        for(int i = 0; i < emotionsManager.listOfPlayerEmotions.Count; ++i)
        {
            if (emotionsManager.listOfPlayerEmotions[i].emotionType == (EmotionInfo.EmotionType)_selected)
            {
                selectedEmotionIndex = i;
                break;
            }
        }

        if(selectedEmotionIndex == -1)
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

    public void UpdateIntensityValue()
    {
        intensityValue.text = intensitySlider.value.ToString();
        emotionsManager.listOfPlayerEmotions[selectedEmotionIndex].intensity = (int)intensitySlider.value;
    }

    public void Reset()
    {
        intensitySlider.value = 0;
        intensitySlider.gameObject.SetActive(false);
        intensityValue.text = intensitySlider.value.ToString();
        for(int i = 0; i < intensityButtons.Length; ++i)
        {
            intensityButtons[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < emotions.Length; ++i)
        {
            emotions[i].GetComponent<Image>().color = emotions[i].colors.normalColor;
        }
    }
}