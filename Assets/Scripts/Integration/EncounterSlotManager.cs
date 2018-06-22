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
                Move(EncounterCards.First().CardViewObject, OddPlacementPosition.Middle, mySequence);
                break;
            case 2:
                Move(EncounterCards[0].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[1].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence);
                break;
            case 3:
                Move(EncounterCards[0].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[1].CardViewObject, OddPlacementPosition.Middle, mySequence);
                Move(EncounterCards[2].CardViewObject, OddPlacementPosition.MiddleRight, mySequence);
                break;
            case 4:
                Move(EncounterCards[0].CardViewObject, EvenPlacementPosition.Left, mySequence);
                Move(EncounterCards[1].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[2].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[3].CardViewObject, EvenPlacementPosition.Right, mySequence);
                break;
            case 5:
                Move(EncounterCards[0].CardViewObject, OddPlacementPosition.Left, mySequence);
                Move(EncounterCards[1].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[2].CardViewObject, OddPlacementPosition.Middle, mySequence);
                Move(EncounterCards[3].CardViewObject, OddPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[4].CardViewObject, OddPlacementPosition.Right, mySequence);
                break;
            case 6:
                Move(EncounterCards[0].CardViewObject, EvenPlacementPosition.LeftFar, mySequence);
                Move(EncounterCards[1].CardViewObject, EvenPlacementPosition.Left, mySequence);
                Move(EncounterCards[2].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[3].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[4].CardViewObject, EvenPlacementPosition.Right, mySequence);
                Move(EncounterCards[5].CardViewObject, EvenPlacementPosition.RightFar, mySequence);
                break;
            case 7:
                Move(EncounterCards[0].CardViewObject, OddPlacementPosition.LeftFar, mySequence);
                Move(EncounterCards[1].CardViewObject, OddPlacementPosition.Left, mySequence);
                Move(EncounterCards[2].CardViewObject, OddPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[3].CardViewObject, OddPlacementPosition.Middle, mySequence);
                Move(EncounterCards[4].CardViewObject, OddPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[5].CardViewObject, OddPlacementPosition.Right, mySequence);
                Move(EncounterCards[6].CardViewObject, OddPlacementPosition.RightFar, mySequence);
                break;
            case 8:
                Move(EncounterCards[0].CardViewObject, EvenPlacementPosition.LeftLast, mySequence);
                Move(EncounterCards[1].CardViewObject, EvenPlacementPosition.LeftFar, mySequence);
                Move(EncounterCards[2].CardViewObject, EvenPlacementPosition.Left, mySequence);
                Move(EncounterCards[3].CardViewObject, EvenPlacementPosition.MiddleLeft, mySequence);
                Move(EncounterCards[4].CardViewObject, EvenPlacementPosition.MiddleRight, mySequence);
                Move(EncounterCards[5].CardViewObject, EvenPlacementPosition.Right, mySequence);
                Move(EncounterCards[6].CardViewObject, EvenPlacementPosition.RightFar, mySequence);
                Move(EncounterCards[7].CardViewObject, EvenPlacementPosition.RightLast, mySequence);
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

    private void Move(GameObject card, OddPlacementPosition oddPosition, Sequence seq)
    {
        seq.Insert(0, card.transform.DOMove(SlotsContainer.OddSlots[oddPosition].GetMyWorldPosition(), 0.85f));
    }

    private void Move(GameObject card, EvenPlacementPosition evenPosition, Sequence seq)
    {
        seq.Insert(0, card.transform.DOMove(SlotsContainer.EvenSlots[evenPosition].GetMyWorldPosition(), 0.85f));
    }
}
