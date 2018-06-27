using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardHandHelperComponent : MonoBehaviour
{
    public ClientSideCard Card { get; set; }
    public SimpleHandSlotManagerV2 HandSlotManager { get; set; }
    private bool clickedOnCard;
    private Vector3 handPosition;
    private Quaternion handRotation;
    private Vector3 previewPosition;
    public void StoreDesignatedHandPositionAndRotation(Vector3 handPosition, Quaternion handRotation)
    {
        this.handPosition = handPosition;
        this.handRotation = handRotation;
    }
    public void StoreDesignatedPreviewPosition(Vector3 previewPos)
    {
        previewPosition = previewPos;
    }
    void Start()
    {
    }
    #region "Hovering"

    //Enable Hovering
    private void OnMouseEnter()
    {
        //Debug.Log("MouseEnter");
        if (HandSlotManager?.ActiveCard != null)
            return;

        if (!Card.IsDragging)
        {
            Card.CardViewObject.GetComponent<Draggable>().enabled = true;
        }

        if (!Card.IsHovering)
        {
            Card.IsHovering = true;

            Card.CardManager.CardVisual.Visual.enabled = false;

            Card.CardManager.PreviewVisual.Visual.enabled = true;
            Card.CardManager.PreviewVisual.gameObject.transform.position = previewPosition;
            AnimationOnEnd(Card);
        }
    }

    //Disable Hovering
    private void OnMouseExit()
    {
        //Debug.Log("MouseExit");

        if (!Card.IsDragging)
        {
            Card.CardViewObject.GetComponent<Draggable>().enabled = false;
        }

        if (Card.IsHovering)
        {
            Card.KillTweens();

            Card.IsHovering = false;

            //Card.CardViewObject.transform.position = handPosition;
            Card.CardManager.PreviewVisual.gameObject.transform.DOMove(handPosition, 0.15f).SetEase(Ease.OutQuad, 0.5f, 0).OnComplete(() =>
            {
                Card.CardManager.PreviewVisual.enabled = false;
                Card.CardManager.CardVisual.Visual.enabled = true;
                Card.CardViewObject.transform.position = handPosition;
                Card.CardViewObject.transform.rotation = handRotation;
            });
            Card.CardViewObject.transform.rotation = handRotation;
            //Card.CardManager.PreviewVisual.gameObject.transform.position = previewPosition;
        }
    }

    //Hover-Start animation
    private void AnimationOnEnd(ClientSideCard card)
    {
        card.DoTweenTweening = null;
        var sequance = DOTween.Sequence();
        card.DoTweenSequence = sequance;
        sequance.Append(Card.CardManager.PreviewVisual.gameObject.transform.DOMove(previewPosition + new Vector3(0, 0, 0.025f), 1f));// SetEase(Ease.OutCirc, 0.5f, 0);
        sequance.Append(Card.CardManager.PreviewVisual.gameObject.transform.DOMove(previewPosition + new Vector3(0, 0, 0.05f), 1f));
        sequance.Append(Card.CardManager.PreviewVisual.gameObject.transform.DOMove(previewPosition - new Vector3(0, 0, 0.03f), 4f));//.SetEase(Ease.InCubic, 0.5f, 0);
        sequance.OnComplete(() => { card.DoTweenSequence = null; });
    }

    #endregion

    #region "Play interaction"

    //Start play interaction
    public void OnMouseDown()
    {
        Card.IsDragging = true;
        Card.CardViewObject.GetComponent<DragRotator>().enabled = true;
        HandSlotManager.ActiveCard = Card;

        Card.KillTweens();

        Card.IsHovering = false;

        Card.CardManager.PreviewVisual.Visual.enabled = false;


        Card.CardManager.CardVisual.Visual.enabled = true;
       // Card.CardViewObject.layer = 2;
        Card.CardViewObject.transform.position = handPosition;
        Card.CardViewObject.transform.rotation = handRotation;

        BoardManager.OnCursorEntersCard += OnOverlappedCard;
    }

    public void OnOverlappedCard(ClientSideCard card)
    {
        //Debug.Log("I moused over a card");
    }

    //Stop play interaction
    private void OnMouseUp()
    {
        BoardManager.OnCursorEntersCard -= OnOverlappedCard;
        //Debug.Log("MouseUp");
        Card.IsDragging = false;
        Card.CardViewObject.GetComponent<DragRotator>().enabled = false;

        if (HandSlotManager.ActiveCard != null)
            HandSlotManager.ActiveCard = null;

        Card.KillTweens();
        Card.CardViewObject.layer = 0;
        Card.CardViewObject.transform.DOMove(handPosition, 0.35f).OnComplete(() =>
        {
            Card.CardViewObject.transform.rotation = handRotation;
        });
        //Card.CardManager.PreviewVisual.gameObject.transform.position = previewPosition;
    }
    #endregion

    //Will not animate
    public void ResetPositionToNormal_Immediate()
    {
        Card.CardViewObject.layer = 0;
        clickedOnCard = false;
        Card.IsHovering = false;
        Card.IsDragging = false;
        Card.CardViewObject.GetComponent<Draggable>().enabled = false;
        Card.CardViewObject.GetComponent<DragRotator>().enabled = false;
        //card.CardViewObject.GetComponent<BoxCollider>().enabled = false;
        Card.KillTweens();
        Card.CardManager.PreviewVisual.Visual.enabled = false;
        Card.CardManager.CardVisual.Visual.enabled = true;
        Card.CardViewObject.transform.position = handPosition;
        Card.CardViewObject.transform.rotation = handRotation;
    }
}
