using AsjernasCG.Common;
using AsjernasCG.Common.ClientEventCodes;
using System.Collections.Generic;

public abstract class BaseServerToClientEventHandler : IServerToClientEventHandler
{
    //The operation family code
    public abstract ClientEventGroupCode RegisteredEventGroupCode { get; }
    public abstract SubEventHandlerCollection SubEventHandlerCollection { get; }

    //The handler family that is available for this operation
    public virtual void OnBeforeHandle()
    {

    }
    public virtual void OnAfterHandle()
    {

    }

    public void HandleEvent(View view, Dictionary<byte, object> parameters)
    {
        SubEventHandlerCollection.GetHandler((byte)parameters[(byte)OperationCodeType.SubOperationRouting]).HandleEvent(view, parameters[(byte)OperationCodeType.SubOperationParameters] as string);
    }
}
