using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class RemoveBuffCardEventHandler<TModel> : BaseEventHandler<TModel> where TModel : BuffModel
{
    public RemoveBuffCardEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.RemoveBuff;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {

    }
}

