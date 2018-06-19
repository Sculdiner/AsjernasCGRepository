using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using System.Collections.Generic;

public class ResourceChangeBatchEventHandler<TModel> : BaseEventHandler<TModel> where TModel : List<ResourceChangeModel>
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.ResourceChangeBatch;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
