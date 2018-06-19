using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class QuestEventHandler<TModel> : BaseEventHandler<TModel> where TModel : QuestProgressionModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.Quest;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}