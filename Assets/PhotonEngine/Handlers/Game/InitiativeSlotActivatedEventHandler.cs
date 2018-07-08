using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;
using HighlightingSystem;
using UnityEngine;

public class InitiativeSlotActivatedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public InitiativeSlotActivatedEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.InitiativeSlotActivated;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardview = (view as BoardView);
        boardview.BoardManager.ActivateInitiativeSlot(model.Value);
        
        //Debug.Log($"Active character initiative slot: {boardview.BoardManager.ActiveCharacterManager.CardManager.Template.CardName}");
    }
}
