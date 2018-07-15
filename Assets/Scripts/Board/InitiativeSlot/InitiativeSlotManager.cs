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
    public Color ConstantSlotColor;
    public Gradient GradientHoverColor;
    private readonly object locker = new object();

    public void ClearAllHighlights()
    {
        foreach (var slot in CurrentSlots)
        {
            slot.Highlighter.constant = false;
        }
    }

    public void SetInitiativeSlot(List<int> cards)
    {
        lock (locker)
        {
            foreach (var item in CurrentSlots)
                Destroy(item.gameObject);

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
                newSlot.CardHighlightColor = GradientHoverColor;
                newSlot.SetCardInfo(card);
                CurrentSlots.Add(newSlot);
            }
            UpdatePositions();
        }
    }


    public void RemoveSlot(int cardId)
    {
        lock (locker)
        {
            var card = CurrentSlots?.FirstOrDefault(s => s.ReferencedCard.CardStats.GeneratedCardId == cardId);
            if (card != null)
            {
                if (ActiveSlot != null && ActiveSlot.ReferencedCard.CardStats.GeneratedCardId == cardId)
                {
                    ActiveSlot.Highlighter.constant = true;
                    ActiveSlot.Highlighter.constantColor = ConstantSlotColor;
                    ActiveSlot = null;
                }

                CurrentSlots.Remove(card);
                Destroy(card.gameObject);
                UpdatePositions();
            }
        }
    }

    //public void SetInitiativeSlot(ClientSideCard card)
    //{
    //    var newSlot = Instantiate(InitiativeSlotPrefab) as InitiativeSlot;
    //    newSlot.SetCardInfo(card);
    //    CurrentSlots.Add(newSlot);
    //    UpdatePositions();
    //}

    public void UpdatePositions()
    {
        for (int i = 0; i < CurrentSlots.Count; i++)
        {
            CurrentSlots[i].transform.position = SlotPositions[i].transform.position;
        }
    }

    public void ActivateSlot(int cardId)
    {
        lock (locker)
        {
            if (ActiveSlot != null)
            {
                ActiveSlot.Highlighter.constant = false;
            }
            //ActiveSlot?. played overlay
            ActiveSlot = CurrentSlots.FirstOrDefault(s => s.ReferencedCard.CardStats.GeneratedCardId == cardId);

            foreach (var currentSlot in CurrentSlots)
            {
                currentSlot.Highlighter.constant = false;
                //currentSlot.Highlighter.constant = false; played overlay
            }
            if (ActiveSlot != null)
            {
                ActiveSlot.Highlighter.constant = true;
                ActiveSlot.Highlighter.constantColor = ConstantSlotColor;
            }
        }

    }
}