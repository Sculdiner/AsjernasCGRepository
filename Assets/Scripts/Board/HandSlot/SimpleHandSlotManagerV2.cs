﻿using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SimpleHandSlotManagerV2 : SerializedMonoBehaviour
{
    private enum CollidersPosition
    {
        Odd,
        Even
    }
    //Card used by user (dragging etc.etc.)
    public ClientSideCard ActiveCard { get; set; }
    public List<ClientSideCard> HandCards { get; set; }
    public HandSlotPositionContainerV2 HandSlotPositionContainer;
    public HandSlotPreviewPositionContainer HandSlotPreviewPositionContainer;
    private object positionUpdaterLocker = new object();
    private float normalHandUpdateTimeframe = 0.85f;
    private object cardPositionDictionaryLocker = new object();
    [OdinSerialize]
    public Dictionary<PlacementPosition, ClientSideCard> CurrentCardPositions { get; set; }


    private void Awake()
    {
        HandCards = new List<ClientSideCard>();
    }

    public void RefreshHandPositions(Ease easingFunction, float animationCompletionTime)
    {
        UpdatePositions(easingFunction, animationCompletionTime, false);
    }

    private void UpdatePositions(Ease easingFunction, float animationCompletionTime, bool nextActionOnEnd = true)
    {
        var currentRunningCardAnimation = DOTween.Sequence();

        lock (cardPositionDictionaryLocker)
        {
            if (CurrentCardPositions != null && CurrentCardPositions.Any())
            {
                foreach (var posKey in CurrentCardPositions.Keys)
                {
                    CurrentCardPositions[posKey].HoverComponent.ForceKillHover();// CardHandHelperComponent.ResetPositionToNormal_Immediate();
                }
            }
            CurrentCardPositions = new Dictionary<PlacementPosition, ClientSideCard>();
        }

        switch (HandCards.Count)
        {
            case 0:
                break;
            case 1:
                MoveToOdd(HandCards.First(), PlacementPosition.OddMiddle, currentRunningCardAnimation, easingFunction, animationCompletionTime, 0);
                break;
            case 2:
                MoveToEven(HandCards[0], PlacementPosition.EvenMiddleLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 0);
                MoveToEven(HandCards[1], PlacementPosition.EvenMiddleRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 1);
                break;
            case 3:
                MoveToOdd(HandCards[0], PlacementPosition.OddMiddleLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 0);
                MoveToOdd(HandCards[1], PlacementPosition.OddMiddle, currentRunningCardAnimation, easingFunction, animationCompletionTime, 1);
                MoveToOdd(HandCards[2], PlacementPosition.OddMiddleRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 2);
                break;
            case 4:
                MoveToEven(HandCards[0], PlacementPosition.EvenLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 0);
                MoveToEven(HandCards[1], PlacementPosition.EvenMiddleLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 1);
                MoveToEven(HandCards[2], PlacementPosition.EvenMiddleRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 2);
                MoveToEven(HandCards[3], PlacementPosition.EvenRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 3);
                break;
            case 5:
                MoveToOdd(HandCards[0], PlacementPosition.OddLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 0);
                MoveToOdd(HandCards[1], PlacementPosition.OddMiddleLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 1);
                MoveToOdd(HandCards[2], PlacementPosition.OddMiddle, currentRunningCardAnimation, easingFunction, animationCompletionTime, 2);
                MoveToOdd(HandCards[3], PlacementPosition.OddMiddleRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 3);
                MoveToOdd(HandCards[4], PlacementPosition.OddRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 4);
                break;
            case 6:
                MoveToEven(HandCards[0], PlacementPosition.EvenLeftFar, currentRunningCardAnimation, easingFunction, animationCompletionTime, 0);
                MoveToEven(HandCards[1], PlacementPosition.EvenLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 1);
                MoveToEven(HandCards[2], PlacementPosition.EvenMiddleLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 2);
                MoveToEven(HandCards[3], PlacementPosition.EvenMiddleRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 3);
                MoveToEven(HandCards[4], PlacementPosition.EvenRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 4);
                MoveToEven(HandCards[5], PlacementPosition.EvenRightFar, currentRunningCardAnimation, easingFunction, animationCompletionTime, 5);
                break;
            case 7:
                MoveToOdd(HandCards[0], PlacementPosition.OddLeftFar, currentRunningCardAnimation, easingFunction, animationCompletionTime, 0);
                MoveToOdd(HandCards[1], PlacementPosition.OddLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 1);
                MoveToOdd(HandCards[2], PlacementPosition.OddMiddleLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 2);
                MoveToOdd(HandCards[3], PlacementPosition.OddMiddle, currentRunningCardAnimation, easingFunction, animationCompletionTime, 3);
                MoveToOdd(HandCards[4], PlacementPosition.OddMiddleRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 4);
                MoveToOdd(HandCards[5], PlacementPosition.OddRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 5);
                MoveToOdd(HandCards[6], PlacementPosition.OddRightFar, currentRunningCardAnimation, easingFunction, animationCompletionTime, 6);
                break;
            case 8:
                MoveToEven(HandCards[0], PlacementPosition.EvenLeftLast, currentRunningCardAnimation, easingFunction, animationCompletionTime, 0);
                MoveToEven(HandCards[1], PlacementPosition.EvenLeftFar, currentRunningCardAnimation, easingFunction, animationCompletionTime, 1);
                MoveToEven(HandCards[2], PlacementPosition.EvenLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 2);
                MoveToEven(HandCards[3], PlacementPosition.EvenMiddleLeft, currentRunningCardAnimation, easingFunction, animationCompletionTime, 3);
                MoveToEven(HandCards[4], PlacementPosition.EvenMiddleRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 4);
                MoveToEven(HandCards[5], PlacementPosition.EvenRight, currentRunningCardAnimation, easingFunction, animationCompletionTime, 5);
                MoveToEven(HandCards[6], PlacementPosition.EvenRightFar, currentRunningCardAnimation, easingFunction, animationCompletionTime, 6);
                MoveToEven(HandCards[7], PlacementPosition.EvenRightLast, currentRunningCardAnimation, easingFunction, animationCompletionTime, 7);
                break;
        }

        if (nextActionOnEnd)
        {
            if (HandCards.Count > 0)
            {
                currentRunningCardAnimation.OnComplete(() =>
                {
                    PhotonEngine.CompletedAction("Handslot");
                });
            }
            else
            {

                PhotonEngine.CompletedAction("Handslot");
            }
        }
    }

    public void AddCardLast(ClientSideCard clientSideCard)
    {
        lock (positionUpdaterLocker)
        {
            if (HandCards.Count < 8)
            {
                //clientSideCard.CardViewObject.GetComponent<BoxCollider>().enabled = false;
                //clientSideCard.CardManager.CardHandHelperComponent.HandSlotManager = this;
                clientSideCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();
                //clientSideCard.CardViewObject.GetComponent<Draggable>().enabled = false;

                //clientSideCard.CardViewObject.GetComponent<Draggable>().enabled = false;
                HandCards.Add(clientSideCard);
                //ApplyFullHandDisplayHoverEvents(clientSideCard);
                UpdatePositions(Ease.Linear, normalHandUpdateTimeframe);
            }
        }
    }

    public void AddCardToPosition(ClientSideCard clientSideCard, int index)
    {
        lock (positionUpdaterLocker)
        {
            if (HandCards.Count < 8)
            {
                //clientSideCard.CardViewObject.GetComponent<BoxCollider>().enabled = false;
                //clientSideCard.CardManager.CardHandHelperComponent.HandSlotManager = this;
                clientSideCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();
                //clientSideCard.CardViewObject.GetComponent<Draggable>().enabled = false;
                //clientSideCard.CardViewObject.GetComponent<Draggable>().enabled = false;
                HandCards.Insert(index, clientSideCard);
                //ApplyFullHandDisplayHoverEvents(clientSideCard);
                UpdatePositions(Ease.Linear, normalHandUpdateTimeframe);
            }
        }
    }

    public void RemoveCard(int cardId)
    {
        lock (positionUpdaterLocker)
        {
            var card = HandCards.FirstOrDefault(c => c.CardStats.GeneratedCardId == cardId);
            if (card == null)
                return;

            HandCards.Remove(card);
            PlacementPosition? cardPosition = null;
            lock (cardPositionDictionaryLocker)
            {
                foreach (var posKey in CurrentCardPositions.Keys)
                {
                    if (CurrentCardPositions[posKey].CardStats.GeneratedCardId == card.CardStats.GeneratedCardId)
                    {
                        cardPosition = posKey;
                        break;
                    }
                    
                }
                if (cardPosition.HasValue)
                {
                    CurrentCardPositions.Remove(cardPosition.Value);
                }
            }
            
            UpdatePositions(Ease.Linear, normalHandUpdateTimeframe);
        }
    }

    public void RemoveCardToDiscard(int cardId)
    {
        lock (positionUpdaterLocker)
        {
            var card = HandCards.FirstOrDefault(c => c.CardStats.GeneratedCardId == cardId);
            if (card == null)
                return;

            HandCards.Remove(card);
            //RemoveFullHandDisplayHoverEvents(card);
            card.CardViewObject.transform.DOMove(new Vector3(-2.47f, 0.05f, 5.2f), 1f).OnComplete(() =>
            {
                card.CardViewObject.SetActive(false);
                UpdatePositions(Ease.Linear, normalHandUpdateTimeframe);
            });
        }
    }

    private void MoveToOdd(ClientSideCard card, PlacementPosition oddPosition, Sequence seq, Ease easingFunction, float animationCompletionTime, int indexInHand)
    {
        lock (cardPositionDictionaryLocker)
        {
            CurrentCardPositions.Add(oddPosition, card);
        }

        if (card.IsHovering)
        {
            return;
        }

        if (card.IsUnderPlayerControl)
            return;

        var cardTrans = card.CardViewObject;

        var yIndexAddition = (float)(indexInHand * (0.01));
        var originalPosition = HandSlotPositionContainer.OddSlots[oddPosition].position;
        var vector = new Vector3(originalPosition.x, originalPosition.y + yIndexAddition, originalPosition.z);
        card.HoverComponent.ForcePreHoveringPositionAndRotation(vector, Quaternion.Euler(Vector3.zero));
        card.HoverComponent.ForceHoveringPositionAndRotation(HandSlotPreviewPositionContainer.Slots[oddPosition].position, Quaternion.Euler(Vector3.zero));
        seq.Insert(0, cardTrans.transform.DOMove(vector, animationCompletionTime).OnComplete(()=> { cardTrans.transform.rotation = Quaternion.Euler(Vector3.zero); }));
    }

    private void MoveToEven(ClientSideCard card, PlacementPosition evenPosition, Sequence seq, Ease easingFunction, float animationCompletionTime, int indexInHand)
    {
        lock (cardPositionDictionaryLocker)
        {
            CurrentCardPositions.Add(evenPosition, card);
        }

        if (card.IsUnderPlayerControl)
            return;

        var cardTrans = card.CardViewObject;

        var yIndexAddition = (float)(indexInHand * (0.01));
        var originalPosition = HandSlotPositionContainer.EvenSlots[evenPosition].position;
        var vector = new Vector3(originalPosition.x, originalPosition.y + yIndexAddition, originalPosition.z);
        card.HoverComponent.ForcePreHoveringPositionAndRotation(vector, Quaternion.Euler(Vector3.zero));
        card.HoverComponent.ForceHoveringPositionAndRotation(HandSlotPreviewPositionContainer.Slots[evenPosition].position, Quaternion.Euler(Vector3.zero));
        seq.Insert(0, cardTrans.transform.DOMove(vector, animationCompletionTime).OnComplete(() => { cardTrans.transform.rotation = Quaternion.Euler(Vector3.zero); }));
    }

    public ClientSideCard GetCardInPosition(PlacementPosition position)
    {
        lock (cardPositionDictionaryLocker)
        {
            if (CurrentCardPositions.ContainsKey(position))
                return CurrentCardPositions[position];
            return null;
        }
    }
}
