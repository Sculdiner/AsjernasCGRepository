﻿using AsjernasCG.Common;
using AsjernasCG.Common.ClientEventCodes;
using System.Collections.Generic;

public abstract class BaseRoutingEventHandler : IEventRoutingHandler
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
        try
        {
            SubEventHandlerCollection
            .GetHandler((byte)parameters[(byte)PacketCodeType.PacketSubRouting])
            .HandleEvent(view, parameters[(byte)PacketCodeType.PacketParameters] as string);
        }
        catch (System.Exception ex)
        {

            
        }
    }
}
