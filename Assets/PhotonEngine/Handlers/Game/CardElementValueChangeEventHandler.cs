using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
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
        if (card != null)
        {
            card.CardManager.VisualStateManager.CurrentState.UpdateVisual(card.CardStats);
        }

    }
}
