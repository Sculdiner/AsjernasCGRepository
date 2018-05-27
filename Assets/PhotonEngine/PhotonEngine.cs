using AsjernasCG.Common.ClientEventCodes;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{
    public PhotonPeer Peer { get; protected set; }
    public GameState State { get; protected set; }
    public ViewController Controller { get; set; }
    public string ServerAddress;
    public string ApplicationName;
    public static string GameStateName { get; protected set; }
    #region Implementation of MonoBehaviour
    private static PhotonEngine _instance;
    public void Awake()
    {
        _instance = this;
    }

    public void OnDestroy()
    {
        
    }

    public void Start()
    {
        DontDestroyOnLoad(this);
        State = new Disconnected(_instance);
        Application.runInBackground = true;
        Initialize();
    }

    public static PhotonEngine Instance
    {
        get { return _instance; }
    }

    public void Initialize()
    {
        EventRoutingHandlerCollection.AddHandler(new LoginRoutingEventHandler());
        EventRoutingHandlerCollection.AddHandler(new MenuRoutingEventHandler());
        EventRoutingHandlerCollection.AddHandler(new GameRoutingEventHandler());
        EventRoutingHandlerCollection.AddHandler(new GeneralRoutingEventHandler());

        Peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        Peer.Connect(ServerAddress, ApplicationName);
        State = new WaitingForConnection(_instance);
    }

    public void Disconnect()
    {
        if (Peer != null)
        {
            Peer.Disconnect();
        }
        State = new Disconnected(_instance);
    }

    public void Update()
    {
        State.OnUpdate();
    }

    public void SetOp(OperationRequest request, bool sendReliable, byte channelId, bool encrypt)
    {
        State.SendOperation(request, sendReliable, channelId, encrypt);
    }

    public static void UseExistingOrCreateNewPhotonEngine(string serverAddress, string applicationName)
    {
        var photonEngineObject = GameObject.Find("PhotonEngine");
        if (photonEngineObject == null)
        {
            photonEngineObject = new GameObject("PhotonEngine");
            photonEngineObject.AddComponent<PhotonEngine>();
        }
        var photonEngineComponent = photonEngineObject.GetComponent<PhotonEngine>();
        photonEngineComponent.ApplicationName = applicationName;
        photonEngineComponent.ServerAddress = serverAddress;
    }

    public static void ChangeConnectionOrReconnect(string serverAddress, string applicationName)
    {
        GameObject tempEngine = GameObject.Find("PhotonEngine");
        if (tempEngine != null)
        {
            var comp  = tempEngine.GetComponent<PhotonEngine>();
            comp.Disconnect();
            DestroyImmediate(comp);
            tempEngine.AddComponent<PhotonEngine>();
        }
        UseExistingOrCreateNewPhotonEngine(serverAddress, applicationName);
    }

    #endregion

    #region Implementation of IPhotonPeerListener

    public void DebugReturn(DebugLevel level, string message)
    {
        Controller.DebugReturn(level, message);
    }

    public void OnEvent(EventData eventData)
    {
        Controller.OnEvent(eventData);
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Controller.OnOperationResponse(operationResponse);
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Peer.EstablishEncryption();
                break;
            case StatusCode.Disconnect:
            case StatusCode.DisconnectByServer:
            case StatusCode.DisconnectByServerLogic:
            case StatusCode.DisconnectByServerUserLimit:
            case StatusCode.Exception:
            case StatusCode.ExceptionOnConnect:
            case StatusCode.TimeoutDisconnect:
                Controller.OnDisconnected("" + statusCode);
                State = new Disconnected(_instance);
                break;
            case StatusCode.EncryptionEstablished:
                State = new Connected(_instance);
                break;
            default:
                Controller.OnUnexpectedStatusCode(statusCode);
                State = new Disconnected(_instance);
                break;
        }
        GameStateName = State.StateName;
    }
    #endregion
}
