
using ExitGames.Client.Photon;
using System.Collections.Generic;

public abstract class PhotonEventHandler : IPhotonEventHandler
{
    protected readonly ViewController _controller;
    public abstract byte Code { get;}
    public virtual int? SubCode { get { return null; } }
    protected PhotonEventHandler(ViewController controller)
    {
        _controller = controller;
    }

    public delegate void BeforeEventReceived();
    public BeforeEventReceived beforeEventReceived;
     
    public delegate void AfterEventReceived();
    public AfterEventReceived afterEventReceived;

    public void HandleEvent(Dictionary<byte,object> parameters)
    {
        if (beforeEventReceived !=null)
        {
            beforeEventReceived();
        }
        OnHandleEvent(parameters);
        if (afterEventReceived !=null)
        {
            afterEventReceived();
        }
    }

    public abstract void OnHandleEvent(Dictionary<byte, object> parameters);
}
