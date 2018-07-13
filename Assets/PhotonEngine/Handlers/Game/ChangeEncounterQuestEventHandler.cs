using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class ChangeEncounterQuestEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public ChangeEncounterQuestEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.CallbackSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.ChangeEncounterQuest;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardView = (view as BoardView);
        var card = boardView.BoardManager.GetCard(model.GeneratedCardId);
        if (card == null)
        {
            var cardPrefab = boardView.MasterCardManager.GenerateCardPrefab(model.CardTemplateId, model.GeneratedCardId);
            card = boardView.BoardManager.RegisterEncounterCard(cardPrefab, cardPrefab.GetComponent<CardManager>().Template, CardLocation.PlayArea);
        }
        boardView.QuestSlotManager.ChangeQuest(card);
    }
}
