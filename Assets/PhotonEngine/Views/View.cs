using System;
using AsjernasCG.Common;
using UnityEngine;

public abstract class View : MonoBehaviour, IView
{
    public virtual void Awake()
    {
        Controller = new ViewController(this, GetRoutingOperationCode());
    }

    public virtual void OnApplicationQuit()
    {
        Controller.ApplicationQuit();
    }

    #region Implementation of IView

    public abstract IViewController Controller { get; protected set; }



    public void LogDebug(string message)
    {
        Debug.Log(message);
    }

    public void LogError(Exception exception)
    {
        Debug.LogError(exception.ToString());
    }

    public void LogError(string message)
    {
        Debug.LogError(message);
    }

    public void LogInfo(string message)
    {
        Debug.Log(message);
    }

    public void Disconnected(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            Debug.Log(message);
        }

        if (Application.loadedLevel != 0)
        {
            Application.LoadLevel(0);
        }
    }

    public abstract RoutingOperationCode GetRoutingOperationCode();
    #endregion
}
