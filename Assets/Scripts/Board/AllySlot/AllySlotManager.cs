using AsjernasCG.Common.BusinessModels.CardModels;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AllySlotManager : PositionalSlotManager
{
    public PlayerState OwningPlayer;
    public bool IsCurrentPlayerArea()
    {
        return OwningPlayer.UserId == PhotonEngine.UserId;
    }
    public List<ClientSideCard> AllyCards { get; set; }
    public AllySlotPositionContainer SlotContainer;
    private object positionUpdaterLocker = new object();
    private void Awake()
    {
        AllyCards = new List<ClientSideCard>();
    }

    //public List<EncounterSlot>

    public void UpdatePositions(bool hasNewMember, bool callbackOnEnd)
    {
        Sequence mySequence = DOTween.Sequence();
        switch (AllyCards.Count)
        {
            case 0:
                break;
            case 1:
                Move(AllyCards.First().CardViewObject, OddPlacementPosition.Middle, mySequence);
                break;
            case 2:
                Move(AllyCards[0].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence);
                Move(AllyCards[1].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence);
                break;
            case 3:
                Move(AllyCards[0].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence);
                Move(AllyCards[1].CardViewObject, OddPlacementPosition.Middle, mySequence);
                Move(AllyCards[2].CardViewObject, OddPlacementPosition.MiddleRight, mySequence);
                break;
            case 4:
                Move(AllyCards[0].CardViewObject, EvenPlacementPosition.Left, mySequence);
                Move(AllyCards[1].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence);
                Move(AllyCards[2].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence);
                Move(AllyCards[3].CardViewObject, EvenPlacementPosition.Right, mySequence);
                break;
            case 5:
                Move(AllyCards[0].CardViewObject, OddPlacementPosition.Left, mySequence);
                Move(AllyCards[1].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence);
                Move(AllyCards[2].CardViewObject, OddPlacementPosition.Middle, mySequence);
                Move(AllyCards[3].CardViewObject, OddPlacementPosition.MiddleRight, mySequence);
                Move(AllyCards[4].CardViewObject, OddPlacementPosition.Right, mySequence);
                break;
            case 6:
                Move(AllyCards[0].CardViewObject, EvenPlacementPosition.LeftFar, mySequence);
                Move(AllyCards[1].CardViewObject, EvenPlacementPosition.Left, mySequence);
                Move(AllyCards[2].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence);
                Move(AllyCards[3].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence);
                Move(AllyCards[4].CardViewObject, EvenPlacementPosition.Right, mySequence);
                Move(AllyCards[5].CardViewObject, EvenPlacementPosition.RightFar, mySequence);
                break;
            case 7:
                Move(AllyCards[0].CardViewObject, OddPlacementPosition.LeftFar, mySequence);
                Move(AllyCards[1].CardViewObject, OddPlacementPosition.Left, mySequence);
                Move(AllyCards[2].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence);
                Move(AllyCards[3].CardViewObject, OddPlacementPosition.Middle, mySequence);
                Move(AllyCards[4].CardViewObject, OddPlacementPosition.MiddleRight, mySequence);
                Move(AllyCards[5].CardViewObject, OddPlacementPosition.Right, mySequence);
                Move(AllyCards[6].CardViewObject, OddPlacementPosition.RightFar, mySequence);
                break;
            case 8:
                Move(AllyCards[0].CardViewObject, EvenPlacementPosition.LeftLast, mySequence);
                Move(AllyCards[1].CardViewObject, EvenPlacementPosition.LeftFar, mySequence);
                Move(AllyCards[2].CardViewObject, EvenPlacementPosition.Left, mySequence);
                Move(AllyCards[3].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence);
                Move(AllyCards[4].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence);
                Move(AllyCards[5].CardViewObject, EvenPlacementPosition.Right, mySequence);
                Move(AllyCards[6].CardViewObject, EvenPlacementPosition.RightFar, mySequence);
                Move(AllyCards[7].CardViewObject, EvenPlacementPosition.RightLast, mySequence);
                break;
        }
        if (AllyCards.Count > 0)
        {
            mySequence.OnComplete(() =>
            {
                if (callbackOnEnd)
                    PhotonEngine.CompletedAction("Ally");
                if (hasNewMember)
                {
                    AllyCards.Last().CardManager.VisualStateManager.SmokePlayParticleSystem.Play();
                }
            });
        }
        else
        {
            if (callbackOnEnd)
                PhotonEngine.CompletedAction("Ally");
        }
    }

    public void AddAllyCardLast(ClientSideCard clientSideCard)
    {
        lock (positionUpdaterLocker)
        {
            if (AllyCards.Count < 8)
            {
                //var allyComp = clientSideCard.CardViewObject.GetComponent<CardAllyHelperComponent>();
                //allyComp.ReferencedCard = clientSideCard;
                //allyComp.enabled = true;
                clientSideCard.CardManager.SlotManager?.RemoveSlot(clientSideCard.CardStats.GeneratedCardId);
                clientSideCard.CardManager.SlotManager = this;
                clientSideCard.SetLocation(CardLocation.PlayArea);
                clientSideCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Follower);
                AllyCards.Add(clientSideCard);
                var draggableComponent = clientSideCard.CardManager.GetComponent<Draggable>();
                if (draggableComponent != null && PhotonEngine.UserId == (clientSideCard.ParticipatorState as PlayerState).UserId)
                    draggableComponent.SetAction<FollowerTargetingBehaviour>();

                //set followerboarddragbehavior draggingaction
                UpdatePositions(true, true);
            }
        }
    }

    public void AddAllyCardToPosition(ClientSideCard clientSideCard, int index)
    {
        lock (positionUpdaterLocker)
        {
            if (AllyCards.Count < 8)
            {
                clientSideCard.SetLocation(CardLocation.PlayArea);
                clientSideCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Follower);
                var allyComp = clientSideCard.CardViewObject.GetComponent<CardAllyHelperComponent>();
                allyComp.ReferencedCard = clientSideCard;
                allyComp.enabled = true;
                AllyCards.Insert(index, clientSideCard);
                var draggableComponent = clientSideCard.CardManager.GetComponent<Draggable>();
                if (draggableComponent != null && PhotonEngine.UserId == (clientSideCard.ParticipatorState as PlayerState).UserId)
                    draggableComponent.SetAction<FollowerCastDragBehaviour>();
                UpdatePositions(true, true);
            }
        }
    }

    public override void RemoveSlot(int cardId)
    {
        lock (positionUpdaterLocker)
        {
            var card = AllyCards.FirstOrDefault(c => c.CardStats.GeneratedCardId == cardId);
            if (card == null)
                return;

            card.CardViewObject.GetComponent<CardAllyHelperComponent>().enabled = false;

            AllyCards.Remove(card);
            card.CardViewObject.transform.DOMove(new Vector3(-2.47f, 0.05f, 5.2f), 1f).OnComplete(() =>
            {
                card.CardViewObject.SetActive(false);
                UpdatePositions(false, false);
            });
        }
    }

    private void Move(GameObject card, OddPlacementPosition oddPosition, Sequence seq)
    {
        seq.Insert(0, card.transform.DOMove(SlotContainer.OddSlots[oddPosition].transform.position, 0.85f));
    }

    private void Move(GameObject card, EvenPlacementPosition evenPosition, Sequence seq)
    {
        seq.Insert(0, card.transform.DOMove(SlotContainer.EvenSlots[evenPosition].transform.position, 0.85f));
    }
}
