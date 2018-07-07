﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AsjernasCG.Common.BusinessModels.CardModels;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public ClientSideCard ControllingCard { get; set; }
    private DraggingActions actions;
    public string DraggingActionName;

    private bool AllowDrag()
    {
        var boardManager = BoardManager.Instance;

        return boardManager.CurrentActiveInitiativeSlots.Any(s => s.CardManager.Template.GeneratedCardId == ControllingCard.CardManager.Template.GeneratedCardId);
        //var activeCharacter = boardManager.ActiveCharacterManager;
        //if (BoardManager.Instance.ActiveCharacterManager != null)
        //{
        //    var id = activeCharacter.CardManager.Template.GeneratedCardId;
        //    var card = BoardManager.Instance.GetCard(id);
        //    if (card != null && card.ParticipatorState is PlayerState)
        //    {
        //        if ((card.ParticipatorState as PlayerState).UserId == PhotonEngine.UserId)
        //            return true;
        //    }
        //}
        //return false;
    }

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
        if (actions != null)
        {
            if (BoardManager.Instance.TurnStatus == TurnStatus.Setup)
            {
                if (actions.AllowInSetup() && (ControllingCard.ParticipatorState as PlayerState).UserId == BoardManager.Instance.ActiveSetupSlotPlayer.UserId)
                {
                    actions.OnStartDrag();
                }
                else
                {
                    //"it's not your setup turn";
                }
            }
            else
            {
                if (AllowDrag())
                {
                    actions.OnStartDrag();
                }
            }
        }
    }

    private bool dragStarted = false;

    public void OnMouseDrag()
    {
        if (dragStarted)
        {
            actions?.OnDraggingInUpdate();
        }
        else
        {
            if (actions != null)
            {
                if (BoardManager.Instance.TurnStatus == TurnStatus.Setup)
                {
                    if (actions.AllowInSetup() && (ControllingCard.ParticipatorState as PlayerState).UserId == BoardManager.Instance.ActiveSetupSlotPlayer.UserId)
                    {
                        dragStarted = true;
                        actions.OnDraggingInUpdate();
                    }
                    else
                    {
                        //"it's not your setup turn";
                    }
                }
                else
                {
                    if (AllowDrag())
                    {
                        dragStarted = true;
                        actions.OnDraggingInUpdate();
                    }
                }
            }
        }
    }

    public void OnMouseUp()
    {
        dragStarted = false;
        if (actions != null)
        {
            if (BoardManager.Instance.TurnStatus == TurnStatus.Setup)
            {
                if (actions.AllowInSetup() && (ControllingCard.ParticipatorState as PlayerState).UserId == BoardManager.Instance.ActiveSetupSlotPlayer.UserId)
                {
                    actions.OnEndDrag();
                }
                else
                {
                    //"it's not your setup turn";
                }
            }
            else
            {
                if (AllowDrag())
                {
                    actions.OnEndDrag();
                }
            }
        }
    }

}
