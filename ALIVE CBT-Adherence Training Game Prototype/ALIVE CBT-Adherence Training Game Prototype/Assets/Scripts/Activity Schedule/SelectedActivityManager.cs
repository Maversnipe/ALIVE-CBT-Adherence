using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedActivityManager : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenActivityMenu()
    {
        for(int i = 0; i < this.transform.childCount; ++i)
        {
            // Set the "Activity Menu" as true
            this.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            // Set the "Add Activity Menu" as false
            this.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void OpenAddActivityMenu()
    {
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            // Set the "Activity Menu" as false
            this.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            // Set the "Add Activity Menu" as true
            this.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
        }
    }
}
