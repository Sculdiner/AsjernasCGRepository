using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;


public class EncounterSlotManager : MonoBehaviour
{
    public List<ClientSideCard> EncounterCards { get; set; }
    public EncounterSlotsPositionContainer SlotsContainer;
    private object positionUpdaterLocker = new object();
    private void Awake()
    {
        EncounterCards = new List<ClientSideCard>();
    }

    //public List<EncounterSlot>

    public void UpdatePositions()
    {
        Sequence mySequence = DOTween.Sequence();
        switch (EncounterCards.Count)
        {
            case 0:
                break;
            case 1:
                Move(EncounterCards.First().CardViewObject, OddEncounterPlacementPosition.Middle, mySequence);
                break;
            case 2:
                Move(EncounterCards[0].CardViewObject, EvenEncounterPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[1].CardViewObject, EvenEncounterPlacementPosition.MiddleRight, mySequence);
                break;
            case 3:
                Move(EncounterCards[0].CardViewObject, OddEncounterPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[1].CardViewObject, OddEncounterPlacementPosition.Middle, mySequence);
                Move(EncounterCards[2].CardViewObject, OddEncounterPlacementPosition.MiddleRight, mySequence);
                break;
            case 4:
                Move(EncounterCards[0].CardViewObject, EvenEncounterPlacementPosition.Left, mySequence);
                Move(EncounterCards[1].CardViewObject, EvenEncounterPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[2].CardViewObject, EvenEncounterPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[3].CardViewObject, EvenEncounterPlacementPosition.Right, mySequence);
                break;
            case 5:
                Move(EncounterCards[0].CardViewObject, OddEncounterPlacementPosition.Left, mySequence);
                Move(EncounterCards[1].CardViewObject, OddEncounterPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[2].CardViewObject, OddEncounterPlacementPosition.Middle, mySequence);
                Move(EncounterCards[3].CardViewObject, OddEncounterPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[4].CardViewObject, OddEncounterPlacementPosition.Right, mySequence);
                break;
            case 6:
                Move(EncounterCards[0].CardViewObject, EvenEncounterPlacementPosition.LeftFar, mySequence);
                Move(EncounterCards[1].CardViewObject, EvenEncounterPlacementPosition.Left, mySequence);
                Move(EncounterCards[2].CardViewObject, EvenEncounterPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[3].CardViewObject, EvenEncounterPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[4].CardViewObject, EvenEncounterPlacementPosition.Right, mySequence);
                Move(EncounterCards[5].CardViewObject, EvenEncounterPlacementPosition.RightFar, mySequence);
                break;
            case 7:
                Move(EncounterCards[0].CardViewObject, OddEncounterPlacementPosition.LeftFar, mySequence);
                Move(EncounterCards[1].CardViewObject, OddEncounterPlacementPosition.Left, mySequence);
                Move(EncounterCards[2].CardViewObject, OddEncounterPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[3].CardViewObject, OddEncounterPlacementPosition.Middle, mySequence);
                Move(EncounterCards[4].CardViewObject, OddEncounterPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[5].CardViewObject, OddEncounterPlacementPosition.Right, mySequence);
                Move(EncounterCards[6].CardViewObject, OddEncounterPlacementPosition.RightFar, mySequence);
                break;
            case 8:
                Move(EncounterCards[0].CardViewObject, EvenEncounterPlacementPosition.LeftLast, mySequence);
                Move(EncounterCards[1].CardViewObject, EvenEncounterPlacementPosition.LeftFar, mySequence);
                Move(EncounterCards[2].CardViewObject, EvenEncounterPlacementPosition.Left, mySequence);
                Move(EncounterCards[3].CardViewObject, EvenEncounterPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[4].CardViewObject, EvenEncounterPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[5].CardViewObject, EvenEncounterPlacementPosition.Right, mySequence);
                Move(EncounterCards[6].CardViewObject, EvenEncounterPlacementPosition.RightFar, mySequence);
                Move(EncounterCards[7].CardViewObject, EvenEncounterPlacementPosition.RightLast, mySequence);
                break;
        }
        if (EncounterCards.Count >0)
        {
            mySequence.OnComplete(() => { PhotonEngine.CompletedAction(); });
        }
        else
        {
            PhotonEngine.CompletedAction();
        }
    }

    public void AddEncounterCardToASlot(ClientSideCard clientSideCard)
    {
        lock (positionUpdaterLocker)
        {
            if (EncounterCards.Count < 8)
            {
                EncounterCards.Add(clientSideCard);
                UpdatePositions();
            }
        }
    }

    public void AddEncounterCardToPosition(ClientSideCard clientSideCard, int index)
    {
        lock (positionUpdaterLocker)
        {
            if (EncounterCards.Count < 8)
            {
                EncounterCards.Insert(index, clientSideCard);
                UpdatePositions();
            }
        }
    }

    public void RemoveEncounterCard(int cardId)
    {
        lock (positionUpdaterLocker)
        {
            var card = EncounterCards.FirstOrDefault(c => c.CardStats.GeneratedCardId == cardId);
            if (card == null)
                return;

            EncounterCards.Remove(card);
            card.CardViewObject.transform.DOMove(new Vector3(-2.47f, 0.05f, 5.2f), 1f).OnComplete(()=> {
                card.CardViewObject.SetActive(false);
                UpdatePositions();
            });
        }
    }

    private void Move(GameObject card, OddEncounterPlacementPosition oddPosition, Sequence seq)
    {
        seq.Insert(0, card.transform.DOMove(SlotsContainer.OddSlots[oddPosition].GetMyWorldPosition(), 0.85f));
    }

    private void Move(GameObject card, EvenEncounterPlacementPosition evenPosition, Sequence seq)
    {
        seq.Insert(0, card.transform.DOMove(SlotsContainer.EvenSlots[evenPosition].GetMyWorldPosition(), 0.85f));
    }
}
