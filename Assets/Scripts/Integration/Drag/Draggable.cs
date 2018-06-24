﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public ClientSideCard ControllingCard { get; set; }
    private DraggingActions actions;
    private Vector3 screenSpace;
    private Vector3 offset;

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        actions = GetComponent<DraggingActions>();
        OnMouseUpEvents = () => { };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExternallyTriggerMouseDown()
    {
        Debug.Log("externally triggered mouse down");
        ControllingCard.IsUnderPlayerControl = true;
        //translate the cubes position from the world to Screen Point
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }

    public void OnMouseDown()
    {
        ControllingCard.IsUnderPlayerControl = true;
        actions.OnStartDrag();
        //translate the cubes position from the world to Screen Point
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

    }

    public void OnMouseDrag()
    {
        actions.OnDraggingInUpdate();
        //keep track of the mouse position
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        //update the position of the object in the world
        transform.position = curPosition;
    }

    public Action OnMouseUpEvents;

    public void OnMouseEnter()
    {

    }

    public void OnMouseUp()
    {
        ControllingCard.IsUnderPlayerControl = false;
        if (OnMouseUpEvents != null)
        {
            OnMouseUpEvents.Invoke();
        }
        actions.OnEndDrag();
        //logic 
    }

}
