using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class AddBuffCardEventHandler<TModel> : BaseEventHandler<TModel> where TModel : BuffModel
{
    public AddBuffCardEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.BuffCard;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        
    }
}

