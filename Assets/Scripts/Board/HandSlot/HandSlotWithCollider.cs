﻿using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


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
        var myPos = GetMyWorldPosition();
        //x is constrained based on the mouse position (screen to world space)
        //var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        //return new Vector3(worldMousePosition.x, myPos.y + 3f, myPos.z + 1.3f);
        return new Vector3(myPos.x, myPos.y + 3f, myPos.z + 1.3f);
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
        card.CardViewObject.transform.DOMove(GetHoveringPosition(),0.1f);
    }

    private void OnMouseDown()
    {
        var card = GetAttachedCard();
        if (card == null)
            return;
        clickedOnCard = true;
        card.CardViewObject.GetComponent<BoxCollider>().enabled = true;
        card.CardViewObject.GetComponent<DragRotator>().enabled = true;
        card.CardViewObject.GetComponent<Draggable>().enabled = true;
    }

    private void OnMouseDrag()
    {
        clickedOnCard = true;
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

        card.CardViewObject.transform.DOMove(GetMyWorldPosition(), 0.1f);
    }

}
