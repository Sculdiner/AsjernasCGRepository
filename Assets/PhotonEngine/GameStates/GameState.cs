using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;


public class GameState : IGameState
{
    protected PhotonEngine _engine;
    public string StateName { get; protected set; }
    protected GameState(PhotonEngine engine)
    {
        _engine = engine;
    }

    //do nothing
    public virtual void OnUpdate()
    {

    }

    //do nothing
    public virtual void SendOperation(OperationRequest request, bool sendReliable, byte channelId, bool encrypt)
    {

    }
}

