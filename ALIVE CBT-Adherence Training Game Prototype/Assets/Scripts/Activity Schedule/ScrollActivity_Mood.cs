using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollActivity_Mood : MonoBehaviour {

    // Scroll View Panel
    public RectTransform scrollPanel;
    // Activity Schedule & Mood Check Panels
    public GameObject[] panels = new GameObject[2]; 
    // Contains the "Center To Scroll" GameObject
    public RectTransform center;

    // Check if player is draggin the UI
    private bool isDragging = false;
    private bool isAtCenter = true;
    private int targetIndex = 0;
    private Vector3 startPos = new Vector3();
    private Vector3 endPos = new Vector3();
    private Vector3 dragDir = new Vector3();
    private const float speed = 2500f;
    private const float minDragMagnitude = 10f;

    void Update()
    {
        // Check if at center
        if(panels[targetIndex].transform.position == center.position)
        {
            isAtCenter = true;
        }
        else
        {
            isAtCenter = false;
        }

        // If not dragging and target panel is not centered
        if(!isDragging && !isAtCenter)
        {
            MoveToCenter(targetIndex);
        }
    }

    public void MoveToCenter(int index)
    {
        // Checks if the target panel is to the left or right of the center
        bool isLeft;
        if(panels[index].transform.position.x < center.position.x)
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

            for(int i = 0; i < panels.Length; ++i)
            {
                panels[i].transform.position += dist;
                isAtCenter = true;
            }
        }
    }

    // Start Dragging
    public void StartDragging()
    {
        startPos = Input.mousePosition;
        isDragging = true;
    }

    // Stop Dragging
    public void StopDragging()
    {
        endPos = Input.mousePosition;
        isDragging = false;
    }

    // Check the direction of drag
    public void CheckDragDir()
    {
        if ((endPos - startPos).magnitude < minDragMagnitude)
            return;
        dragDir = (endPos - startPos).normalized;

        // Check which panel the current target is
        switch(targetIndex)
        {
            // Left Panel
            case 0:
                // If the drag direction is to the left
                if (dragDir.x < 0)
                    targetIndex = 1;
                break;
            // Right Panel
            case 1:
                // If the drag direction is to the right
                if (dragDir.x > 0)
                    targetIndex = 0;
                break;
        }
    }
}
