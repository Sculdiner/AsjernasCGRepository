using AsjernasCG.Common.OperationHelpers;
using ExitGames.Client.Photon;


public interface IViewController
{
    bool IsConnected { get; }
    void ApplicationQuit();
    void DisconnectPeer();
    void Connect();
    void SendOperation<TInput>(IOperationHelper<TInput> operationHelper, TInput input, bool sendReliable, byte channelId, bool encrypt) where TInput : class;
    #region
    void DebugReturn(DebugLevel level, string message);
    void OnOperationResponse(OperationResponse operationResponse);
    void OnEvent(EventData eventData);
    void OnUnexpectedEvent(EventData eventData);
    void OnUnexpectedOperationResponse(OperationResponse operationResponse);
    void OnUnexpectedStatusCode(StatusCode statusCode);
    void OnDisconnected(string message);
    #endregion
}

