using ExitGames.Client.Photon;
using System.Collections.Generic;

public interface IPhotonEventHandler
{
    byte Code { get; }
    void HandleEvent(Dictionary<byte,object> eventParameters);
    void OnHandleEvent(Dictionary<byte, object> eventParameters);
}
