﻿using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class HandSlotWithCollider : MonoBehaviour
{
    public SimpleHandSlotManager HandSlotManager;
    public PlacementPosition PlacementPosition;
    public BoxCollider CardGhostCollider;

    private void Awake()
    {
        CardGhostCollider = this.gameObject.GetComponent<BoxCollider>();
    }

    public Vector3 GetMyWorldPosition()
    {
        return this.transform.position;
    }

    public ClientSideCard GetAttachedCard()
    {
        return HandSlotManager.GetCardInPosition(PlacementPosition);
    }

    public Vector3 GetHoveringPosition()
    {
        return HandSlotManager.HandSlotPreviewPositionContainer.Slots[PlacementPosition].position;

        //var myPos = GetMyWorldPosition();
        ////x is constrained based on the mouse position (screen to world space)
        ////var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        ////return new Vector3(worldMousePosition.x, myPos.y + 3f, myPos.z + 1.3f);
        //return new Vector3(myPos.x, myPos.y + 3f, myPos.z + 1.3f);
    }

    private void OnMouseEnter()
    {
        var card = GetAttachedCard();
        if (card == null)
            return;

        //var killedTweens = DOTween.Kill(card.CardViewObject);
        //if (killedTweens>0)
        //{
        //    var asd = "";
        //}
        card.CardViewObject.transform.DOMove(GetHoveringPosition(), 0.1f).SetEase(Ease.OutBack, 0.5f, 0);//.OnComplete(AnimationOnEnd);
    }

    //private void AnimationOnEnd()
    //{
    //    var card = GetAttachedCard();
    //    if (card == null)
    //        return;
    //    var sequance = DOTween.Sequence();
    //    sequance.SetId(card.CardStats.GeneratedCardId);
    //    card.DoTweenSequence = sequance;
    //    var hov = GetHoveringPosition().z;
    //    sequance.Append(card.CardViewObject.transform.DOMoveZ(hov + 0.025f, 1f)).SetEase(Ease.OutCirc, 0.5f, 0);
    //    sequance.Append(card.CardViewObject.transform.DOMoveZ(hov + 0.005f, 1f));
    //    sequance.Append(card.CardViewObject.transform.DOMoveZ(hov - 0.030f, 1f)).SetEase(Ease.InSine, 0.5f, 0);
    //    sequance.OnComplete(() => { card.DoTweenSequence = null; });
    //}

    private void OnMouseDown()
    {
        var card = GetAttachedCard();
        if (card == null)
            return;
        clickedOnCard = true;
        card.CardViewObject.GetComponent<BoxCollider>().enabled = true;
        var draggableComponent = card.CardViewObject.GetComponent<Draggable>();
        draggableComponent.OnMouseUpEvents += () =>
        {
            card.CardViewObject.transform.DOMove(GetMyWorldPosition(), 0.15f);
        };
        draggableComponent.OnMouseEnter();
        draggableComponent.OnMouseDown();

        //card.CardViewObject.GetComponent<DragRotator>().enabled = true;
        //var draggableComponent = card.CardViewObject.GetComponent<Draggable>();
        //draggableComponent.enabled = true;
        //draggableComponent.ExternallyTriggerMouseDown();
    }

    private void OnMouseDrag()
    {
        var card = GetAttachedCard();
        if (card == null)
            return;

        clickedOnCard = true;
        card.CardViewObject.GetComponent<BoxCollider>().enabled = true;
        GetAttachedCard().CardViewObject.GetComponent<Draggable>().OnMouseDrag();
    }

    private void OnMouseUp()
    {
        clickedOnCard = false;
    }

    private bool clickedOnCard;

    private void OnMouseExit()
    {
        if (clickedOnCard)
        {
            clickedOnCard = false;
            return;
        }
        var card = GetAttachedCard();
        if (card == null)
            return;

        //var killed = DOTween.Kill(card.CardStats.GeneratedCardId);
        //Debug.Log(killed);

        card.CardViewObject.transform.DOMove(GetMyWorldPosition(), 0.15f).SetEase(Ease.OutQuad, 0.5f, 0);
    }
}
