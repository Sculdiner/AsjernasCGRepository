using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SimpleHandSlotManager : MonoBehaviour
{
    public List<ClientSideCard> HandCards { get; set; }
    public HandSlotPositionContainer HandSlotContainerLow;
    private object positionUpdaterLocker = new object();
    private float normalHandUpdateTimeframe = 0.85f;

    private void Update()
    {
    }

    private void Awake()
    {
        HandCards = new List<ClientSideCard>();
    }

    public void UpdatePositions(Ease easingFunction, float animationCompletionTime)
    {
        var currentRunningCardAnimation = DOTween.Sequence();
        var container = HandSlotContainerLow;
        //if (HandPreviewOn)
        //    container = HandSlotContainerHigh;

        switch (HandCards.Count)
        {
            case 0:
                break;
            case 1:
                Move(HandCards.First(), OddPlacementPosition.Middle, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 0);
                break;
            case 2:
                Move(HandCards[0], EvenPlacementPosition.MiddleLeft, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1], EvenPlacementPosition.MiddleRight, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 1);
                break;
            case 3:
                Move(HandCards[0], OddPlacementPosition.MiddleLeft, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1], OddPlacementPosition.Middle, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2], OddPlacementPosition.MiddleRight, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 2);
                break;
            case 4:
                Move(HandCards[0], EvenPlacementPosition.Left, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1], EvenPlacementPosition.MiddleLeft, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2], EvenPlacementPosition.MiddleRight, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3], EvenPlacementPosition.Right, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 3);
                break;
            case 5:
                Move(HandCards[0], OddPlacementPosition.Left, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1], OddPlacementPosition.MiddleLeft, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2], OddPlacementPosition.Middle, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3], OddPlacementPosition.MiddleRight, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 3);
                Move(HandCards[4], OddPlacementPosition.Right, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 4);
                break;
            case 6:
                Move(HandCards[0], EvenPlacementPosition.LeftFar, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1], EvenPlacementPosition.Left, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2], EvenPlacementPosition.MiddleLeft, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3], EvenPlacementPosition.MiddleRight, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 3);
                Move(HandCards[4], EvenPlacementPosition.Right, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 4);
                Move(HandCards[5], EvenPlacementPosition.RightFar, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 5);
                break;
            case 7:
                Move(HandCards[0], OddPlacementPosition.LeftFar, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1], OddPlacementPosition.Left, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2], OddPlacementPosition.MiddleLeft, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3], OddPlacementPosition.Middle, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 3);
                Move(HandCards[4], OddPlacementPosition.MiddleRight, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 4);
                Move(HandCards[5], OddPlacementPosition.Right, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 5);
                Move(HandCards[6], OddPlacementPosition.RightFar, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 6);
                break;
            case 8:
                Move(HandCards[0], EvenPlacementPosition.LeftLast, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1], EvenPlacementPosition.LeftFar, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2], EvenPlacementPosition.Left, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3], EvenPlacementPosition.MiddleLeft, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 3);
                Move(HandCards[4], EvenPlacementPosition.MiddleRight, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 4);
                Move(HandCards[5], EvenPlacementPosition.Right, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 5);
                Move(HandCards[6], EvenPlacementPosition.RightFar, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 6);
                Move(HandCards[7], EvenPlacementPosition.RightLast, currentRunningCardAnimation, container, easingFunction, animationCompletionTime, 7);
                break;
        }


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

    public void AddCardLast(ClientSideCard clientSideCard)
    {
        lock (positionUpdaterLocker)
        {
            if (HandCards.Count < 8)
            {
                HandCards.Add(clientSideCard);
                ApplyFullHandDisplayHoverEvents(clientSideCard);
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
                HandCards.Insert(index, clientSideCard);
                ApplyFullHandDisplayHoverEvents(clientSideCard);
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
            RemoveFullHandDisplayHoverEvents(card);
            card.CardViewObject.transform.DOMove(new Vector3(-2.47f, 0.05f, 5.2f), 1f).OnComplete(() =>
            {
                card.CardViewObject.SetActive(false);
                UpdatePositions(Ease.Linear, normalHandUpdateTimeframe);
            });
        }
    }

    private void Move(ClientSideCard card, OddPlacementPosition oddPosition, Sequence seq, HandSlotPositionContainer handSlotContainer, Ease easingFunction, float animationCompletionTime, int indexInHand)
    {
        if (card.IsUnderPlayerControl)
            return;

        var cardTrans = card.CardViewObject;

        var yIndexAddition = (float)(indexInHand * (0.01));
        var originalPosition = handSlotContainer.OddSlots[oddPosition].gameObject.transform.position;
        var vector = new Vector3(originalPosition.x, originalPosition.y + yIndexAddition, originalPosition.z);
        seq.Insert(0, cardTrans.transform.DOMove(vector, animationCompletionTime));
    }

    private void Move(ClientSideCard card, EvenPlacementPosition evenPosition, Sequence seq, HandSlotPositionContainer handSlotContainer, Ease easingFunction, float animationCompletionTime, int indexInHand)
    {
        if (card.IsUnderPlayerControl)
            return;

        var cardTrans = card.CardViewObject;

        var yIndexAddition = (float)(indexInHand * (0.01));
        var originalPosition = handSlotContainer.EvenSlots[evenPosition].gameObject.transform.position;
        var vector = new Vector3(originalPosition.x, originalPosition.y + yIndexAddition, originalPosition.z);
        seq.Insert(0, cardTrans.transform.DOMove(vector, animationCompletionTime));
    }

    public void ApplyFullHandDisplayHoverEvents(ClientSideCard card)
    {
        card.Events.OnMouseOverEventHandler += Event_UserHoveredOverCard;
        card.Events.OnMouseExitEventHandler += Event_UserHoveredOutOfCard;
    }

    public void RemoveFullHandDisplayHoverEvents(ClientSideCard card)
    {
        card.Events.OnMouseOverEventHandler -= Event_UserHoveredOverCard;
        card.Events.OnMouseExitEventHandler -= Event_UserHoveredOutOfCard;
    }

    private void Event_UserHoveredOverCard(ClientSideCard card)
    {
        if (card.IsHovering)
            return;

        card.IsHovering = true;
        var currentPosition = card.CardViewObject.transform.position;
        card.LastPosition = currentPosition;
        var finalVector = new Vector3(currentPosition.x, currentPosition.y + 1.9f, currentPosition.z + 0.8f);
        card.CardViewObject.transform.position = finalVector;// DOMove(finalVector, 0.1f);
    }

    private void Event_UserHoveredOutOfCard(ClientSideCard card)
    {
        if (!card.IsHovering)
            return;

        card.IsHovering = false;
        card.CardViewObject.transform.position = card.LastPosition;//.DOMove(card.LastPosition, 0.1f);
    }

    //IEnumerator ExecuteAfterTime(float time, Action action)
    //{
    //    yield return new WaitForSeconds(time);
    //    action();
    //}
}
