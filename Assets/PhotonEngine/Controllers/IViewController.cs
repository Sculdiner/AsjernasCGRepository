using AsjernasCG.Common.OperationHelpers;
using ExitGames.Client.Photon;


public interface IViewController
{
    bool IsConnected { get; }
    void ApplicationQuit();
    void DisconnectPeer();
    void Connect();
    void SendOperation<TInput>(IOperationHelper<TInput> operationHelper, bool sendReliable, byte channelId, bool encrypt) where TInput : class;
    void UpdateUserProfile(int userId, string userName);
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

