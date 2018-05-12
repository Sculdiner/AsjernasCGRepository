using ExitGames.Client.Photon;

public class WaitingForConnection :GameState
{
    public WaitingForConnection(PhotonEngine engine) : base(engine)
    {
        StateName = "Waiting for connection";
    }

    public override void OnUpdate()
    {
        _engine.Peer.Service();
    }

    public override void SendOperation(OperationRequest request, bool sendReliable, byte channelId, bool encrypt)
    {
        _engine.Peer.OpCustom(request, sendReliable, channelId, encrypt);
    }
}
