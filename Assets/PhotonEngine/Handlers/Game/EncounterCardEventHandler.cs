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
        var cardPrefab = boardView.MasterCardManager.GenerateCardPrefab(model.CardTemplateId, model.GeneratedCardId);
        var ccc = boardView.BoardManager.RegisterEncounterCard(cardPrefab, boardView.MasterCardManager.GetCardManager(model.GeneratedCardId).Template, CardLocation.PlayArea);
        boardView.EncounterSlotManager.AddEncounterCardToASlot(ccc);
    }
}
