using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScroll : MonoBehaviour {

    // Maximum number of slots in the scroll rect content
    public int maxSlots;
    // Height of the slot
    public int slotHeight;
    // The content refers to the gameobject that contains all of the different slots
    public GameObject content;
    // Time type
    public TimeType timeType;

    public enum TimeType
    {
        Hour,
        Minute
    }

    private int _targetIndex = 0;
    // To check if it is moving to the target
    private bool _moveToTarget = false;
    // Contains all slots
    private GameObject[] _slots;
    // Speed of scroll when the slots auto scroll
    private float _scrollSpeed = 150f;
    // To check if it is moving at all
    private bool _isMoving;
    private float _targetYPos;
    private Vector3 _originalLocalPos;
    private Vector3 _originalPos;
    private bool _isDragging;

    // Use this for initialization
    void Start () {
        _slots = new GameObject[maxSlots];
        // Add all the content slots into the array
        for(int i = 0; i < content.transform.childCount; ++i)
        {
            _slots[i] = content.transform.GetChild(i).gameObject;
        }
        _isMoving = false;
        _originalLocalPos = content.transform.localPosition;
        _originalPos = content.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(this.GetComponent<ScrollRect>().velocity.magnitude != 0)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        if(_isDragging)
        {
            _moveToTarget = false;
            _isMoving = true;
        }

		if(!_moveToTarget && _isMoving && !_isDragging && this.GetComponent<ScrollRect>().velocity.magnitude < 100f)
        {
            CheckClosest();
        }

        if(_moveToTarget)
        {
            MoveToTarget(_targetIndex);
        }
	}

    public void MoveToTarget(int _index)
    {
        Vector3 dir;
        bool isAbove;

        if(content.transform.localPosition.y > _targetYPos)
        {
            // If the target is above center, make dir be downwards
            dir = new Vector3(0, -1);
            isAbove = true;
        }
        else
        {
            // If the target is above center, make dir be upwards
            dir = new Vector3(0, 1);
            isAbove = false;
        }

        // Move content
        content.transform.Translate(dir * _scrollSpeed * Time.deltaTime);
        
        // Check if the target slot has moved pass the center
        if((isAbove && content.transform.localPosition.y < _targetYPos)
            || (!isAbove && content.transform.localPosition.y > _targetYPos))
        {
            content.transform.Translate(new Vector3(0, _targetYPos - content.transform.localPosition.y));
            _moveToTarget = false;
            _isMoving = false;
            this.GetComponent<ScrollRect>().velocity = new Vector2(0, 0);
        }
    }

    public void CheckClosest()
    {
        // The height of the content
        float heightOfContent = maxSlots * slotHeight;
        float yPosOfContent = content.transform.localPosition.y - _originalLocalPos.y;
        _targetIndex = (int)Math.Round(((yPosOfContent / heightOfContent) * maxSlots), 0);
        if (_targetIndex < 0)
            _targetIndex = 0;
        else if (_targetIndex >= maxSlots)
            _targetIndex = maxSlots - 1;
        _targetYPos = _targetIndex * slotHeight + _originalLocalPos.y;
        _moveToTarget = true;
    }

    // Set is dragging
    public void SetIsDragging(bool _drag)
    {
        _isDragging = _drag;
    }

    // Getter for target index
    public int GetTargetIndex()
    {
        return _targetIndex;
    }

    public void ResetScroll()
    {
        content.transform.localPosition = _originalLocalPos;
        content.transform.position = _originalPos;
        _targetIndex = 0;
    }
}
