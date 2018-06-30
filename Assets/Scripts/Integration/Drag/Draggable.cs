using System;
using System.Collections;
using System.Collections.Generic;
using AsjernasCG.Common.BusinessModels.CardModels;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public ClientSideCard ControllingCard { get; set; }

    public Action OnMouseUpEvents;
    private DraggingActions actions;
    public BoardManager BoardManager;
    private int CardLayerMask;
    public string DraggingActionName;

    public void SetAction<T>() where T : DraggingActions
    {
        //what to do if the set action is called while a dragging operation takes place
        actions?.KillCurrentActions();
        var action = (T)Activator.CreateInstance(typeof(T), ControllingCard);
        actions = action;
        DraggingActionName = actions.ToString();
        //we will encounter the problem where the card dragging type is instansiated ON the mouse so the mouse enter will not trigger!!!! maybe trigger it manually: OnMouseEnter()
    }

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        CardLayerMask = LayerMask.GetMask("RaycastEligibleTargets");
        OnMouseUpEvents = () => { };
        switch (ControllingCard.CardStats.CardType)
        {
            case CardType.Follower:
                //SetAction<FollowerDragBehaviour>();
                break;
            //case CardType.Equipment:
            //    break;
            //case CardType.Ability:
            //    break;
            case CardType.Event:
                if (ControllingCard.CardStats.CastType == CardCastType.Field)
                    SetAction<EventDragBehaviour>();
                else if (ControllingCard.CardStats.CastType == CardCastType.Target)
                    SetAction<EventTargetingBehaviour>();
                break;
            //case CardType.Character:
            //    break;
            //case CardType.Action:
            //    break;
            //case CardType.Location:
            //    break;
            //case CardType.Minion:
            //    break;
            //case CardType.Resource:
            //    break;
            //case CardType.Quest:
            //    break;
            //case CardType.Objective:
            //    break;
            //case CardType.Enviromental:
            //    break;
            default:
                SetAction<EventTargetingBehaviour>();
                //SetAction<FollowerDragBehaviour>();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseEnter()
    {

    }
    public void OnMouseDown()
    {
        actions?.OnStartDrag();
    }

    public void OnMouseDrag()
    {

        actions?.OnDraggingInUpdate();
    }

    public void OnMouseUp()
    {
        if (OnMouseUpEvents != null)
        {
            OnMouseUpEvents.Invoke();
        }
        actions?.OnEndDrag();
    }

}
