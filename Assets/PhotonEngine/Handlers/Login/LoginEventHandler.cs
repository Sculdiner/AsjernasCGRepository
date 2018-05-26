using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LoginEventHandler<TModel> : BaseEventHandler<TModel> where TModel : LoginResultModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientLoginEventCode.LoggedIn;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (model.Success)
        {
            view.ChangeScene("MainMenu");
        }
    }
}
