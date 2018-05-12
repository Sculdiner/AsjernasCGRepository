﻿using AsjernasCG.Common;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class LoginController : ViewController
{
    public LoginView _view;

    public LoginController(View controlledView, RoutingOperationCode routingOperationCode) : base(controlledView, routingOperationCode)
    {
        _view = controlledView as LoginView;
        //EventHandlers.Add((byte)MessageSubCode.Login, new LoginHandler(this));
    }

    public void SendLoginMessage(string message)
    {
        _view.hasNewMessage = true;
        _view.newMessageText = message;
    }

    public void SendLogin(string username, string password)
    {
        SendOperation(new OperationRequest()
        {
            OperationCode = (byte)RoutingOperationCode.Login,
            Parameters = new Dictionary<byte, object>() {
                { (byte)OperationCodeType.SubOperationRouting, 0}
            }
        }, true, 0, false);
    }

    public void SendRegister(string username, string password, string email)
    {
        //var parameters = new Dictionary<byte, object>()
        //{
        //    {(byte)ClientParameterCode.UserName,username },
        //    {(byte)ClientParameterCode.Password,password},
        //    {(byte)ClientParameterCode.Email,email},
        //    {(byte)ClientParameterCode.SubOperationCode,(int)MessageSubCode.Register}
        //};
        //SendOperation(new OperationRequest()
        //{
        //    OperationCode = (byte)ClientOperationCode.Login,
        //    Parameters = parameters
        //}, true, 0, false);
    }
}
