using DG.Tweening;
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
        card.IsHovering = true;
        card.CardViewObject.transform.position = GetHoveringPosition();
        AnimationOnEnd(card);
        //var tween = card.CardViewObject.transform.DOMove(GetHoveringPosition(), 0.1f).SetEase(Ease.OutBack, 0.5f, 0).OnComplete(AnimationOnEnd);
        //card.DoTweenTweening = tween;
    }
    private void AnimationOnEnd()
    {
        var card = GetAttachedCard();
        if (card == null)
            return;

        AnimationOnEnd(card);
    }
    private void AnimationOnEnd(ClientSideCard card)
    {
        card.DoTweenTweening = null;
        var sequance = DOTween.Sequence();
        card.DoTweenSequence = sequance;
        var hov = GetHoveringPosition();
        sequance.Append(card.CardViewObject.transform.DOMove(hov + new Vector3(0, 0, 0.025f), 1f));// SetEase(Ease.OutCirc, 0.5f, 0);
        sequance.Append(card.CardViewObject.transform.DOMove(hov + new Vector3(0, 0, 0.05f), 1f));
        sequance.Append(card.CardViewObject.transform.DOMove(hov - new Vector3(0, 0, 0.03f), 4f));//.SetEase(Ease.InCubic, 0.5f, 0);
        sequance.OnComplete(() => { card.DoTweenSequence = null; });
    }

    private void OnMouseDown()
    {
        var card = GetAttachedCard();
        if (card == null)
            return;
        clickedOnCard = true;
        card.KillTweens();
        var colliderComp = card.CardViewObject.GetComponent<BoxCollider>();
        var dragRotatorComp = card.CardViewObject.GetComponent<DragRotator>();
        colliderComp.enabled = true;
        dragRotatorComp.enabled = true;
        var draggableComponent = card.CardViewObject.GetComponent<Draggable>();
        draggableComponent.OnMouseUpEvents += () =>
        {
            dragRotatorComp.enabled = false;
            card.IsHovering = false;
            colliderComp.enabled = false;
            dragRotatorComp.enabled = false;
            card.KillTweens();
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
        card.CardViewObject.GetComponent<BoxCollider>().enabled = true; //TODO remove?
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
        
        card.CardViewObject.GetComponent<DragRotator>().enabled = false;
        card.IsHovering = false;
        card.KillTweens();

        card.CardViewObject.transform.DOMove(GetMyWorldPosition(), 0.15f).SetEase(Ease.OutQuad, 0.5f, 0);
    }

    public void ResetPositionToNormal()
    {
        var card = GetAttachedCard();
        if (card == null)
            return;

        ResetPositionToNormal(card);
    }
    public void ResetPositionToNormal(ClientSideCard card)
    {
        clickedOnCard = false;
        card.IsHovering = false;
        card.CardViewObject.GetComponent<DragRotator>().enabled = false;
        card.CardViewObject.GetComponent<BoxCollider>().enabled = false;
        card.KillTweens();
        card.CardViewObject.transform.position = GetMyWorldPosition();
    }

    //public void ResetPositionToNormal(ClientSideCard card)
    //{
    //    clickedOnCard = false;

    //    if (card.IsHovering)
    //    {
    //        card.IsHovering = false;
    //        card.CardViewObject.transform.position = GetMyWorldPosition();
    //    }
    //    card.KillTweens();
    //}
}
