using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsjernasCG.Common;
using AsjernasCG.Common.OperationHelpers;

public class ViewController : IViewController
{
    private readonly View _controlledView;
    private static string FIRSTSCENE_NAME { get { return "LoginScene"; } }
    public View ControlledView { get { return _controlledView; } }

    //private readonly Dictionary<byte, IPhotonOperationHandler> _operationHandlers = new Dictionary<byte, IPhotonOperationHandler>();
    private readonly Dictionary<byte, IEventHandler> _eventHandlers = new Dictionary<byte, IEventHandler>();
    public ViewController(View controlledView)
    {
        _controlledView = controlledView;

        if (!_controlledView.IsArtistDebug)
        {
            if (PhotonEngine.Instance == null)
            {
                SceneManager.LoadScene(FIRSTSCENE_NAME);
            }
            else
            {
                PhotonEngine.Instance.Controller = this;
            }
        }
    }

    //public Dictionary<byte, IPhotonOperationHandler> OperationHandlers
    //{
    //    get { return _operationHandlers; }
    //}

    public Dictionary<byte, IEventHandler> EventHandlers
    {
        get { return _eventHandlers; }
    }

    #region Implementation of IViewController
    public bool IsConnected
    {
        get
        {
            if (!_controlledView.IsArtistDebug && PhotonEngine.Instance != null)
            {
                return PhotonEngine.Instance.State is Connected;
            }
            return false;
        }
    }

    public void ApplicationQuit()
    {
        if (!_controlledView.IsArtistDebug && PhotonEngine.Instance != null)
            PhotonEngine.Instance.Disconnect();
    }

    public void DisconnectPeer()
    {
        if (!_controlledView.IsArtistDebug && PhotonEngine.Instance != null)
            PhotonEngine.Instance.Disconnect();
    }

    public void Connect()
    {
        if (!IsConnected)
        {
            if (!_controlledView.IsArtistDebug && PhotonEngine.Instance != null)
                PhotonEngine.Instance.Initialize();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        _controlledView.LogDebug(string.Format("{0} - {1}", level, message));
    }

    public void OnDisconnected(string message)
    {
        _controlledView.Disconnected(message);
    }

    public void OnEvent(EventData eventData)
    {
        EventRoutingHandlerCollection.GetHandler(eventData.Code).HandleEvent(_controlledView, eventData.Parameters);
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        //IPhotonOperationHandler handler;
        //if (operationResponse.Parameters.ContainsKey(_subOperationCode)
        //    && OperationHandlers.TryGetValue(Convert.ToByte(operationResponse.Parameters[_subOperationCode]), out handler))
        //{
        //    handler.HandleResponse(operationResponse);
        //}
        //else
        //{
        //    OnUnexpectedOperationResponse(operationResponse);
        //}
    }

    public void OnUnexpectedEvent(EventData eventData)
    {
        _controlledView.LogError(string.Format("Unexpected Event {0}", eventData.Code));
    }

    public void OnUnexpectedOperationResponse(OperationResponse operationResponse)
    {
        _controlledView.LogError(string.Format("Unexpected Operation error {0} from operation {1}", operationResponse.ReturnCode, operationResponse.OperationCode));
    }

    public void OnUnexpectedStatusCode(StatusCode statusCode)
    {
        _controlledView.LogError(string.Format("Unexpected Status  {0}", statusCode));
    }

    public void SendOperation<TInput>(IOperationHelper<TInput> operationHelper, bool sendReliable, byte channelId, bool encrypt) where TInput : class
    {
        var operationParams = operationHelper.GenerateOperationParameters();
        var operationRoutingCode = (byte)operationParams[(byte)PacketCodeType.PacketBaseRouting];
        operationParams.Remove((byte)PacketCodeType.PacketBaseRouting);
        var request = new OperationRequest()
        {
            OperationCode = operationRoutingCode,
            Parameters = operationParams
        };
        PhotonEngine.Instance.SetOp(request, sendReliable, channelId, encrypt);
    }

    public void UpdateUserProfile(int userId, string userName)
    {
        PhotonEngine.UserId = userId;
        PhotonEngine.Instance.UserName = userName;
    }

    public int GetUserId()
    {
        return PhotonEngine.UserId;
    }

    public string GetUserName()
    {
        return PhotonEngine.Instance.UserName;
    }

    #endregion
}
