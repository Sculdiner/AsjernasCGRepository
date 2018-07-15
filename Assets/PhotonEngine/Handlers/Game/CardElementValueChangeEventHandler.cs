using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardElementValueChangeEventHandler<TModel> : BaseEventHandler<TModel> where TModel : CardElementValueChange
{
    public CardElementValueChangeEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.ChangeCardElement;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardview = view as BoardView;
        var card = boardview.BoardManager.GetCard(model.CardGeneratedId);
        Debug.Log($"modifying value for the card: {card.CardStats.CardName}, to the value: {Int32.Parse(model.Value)}");
        switch (model.CardElementType)
        {
            case CardElementType.BaseResourceCost:
                card.CardStats.BaseResourceCost = Int32.Parse(model.Value);
                break;
            case CardElementType.Durability:
                card.CardStats.Durability = Int32.Parse(model.Value);
                break;
            case CardElementType.Power:
                card.CardStats.Power = Int32.Parse(model.Value);
                break;
            //case CardElementType.MaxHealth:
            //    card.CardStats.Heal = Int32.Parse(model.Value);
            //    break;
            case CardElementType.Threat:
                card.CardStats.Threat = Int32.Parse(model.Value);
                break;
            case CardElementType.Initiative:
                card.CardStats.Initiative = Int32.Parse(model.Value);
                break;
            case CardElementType.MaxCooldown:
                card.CardStats.InternalCooldownTarget = Int32.Parse(model.Value);
                break;
            case CardElementType.CurrentCooldown:
                card.CardStats.InternalCooldownCurrent = Int32.Parse(model.Value);
                break;
            default:
                break;
        }
        if (card != null)
        {
            card.CardManager.VisualStateManager.CurrentState.UpdateVisual(card.CardStats);
        }

    }
}
