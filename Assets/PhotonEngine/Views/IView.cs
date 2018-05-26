using UnityEngine;
using System.Collections;
using System;
using AsjernasCG.Common;

public interface IView
{
    IViewController Controller { get; }
    RoutingOperationCode GetRoutingOperationCode();
    void LogDebug(string message);
    void LogError(string message);
    void LogError(Exception exception);
    void LogInfo(string message);
    void Disconnected(string message);
    void ChangeScene(string newScene);
}