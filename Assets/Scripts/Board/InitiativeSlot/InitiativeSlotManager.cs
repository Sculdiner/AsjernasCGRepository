using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InitiativeSlotManager : MonoBehaviour
{
    public InitiativeSlot InitiativeSlotPrefab;
    public List<Transform> SlotPositions;
    public List<InitiativeSlot> CurrentSlots = new List<InitiativeSlot>();
    public BoardManager BoardManager;
    public void SetInitiativeSlot(List<int> cards)
    {
        foreach (var item in cards)
        {
            var card = BoardManager.GetCard(item);
            var newSlot = Instantiate(InitiativeSlotPrefab) as InitiativeSlot;
            newSlot.SetCardInfo(card);
            CurrentSlots.Add(newSlot);
        }
        UpdatePositions();
    }
    public void SetInitiativeSlot(ClientSideCard card)
    {
        var newSlot = Instantiate(InitiativeSlotPrefab) as InitiativeSlot;
        newSlot.SetCardInfo(card);
        CurrentSlots.Add(newSlot);
        UpdatePositions();
    }

    public void UpdatePositions()
    {
        for (int i = 0; i < CurrentSlots.Count; i++)
        {
            CurrentSlots[i].transform.position = SlotPositions[i].transform.position;
        }
    }
}