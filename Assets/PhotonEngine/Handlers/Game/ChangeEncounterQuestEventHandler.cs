using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class ChangeEncounterQuestEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.ChangeEncounterQuest;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
