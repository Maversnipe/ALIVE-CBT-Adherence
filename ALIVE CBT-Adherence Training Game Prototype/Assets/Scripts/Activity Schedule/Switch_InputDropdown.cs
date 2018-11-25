using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch_InputDropdown : MonoBehaviour
{
    
    public GameObject userInput;
    public GameObject dropdown;
    public Button toType;
    public Button toSelect;
    public bool isType = true;

    // Set the options for user typing
    public void SetToType()
    {
        if (isType)
            return;
        userInput.SetActive(true);
        dropdown.SetActive(false);
        isType = true;

        toType.interactable = false;
        toSelect.interactable = true;
    }

    // Set the options for selection with dropdown menu
    public void SetToSelect()
    {
        if (!isType)
            return;
        userInput.SetActive(false);
        dropdown.SetActive(true);
        isType = false;

        toType.interactable = true;
        toSelect.interactable = false;
    }
}
