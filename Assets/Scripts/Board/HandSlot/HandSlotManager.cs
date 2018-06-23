using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HandSlotManager : MonoBehaviour
{
    public List<ClientSideCard> HandCards { get; set; }
    public HandSlotPositionContainer HandSlotContainerLow;
    public HandSlotPositionContainer HandSlotContainerHigh;
    public bool HandPreviewOn;
    private object positionUpdaterLocker = new object();
    private Sequence currentRunningCardAnimation;
    private bool lastAnimationWasKilledByUser;
    private bool currentRunningCardAnimationWasSentByServer;
    private List<int> currentRunningCardAnimationsCardIds;

    private float forcedHandUpdateTimeframe = 0.35f;
    private float normalHandUpdateTimeframe = 0.85f;

    private void Awake()
    {
        currentRunningCardAnimationsCardIds = new List<int>();
        currentRunningCardAnimationWasSentByServer = true;
        HandCards = new List<ClientSideCard>();
    }

    public void UpdatePositions(Ease easingFunction, float animationCompletionTime, bool callbackOnEnd)
    {
        currentRunningCardAnimation = DOTween.Sequence();
        var container = HandSlotContainerLow;
        if (HandPreviewOn)
            container = HandSlotContainerHigh;

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

        if (callbackOnEnd)
        {
            if (HandCards.Count > 0)
            {
                currentRunningCardAnimation.OnComplete(() =>
                {
                    //Debug.Log("Completed card running animation. Next action.");
                    currentRunningCardAnimation = null; currentRunningCardAnimationsCardIds = new List<int>(); PhotonEngine.CompletedAction("Handslot");
                });
            }
            else
            {
                currentRunningCardAnimation = null;
                currentRunningCardAnimationsCardIds = new List<int>();
                PhotonEngine.CompletedAction("Handslot");
            }
        }
    }

    public void AddCardLast(ClientSideCard clientSideCard)
    {
        lock (positionUpdaterLocker)
        {
            currentRunningCardAnimationWasSentByServer = true;
            if (HandCards.Count < 8)
            {
                HandCards.Add(clientSideCard);
                ApplyFullHandDisplayHoverEvents(clientSideCard);
                currentRunningCardAnimationsCardIds = new List<int>();
                currentRunningCardAnimationsCardIds.Add(clientSideCard.CardStats.GeneratedCardId);
                UpdatePositions(Ease.Linear, normalHandUpdateTimeframe, true);
            }
        }
    }

    public void AddCardToPosition(ClientSideCard clientSideCard, int index)
    {
        lock (positionUpdaterLocker)
        {
            currentRunningCardAnimationWasSentByServer = true;
            if (HandCards.Count < 8)
            {
                HandCards.Insert(index, clientSideCard);
                ApplyFullHandDisplayHoverEvents(clientSideCard);
                currentRunningCardAnimationsCardIds.Add(clientSideCard.CardStats.GeneratedCardId);
                UpdatePositions(Ease.Linear, normalHandUpdateTimeframe, true);
            }
        }
    }

    public void RemoveCard(int cardId)
    {
        lock (positionUpdaterLocker)
        {
            currentRunningCardAnimationWasSentByServer = true;
            var card = HandCards.FirstOrDefault(c => c.CardStats.GeneratedCardId == cardId);
            if (card == null)
                return;

            HandCards.Remove(card);
            RemoveFullHandDisplayHoverEvents(card);
            currentRunningCardAnimationsCardIds.Add(card.CardStats.GeneratedCardId);
            card.CardViewObject.transform.DOMove(new Vector3(-2.47f, 0.05f, 5.2f), 1f).OnComplete(() =>
            {
                card.CardViewObject.SetActive(false);
                UpdatePositions(Ease.Linear, normalHandUpdateTimeframe, true);
            });
        }
    }

    //private bool handpreviewenabledlastframe;
    //private void Update()
    //{
    //    if (handpreviewenabledlastframe != HandPreviewOn)
    //    {
    //        handpreviewenabledlastframe = HandPreviewOn;
    //        ForceUpdatePositions();
    //    }
    //}

    //Call this for hand hovering position changing
    //NOTE: This function is called by the user so we have to be carefull, to NOT call PhotonEngine.CompletedAction if the current animation beeing played was not from the server.
    //If there is an active animation sent by the server, we kill the animation, recalculate the positions, animate the cards to them, and then call PhotonEngine.CompletedAction.
    //If there is an active animation sent by the user, we just update the positions and animate the cards without calling PhotonEngine.CompletedAction, else the user would be able to control the
    //queue of the actions by just hovering over the cards in hand quickly
    //the currentRunningCardAnimation & currentRunningCardAnimationWasSentByServer bools help us achieve that
    public void ForceUpdatePositions()
    {
        //If there is an active card hand animation and the animation was sent by the server 
        if (currentRunningCardAnimation != null && currentRunningCardAnimation.IsPlaying() && currentRunningCardAnimationWasSentByServer)
        {
            //Notify that the animation that will be played now, is sent by the user, so if he calls 'ForceUpdatePositions' in quick succession, he won't end up in this if statement again.
            currentRunningCardAnimationWasSentByServer = false;
            currentRunningCardAnimation.OnComplete(() => { });
            currentRunningCardAnimation.Kill();
            //on purpose, the currentRunningCardAnimationsCardIds is not reset, so the forced update can play the animation speed as it was originaly supposed to do
            lock (positionUpdaterLocker)
            {
                //Debug.Log("I killed a hand animation. I will call for photonengine.completedaction");
                UpdatePositions(Ease.OutQuart, forcedHandUpdateTimeframe, true);
            }
        }
        else
        {
            lock (positionUpdaterLocker)
            {
                UpdatePositions(Ease.OutQuart, forcedHandUpdateTimeframe, false);
            }
        }

    }

    private void Move(ClientSideCard card, OddPlacementPosition oddPosition, Sequence seq, HandSlotPositionContainer handSlotContainer, Ease easingFunction, float animationCompletionTime, int indexInHand)
    {
        var cardTrans = card.CardViewObject;
        if (currentRunningCardAnimationsCardIds.Contains(card.CardStats.GeneratedCardId))
        {
            animationCompletionTime = 0.85f;
        }
        var yIndexAddition = (float)(indexInHand * (0.01));
        var originalPosition = handSlotContainer.OddSlots[oddPosition].gameObject.transform.position;
        var vector = new Vector3(originalPosition.x, originalPosition.y + yIndexAddition, originalPosition.z);
        seq.Insert(0, cardTrans.transform.DOMove(vector, animationCompletionTime));
    }

    private void Move(ClientSideCard card, EvenPlacementPosition evenPosition, Sequence seq, HandSlotPositionContainer handSlotContainer, Ease easingFunction, float animationCompletionTime, int indexInHand)
    {
        var cardTrans = card.CardViewObject;
        if (currentRunningCardAnimationsCardIds.Contains(card.CardStats.GeneratedCardId))
        {
            animationCompletionTime = 0.85f;
        }
        var yIndexAddition = (float)(indexInHand * (0.01));
        var originalPosition = handSlotContainer.EvenSlots[evenPosition].gameObject.transform.position;
        var vector = new Vector3(originalPosition.x, originalPosition.y + yIndexAddition, originalPosition.z);
        seq.Insert(0, cardTrans.transform.DOMove(vector, animationCompletionTime));
    }

    public void ApplyFullHandDisplayHoverEvents(ClientSideCard card)
    {
        card.Events.OnMouseOverEventHandler += Event_UserHoveredOverHand;
        card.Events.OnMouseExitEventHandler += Event_UserHoveredOutOfHand;
    }

    public void RemoveFullHandDisplayHoverEvents(ClientSideCard card)
    {
        card.Events.OnMouseOverEventHandler -= Event_UserHoveredOverHand;
        card.Events.OnMouseExitEventHandler -= Event_UserHoveredOutOfHand;
    }

    private void Event_UserHoveredOverHand()
    {
        if (HandPreviewOn == true)
            return;
        HandPreviewOn = true;
        ForceUpdatePositions();
    }

    private void Event_UserHoveredOutOfHand()
    {
        HandPreviewOn = false;

        StartCoroutine(ExecuteAfterTime(0.20f, () =>
        {
            if (HandPreviewOn == true)
                return;
            HandPreviewOn = false;
            ForceUpdatePositions();
        }));
    }

    IEnumerator ExecuteAfterTime(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
