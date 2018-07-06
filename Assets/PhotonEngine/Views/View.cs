using System;
using AsjernasCG.Common;
using AsjernasCG.Common.EventModels;
using AsjernasCG.Common.EventModels.General;
using AsjernasCG.Common.OperationHelpers.General;
using AsjernasCG.Common.OperationModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class View : MonoBehaviour, IView
{
    public bool IsArtistDebug = true;

    public MessageBoxManager MessageBoxManager;

    public virtual void Awake()
    {
        Controller = new ViewController(this);
    }

    public virtual void OnApplicationQuit()
    {
        Controller.ApplicationQuit();
    }

    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene, LoadSceneMode.Single);
    }

    public void RequestFriendListUpdate()
    {
        Controller.SendOperation(new GetFriendListOperationHelper<FriendListOperationModel>(new FriendListOperationModel() { FriendsToSkip = 0 }), true, 0, false);
    }

    public abstract void OnFriendStatusUpdate(FriendListItemViewModel friendStatusModel);

    #region Implementation of IView

    public abstract IViewController Controller { get; protected set; }



    public void LogDebug(string message)
    {
        //Debug.Log(message);
    }

    public void LogError(Exception exception)
    {
        //Debug.LogError(exception.ToString());
    }

    public void LogError(string message)
    {
        //Debug.LogError(message);
    }

    public void LogInfo(string message)
    {
        //Debug.Log(message);
    }

    public void Disconnected(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            //Debug.Log(message);
        }

        if (Application.loadedLevel != 0)
        {
            Application.LoadLevel(0);
        }
    }

    public abstract RoutingOperationCode GetRoutingOperationCode();
    #endregion
}
