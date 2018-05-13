using AsjernasCG.Common.ClientEventCodes;
using System.Collections.Generic;

public interface IServerToClientEventHandler
{
    ClientEventGroupCode RegisteredEventGroupCode { get; }
    void HandleEvent(View view, Dictionary<byte, object> parameters);
}
