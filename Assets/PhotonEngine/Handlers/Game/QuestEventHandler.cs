using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

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
    }
}