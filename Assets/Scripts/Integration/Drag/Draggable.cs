using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public ClientSideCard ControllingCard { get; set; }
    private DraggingActions actions;
    private Vector3 screenSpace;
    private Vector3 offset;
    public BoardManager BoardManager;
    private bool IsOverATarget;
    private int CardLayerMask;

    public void SetAction<T>() where T: DraggingActions
    {
        //what to do if the set action is called while a dragging operation takes place
        actions?.KillCurrentActions();
        var action = (T)Activator.CreateInstance(typeof(T), ControllingCard);
        actions = action;
        //we will encounter the problem where the card dragging type is instansiated ON the mouse so the mouse enter will not trigger!!!! maybe trigger it manually: OnMouseEnter()
    }

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        //actions = GetComponent<DraggingActions>();
        CardLayerMask = LayerMask.GetMask("RaycastEligibleTargets");
        OnMouseUpEvents = () => { };
    }

    public ClientSideCard TargetedCard;

    // Update is called once per frame
    void Update()
    {
        actions?.OnDraggingInUpdate();

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //var hits = Physics.RaycastAll(ray, 30f, CardLayerMask);

        //if (TargetedCard != null)
        //{
        //    Debug.DrawLine(ray.origin, TargetedCard.CardViewObject.transform.position, Color.green);
        //}
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    RaycastHit hit = hits[i];
        //    Debug.DrawLine(ray.origin, hit.transform.position, Color.cyan);
        //}

        //if (hits.Length > 0)
        //{
        //    //We have not saved a card. Check if the hits collided with a valid target
        //    for (int i = 0; i < hits.Length; i++)
        //    {

        //        var card = BoardManager.GetCard(hits[i]);
        //        if (card != null && card.CardStats.GeneratedCardId != ControllingCard.CardStats.GeneratedCardId)
        //        {
        //            if (TargetedCard != card)
        //            {
        //                //mouse enter
        //            }
        //            //IsOverATarget = true;
        //            TargetedCard = card;
        //            Debug.Log("hit card " + TargetedCard.CardStats.GeneratedCardId);
        //            return;
        //        }
        //    }
        //}
        //if (TargetedCard != null)
        //{
        //    //mouse exit
        //    Debug.Log("exited card " + TargetedCard.CardStats.GeneratedCardId);
        //    TargetedCard = null;
        //}
    }

    public void OnMouseDown()
    {
        ControllingCard.IsUnderPlayerControl = true;
        //actions.OnStartDrag();
        //translate the cubes position from the world to Screen Point
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

    }

    public void OnMouseDrag()
    {
        //actions.OnDraggingInUpdate();
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
