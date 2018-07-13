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
    public InitiativeSlot ActiveSlot;
    public BoardManager BoardManager;
    public void SetInitiativeSlot(List<int> cards)
    {
        foreach (var item in CurrentSlots)
            DestroyImmediate(item);

        CurrentSlots.Clear();

        foreach (var item in cards)
        {
            var card = BoardManager.GetCard(item);
            if (card == null)
            {
                Debug.Log($"can't find init slot for card with id:{item}. Current card count: {BoardManager.DEBUG_SHOWCARDCOUNT()}");
                continue;
            }
            else
            {
                Debug.Log($"found init slot for card with id:{item}. Current card count: {BoardManager.DEBUG_SHOWCARDCOUNT()}");
            }

            var newSlot = Instantiate(InitiativeSlotPrefab) as InitiativeSlot;
            newSlot.SetCardInfo(card);
            CurrentSlots.Add(newSlot);
        }
        UpdatePositions();
    }


    public void RemoveSlot(int cardId)
    {
        var card = CurrentSlots?.FirstOrDefault(s => s.ReferencedCard.CardStats.GeneratedCardId == cardId);
        if (card != null)
        {
            if (ActiveSlot != null && ActiveSlot.ReferencedCard.CardStats.GeneratedCardId == cardId)
            {
                ActiveSlot.Highlighter.constant = true;
                ActiveSlot = null;
            }

            CurrentSlots.Remove(card);
            DestroyImmediate(card);
            UpdatePositions();
        }
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

    public void ActivateSlot(int cardId)
    {
        ActiveSlot?.Highlighter.constant = false;
        //ActiveSlot?. played overlay
        ActiveSlot = CurrentSlots.FirstOrDefault(s => s.ReferencedCard.CardStats.GeneratedCardId == cardId);
        ActiveSlot?.Highlighter.constant = true;

        foreach (var currentSlot in CurrentSlots)
        {
            currentSlot.Highlighter.constant = false;
            //currentSlot.Highlighter.constant = false; played overlay
        }
    }
}