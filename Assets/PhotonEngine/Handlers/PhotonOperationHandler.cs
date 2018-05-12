
using ExitGames.Client.Photon;

public abstract class PhotonOperationHandler :IPhotonOperationHandler
{
    protected readonly ViewController _controller;
    public abstract byte Code { get; }

    protected PhotonOperationHandler(ViewController controller)
    {
        _controller = controller;
    }

    public delegate void BeforeResponseReceived();
    public BeforeResponseReceived beforeOperationReceived;

    public delegate void AfterResponseReceived();
    public AfterResponseReceived afterOperationReceived;

    public void HandleResponse(OperationResponse response)
    {
        if (beforeOperationReceived != null)
        {
            beforeOperationReceived();
        }
        OnHandleResponse(response);
        if (afterOperationReceived != null)
        {
            afterOperationReceived();
        }
    }

    public abstract void OnHandleResponse(OperationResponse response);
}
