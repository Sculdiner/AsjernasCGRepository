using System;
using System.Collections;
using System.Collections.Generic;
using AsjernasCG.Common.BusinessModels.CardModels;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public ClientSideCard ControllingCard { get; set; }
    private DraggingActions actions;
    public string DraggingActionName;

    // Use this for initialization
    void Start()
    {
        switch (ControllingCard.CardStats.CardType)
        {
            case CardType.Follower:
                SetAction<FollowerCastDragBehaviour>();
                break;
            case CardType.Equipment:
                SetAction<EquipmentCastDragBehaviour>();
                break;
            case CardType.Ability:
                SetAction<AbilityCastDragBehaviour>();
                break;
            case CardType.Event:
                if (ControllingCard.CardStats.CastType == CardCastType.Target)
                {
                    SetAction<EventTargetingBehaviour>();
                }
                else
                {
                    //SetAction<EventCastDragBehaviour>();
                    SetAction<EventTargetingBehaviour>();
                }
                break;
            case CardType.Character:
                SetAction<CharacterBoardDragBehaviour>();
                break;
            default:
                //SetAction<FollowerCastDragBehaviour>();
                break;
        }
    }

    public void SetAction<T>() where T : DraggingActions
    {
        //what to do if the set action is called while a dragging operation takes place
        actions?.KillCurrentActions();
        var action = (T)Activator.CreateInstance(typeof(T), ControllingCard);
        actions = action;
        DraggingActionName = actions.ToString();
        //we will encounter the problem where the card dragging type is instansiated ON the mouse so the mouse enter will not trigger!!!! maybe trigger it manually: OnMouseEnter()
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
        actions?.OnEndDrag();
    }

}
