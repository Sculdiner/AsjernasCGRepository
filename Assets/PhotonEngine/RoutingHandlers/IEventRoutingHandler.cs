using AsjernasCG.Common.ClientEventCodes;
using System.Collections.Generic;

public interface IEventRoutingHandler
{
    ClientEventGroupCode RegisteredEventGroupCode { get; }
    void HandleEvent(View view, Dictionary<byte, object> parameters);
}
