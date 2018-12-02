using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_ActivityMood : MonoBehaviour {

    // Scroll Panel
    public RectTransform scrollPanel;
    // Activity Schedule & Mood Check Panels
    public GameObject[] panels = new GameObject[2];
    // Contains the "Center To Scroll" GameObject
    public RectTransform center;
    
    private int targetIndex = 0;
    private const float speed = 2500f;

    // Update is called once per frame
    void Update () {
        // Check if at center
        if (panels[targetIndex].transform.position != center.position)
        {
            MoveToCenter(targetIndex);
        }
    }

    public void MoveToCenter(int index)
    {
        // Checks if the target panel is to the left or right of the center
        bool isLeft;
        if (panels[index].transform.position.x < center.position.x)
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }

        // Get direction between center and panel
        Vector3 dir = (center.position - panels[index].transform.position).normalized;

        // Move the panels towards center
        for (int i = 0; i < panels.Length; ++i)
        {
            panels[i].transform.Translate(dir * speed * Time.deltaTime);
        }

        // If the panel went pass the center
        if ((isLeft && panels[index].transform.position.x >= center.position.x)
            || (!isLeft && panels[index].transform.position.x <= center.position.x))
        {
            // Make panel be at center
            Vector3 dist = center.position - panels[index].transform.position;

            for (int i = 0; i < panels.Length; ++i)
            {
                panels[i].transform.position += dist;
            }
        }
    }

    public void ChangeTarget(int newTarget)
    {
        targetIndex = newTarget;
    }

    public void Reset()
    {
        // Make panel be at center
        Vector3 dist = center.position - panels[0].transform.position;

        for (int i = 0; i < panels.Length; ++i)
        {
            panels[i].transform.position += dist;
        }

        // Set target back to 0
        targetIndex = 0;
    }
}
