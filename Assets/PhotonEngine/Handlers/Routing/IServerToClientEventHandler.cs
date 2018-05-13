using AsjernasCG.Common.ClientEventCodes;
using System.Collections.Generic;

public interface IRoutingEventHandler
{
    ClientEventGroupCode RegisteredEventGroupCode { get; }
    void HandleEvent(View view, Dictionary<byte, object> parameters);
}
