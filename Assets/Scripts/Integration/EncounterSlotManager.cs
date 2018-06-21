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
        lock (positionUpdaterLocker)
        {
            switch (EncounterCards.Count)
            {
                case 0:
                    break;
                case 1:
                    Move(EncounterCards.First().CardViewObject, OddEncounterPlacementPosition.Middle);
                    break;
                case 2:
                    Move(EncounterCards[0].CardViewObject, EvenEncounterPlacementPosition.MiddleLeft);
                    Move(EncounterCards[1].CardViewObject, EvenEncounterPlacementPosition.MiddleRight);
                    break;
                case 3:
                    Move(EncounterCards[0].CardViewObject, OddEncounterPlacementPosition.MiddleLeft);
                    Move(EncounterCards[1].CardViewObject, OddEncounterPlacementPosition.Middle);
                    Move(EncounterCards[2].CardViewObject, OddEncounterPlacementPosition.MiddleRight);
                    break;
                case 4:
                    Move(EncounterCards[0].CardViewObject, EvenEncounterPlacementPosition.Left);
                    Move(EncounterCards[1].CardViewObject, EvenEncounterPlacementPosition.MiddleLeft);
                    Move(EncounterCards[2].CardViewObject, EvenEncounterPlacementPosition.MiddleRight);
                    Move(EncounterCards[3].CardViewObject, EvenEncounterPlacementPosition.Right);
                    break;
                case 5:
                    Move(EncounterCards[0].CardViewObject, OddEncounterPlacementPosition.Left);
                    Move(EncounterCards[1].CardViewObject, OddEncounterPlacementPosition.MiddleLeft);
                    Move(EncounterCards[2].CardViewObject, OddEncounterPlacementPosition.Middle);
                    Move(EncounterCards[3].CardViewObject, OddEncounterPlacementPosition.MiddleRight);
                    Move(EncounterCards[4].CardViewObject, OddEncounterPlacementPosition.Right);
                    break;
                case 6:
                    Move(EncounterCards[0].CardViewObject, EvenEncounterPlacementPosition.LeftFar);
                    Move(EncounterCards[1].CardViewObject, EvenEncounterPlacementPosition.Left);
                    Move(EncounterCards[2].CardViewObject, EvenEncounterPlacementPosition.MiddleLeft);
                    Move(EncounterCards[3].CardViewObject, EvenEncounterPlacementPosition.MiddleRight);
                    Move(EncounterCards[4].CardViewObject, EvenEncounterPlacementPosition.Right);
                    Move(EncounterCards[5].CardViewObject, EvenEncounterPlacementPosition.RightFar);
                    break;
                case 7:
                    Move(EncounterCards[0].CardViewObject, OddEncounterPlacementPosition.LeftFar);
                    Move(EncounterCards[1].CardViewObject, OddEncounterPlacementPosition.Left);
                    Move(EncounterCards[2].CardViewObject, OddEncounterPlacementPosition.MiddleLeft);
                    Move(EncounterCards[3].CardViewObject, OddEncounterPlacementPosition.Middle);
                    Move(EncounterCards[4].CardViewObject, OddEncounterPlacementPosition.MiddleRight);
                    Move(EncounterCards[5].CardViewObject, OddEncounterPlacementPosition.Right);
                    Move(EncounterCards[6].CardViewObject, OddEncounterPlacementPosition.RightFar);
                    break;
                case 8:
                    Move(EncounterCards[0].CardViewObject, EvenEncounterPlacementPosition.LeftLast);
                    Move(EncounterCards[1].CardViewObject, EvenEncounterPlacementPosition.LeftFar);
                    Move(EncounterCards[2].CardViewObject, EvenEncounterPlacementPosition.Left);
                    Move(EncounterCards[3].CardViewObject, EvenEncounterPlacementPosition.MiddleLeft);
                    Move(EncounterCards[4].CardViewObject, EvenEncounterPlacementPosition.MiddleRight);
                    Move(EncounterCards[5].CardViewObject, EvenEncounterPlacementPosition.Right);
                    Move(EncounterCards[6].CardViewObject, EvenEncounterPlacementPosition.RightFar);
                    Move(EncounterCards[7].CardViewObject, EvenEncounterPlacementPosition.RightLast);
                    break;
            }
        }
    }

    public void AddEncounterCardToASlot(ClientSideCard clientSideCard)
    {
        if (EncounterCards.Count < 8)
        {
            EncounterCards.Add(clientSideCard);
            UpdatePositions();
        }
    }

    public void RemoveEncounterCard(ClientSideCard clientSideCard)
    {

        var card = EncounterCards.FirstOrDefault(c => c.CardStats.GeneratedCardId == clientSideCard.CardStats.GeneratedCardId);
        if (card == null)
            return;

        EncounterCards.Remove(card);
        UpdatePositions();
    }

    private void Move(GameObject card, OddEncounterPlacementPosition oddPosition)
    {
        card.transform.DOMove(SlotsContainer.OddSlots[oddPosition].GetMyWorldPosition(), 0.3f);
    }

    private void Move(GameObject card, EvenEncounterPlacementPosition evenPosition)
    {
        card.transform.DOMove(SlotsContainer.EvenSlots[evenPosition].GetMyWorldPosition(), 0.3f);
    }
}
