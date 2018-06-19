using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class ResourceChangeEventHandler<TModel> : BaseEventHandler<TModel> where TModel : ResourceChangeModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.ResourceChange;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
