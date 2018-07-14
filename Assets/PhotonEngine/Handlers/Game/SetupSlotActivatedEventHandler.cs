using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;

public class SetupSlotActivatedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public SetupSlotActivatedEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.SetupSlotActivated;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardview = (view as BoardView);
        boardview.BoardManager.ActiveCard?.CardManager.GetComponent<Draggable>()?.ForceKillDraggingAction();
        BoardView.Instance.TurnButton.FlipToWait();
        boardview.BoardManager.SetupSlotActivated(model.Value);
        //boardview.BoardManager.ActiveCharacterManager?.CardManager.VisualStateManager.EndHighlight();
        //boardview.BoardManager.ActiveCharacterManager = boardview.BoardManager.GetCard(model.Value).CardManager.CharacterManager;
        //boardview.BoardManager.ActiveCharacterManager?.CardManager.VisualStateManager.Hightlight();
    }
}