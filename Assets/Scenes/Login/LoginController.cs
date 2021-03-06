﻿using AsjernasCG.Common;
using AsjernasCG.Common.OperationHelpers;
using AsjernasCG.Common.OperationHelpers.Login;
using AsjernasCG.Common.OperationHelpers.Menu;
using AsjernasCG.Common.OperationModels;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class LoginController : ViewController
{
    public LoginView _view;

    public LoginController(View controlledView) : base(controlledView)
    {
        _view = controlledView as LoginView;
    }

    public void SendLoginMessage(string message)
    {
        _view.hasNewMessage = true;
        _view.newMessageText = message;
    }

    public void SendLogin(string email, string password)
    {
        SendOperation(new LoginOperationHelper<LoginOperationModel>(new LoginOperationModel()
        {
            Email = email,
            Password = password
        }), true, 0, false);
    }

    public void LoadDeckTest(int deckId)
    {
        SendOperation(new MatchmakeRequestOperationHelper<MatchmakeRequestModel>(new MatchmakeRequestModel()
        {
            DeckId = deckId,
            GameType = "casual"
        }), true, 0, false);
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
