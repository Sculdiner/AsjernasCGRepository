using DG.Tweening;
using System;
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
    private void Awake()
    {
        handpreviewenabledlastframe = HandPreviewOn;
        HandCards = new List<ClientSideCard>();
    }

    public void UpdatePositions(Ease easingFunction, float animationCompletionTime, bool callbackOnEnd)
    {
        Sequence mySequence = DOTween.Sequence();
        var container = HandSlotContainerLow;
        if (HandPreviewOn)
            container = HandSlotContainerHigh;

        switch (HandCards.Count)
        {
            case 0:
                break;
            case 1:
                Move(HandCards.First().CardViewObject, OddPlacementPosition.Middle, mySequence, container, easingFunction, animationCompletionTime, 0);
                break;
            case 2:
                Move(HandCards[0].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence, container, easingFunction, animationCompletionTime, 1);
                break;
            case 3:
                Move(HandCards[0].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1].CardViewObject, OddPlacementPosition.Middle, mySequence, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2].CardViewObject, OddPlacementPosition.MiddleRight, mySequence, container, easingFunction, animationCompletionTime, 2);
                break;
            case 4:
                Move(HandCards[0].CardViewObject, EvenPlacementPosition.Left, mySequence, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3].CardViewObject, EvenPlacementPosition.Right, mySequence, container, easingFunction, animationCompletionTime, 3);
                break;
            case 5:
                Move(HandCards[0].CardViewObject, OddPlacementPosition.Left, mySequence, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2].CardViewObject, OddPlacementPosition.Middle, mySequence, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3].CardViewObject, OddPlacementPosition.MiddleRight, mySequence, container, easingFunction, animationCompletionTime, 3);
                Move(HandCards[4].CardViewObject, OddPlacementPosition.Right, mySequence, container, easingFunction, animationCompletionTime, 4);
                break;
            case 6:
                Move(HandCards[0].CardViewObject, EvenPlacementPosition.LeftFar, mySequence, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1].CardViewObject, EvenPlacementPosition.Left, mySequence, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence, container, easingFunction, animationCompletionTime, 3);
                Move(HandCards[4].CardViewObject, EvenPlacementPosition.Right, mySequence, container, easingFunction, animationCompletionTime, 4);
                Move(HandCards[5].CardViewObject, EvenPlacementPosition.RightFar, mySequence, container, easingFunction, animationCompletionTime, 5);
                break;
            case 7:
                Move(HandCards[0].CardViewObject, OddPlacementPosition.LeftFar, mySequence, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1].CardViewObject, OddPlacementPosition.Left, mySequence, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3].CardViewObject, OddPlacementPosition.Middle, mySequence, container, easingFunction, animationCompletionTime, 3);
                Move(HandCards[4].CardViewObject, OddPlacementPosition.MiddleRight, mySequence, container, easingFunction, animationCompletionTime, 4);
                Move(HandCards[5].CardViewObject, OddPlacementPosition.Right, mySequence, container, easingFunction, animationCompletionTime, 5);
                Move(HandCards[6].CardViewObject, OddPlacementPosition.RightFar, mySequence, container, easingFunction, animationCompletionTime, 6);
                break;
            case 8:
                Move(HandCards[0].CardViewObject, EvenPlacementPosition.LeftLast, mySequence, container, easingFunction, animationCompletionTime, 0);
                Move(HandCards[1].CardViewObject, EvenPlacementPosition.LeftFar, mySequence, container, easingFunction, animationCompletionTime, 1);
                Move(HandCards[2].CardViewObject, EvenPlacementPosition.Left, mySequence, container, easingFunction, animationCompletionTime, 2);
                Move(HandCards[3].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence, container, easingFunction, animationCompletionTime, 3);
                Move(HandCards[4].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence, container, easingFunction, animationCompletionTime, 4);
                Move(HandCards[5].CardViewObject, EvenPlacementPosition.Right, mySequence, container, easingFunction, animationCompletionTime, 5);
                Move(HandCards[6].CardViewObject, EvenPlacementPosition.RightFar, mySequence, container, easingFunction, animationCompletionTime, 6);
                Move(HandCards[7].CardViewObject, EvenPlacementPosition.RightLast, mySequence, container, easingFunction, animationCompletionTime, 7);
                break;
        }
        if (callbackOnEnd)
        {
            if (HandCards.Count > 0)
            {
                mySequence.OnComplete(() => { PhotonEngine.CompletedAction(); });
            }
            else
            {
                PhotonEngine.CompletedAction();
            }
        }
    }

    public void AddCardLast(ClientSideCard clientSideCard)
    {
        lock (positionUpdaterLocker)
        {
            if (HandCards.Count < 8)
            {
                HandCards.Add(clientSideCard);
                UpdatePositions(Ease.Linear, 0.85f, true);
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
                UpdatePositions(Ease.Linear, 0.85f, true);
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
            card.CardViewObject.transform.DOMove(new Vector3(-2.47f, 0.05f, 5.2f), 1f).OnComplete(() =>
            {
                card.CardViewObject.SetActive(false);
                UpdatePositions(Ease.Linear, 0.85f, true);
            });
        }
    }

    private bool handpreviewenabledlastframe;
    private void Update()
    {
        if (handpreviewenabledlastframe != HandPreviewOn)
        {
            handpreviewenabledlastframe = HandPreviewOn;
            ForceUpdatePositions();
        }
    }

    public void ForceUpdatePositions()
    {
        UpdatePositions(Ease.OutQuart, 0.35f, false);
    }

    private void Move(GameObject card, OddPlacementPosition oddPosition, Sequence seq, HandSlotPositionContainer handSlotContainer, Ease easingFunction, float animationCompletionTime, int indexInHand)
    {
        var yIndexAddition = (float)(indexInHand * (0.01));
        var originalPosition = handSlotContainer.OddSlots[oddPosition].gameObject.transform.position;
        var vector = new Vector3(originalPosition.x, originalPosition.y + yIndexAddition, originalPosition.z);
        seq.Insert(0, card.transform.DOMove(vector, animationCompletionTime));
    }

    private void Move(GameObject card, EvenPlacementPosition evenPosition, Sequence seq, HandSlotPositionContainer handSlotContainer, Ease easingFunction, float animationCompletionTime, int indexInHand)
    {
        var yIndexAddition = (float)(indexInHand * (0.01));
        var originalPosition = handSlotContainer.EvenSlots[evenPosition].gameObject.transform.position;
        var vector = new Vector3(originalPosition.x, originalPosition.y + yIndexAddition, originalPosition.z);
        seq.Insert(0, card.transform.DOMove(vector, animationCompletionTime));
    }
}
