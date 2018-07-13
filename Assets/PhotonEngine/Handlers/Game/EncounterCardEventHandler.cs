using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class EncounterCardEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public EncounterCardEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.CallbackSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.EncounterCard;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardView = view as BoardView;
        boardView.BoardManager.EncounterCard(model.CardTemplateId, model.GeneratedCardId, () => { PhotonEngine.CompletedAction(); });
    }
}
