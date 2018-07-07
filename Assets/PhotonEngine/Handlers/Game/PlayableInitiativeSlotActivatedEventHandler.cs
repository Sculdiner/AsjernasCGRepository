using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;
using HighlightingSystem;
using UnityEngine;

public class PlayableInitiativeSlotActivatedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayableInitiativeSlotActivated;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardview = (view as BoardView);
        //boardview.BoardManager.ActivatePlayableSlot(model.Value);
        
        Debug.Log($"Active character initiative slot: {boardview.BoardManager.ActiveCharacterManager.CardManager.Template.CardName}");
    }
}
