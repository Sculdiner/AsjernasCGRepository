using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;
using UnityEngine;

public class QuestActionCancellationEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.QuestActionCancellation;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var board = view as BoardView;
        var cardToCancel = board.BoardManager.GetCard(model.Value);
        if (cardToCancel != null)
        {
            var draggable = cardToCancel.CardViewObject.GetComponent<Draggable>();
            if (draggable != null)
                draggable.ForceKillDraggingAction();
        }
        else
            Debug.Log($"Tried to cancel an action (quest) of a non-registered card. Registered card: {model.Value}");
    }
}
