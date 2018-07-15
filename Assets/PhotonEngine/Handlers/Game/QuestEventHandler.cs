using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using DG.Tweening;

public class QuestEventHandler<TModel> : BaseEventHandler<TModel> where TModel : QuestProgressionModel
{
    public QuestEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.CallbackSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.Quest;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var board = view as BoardView;
        board.QuestSlotManager.ProgressQuest(model.ProgressionValue);
        var card = board.BoardManager.GetCard(model.QuestingSourceId);
        if (card != null && card.CardStats.CardType == AsjernasCG.Common.BusinessModels.CardModels.CardType.Character && card.LastPosition.HasValue)
        {
            card.CardViewObject.transform.DOMove(card.LastPosition.Value, 0.3f).SetEase(Ease.InOutQuint);
        }
    }
}