using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common.BusinessModels.CardModels;

public class CardPlayCancelationEventHandler<TModel> : BaseEventHandler<TModel> where TModel : CardPlayCancelationReasonModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayCardFromHandCancelation;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var board = view as BoardView;
        var cardToCancel = board.BoardManager.GetCard(model.CardPlayIdToCancel);
        if (cardToCancel != null)
        {
            var draggable = cardToCancel.CardViewObject.GetComponent<Draggable>();
            if (draggable != null)
                draggable.ForceKillDraggingAction();
        }
        else
            Debug.Log($"Tried to cancel an action of a non-registered card. Registered card: {model.CardPlayIdToCancel}");
    }
}