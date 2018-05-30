using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FriendStatusChangedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : FriendStatusModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.FriendStatusChanged;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var info = string.Empty;
        switch (model.UserStatus)
        {
            case AsjernasCG.Common.BusinessModels.UserStatusModel.Disconnected:
                info = model.UserName + " has gone offline";
                break;
            case AsjernasCG.Common.BusinessModels.UserStatusModel.Away:
                info = model.UserName + " is now away";
                break;
            case AsjernasCG.Common.BusinessModels.UserStatusModel.DND:
                info = model.UserName + " set their status to Do Not Disturb";
                break;
            case AsjernasCG.Common.BusinessModels.UserStatusModel.Connected:
                info = model.UserName + " has come online";
                break;
            default:
                break;
        }

        view.OnFriendStatusUpdate(new FriendListItemViewModel()
        {
            Status = model.UserStatus,
            StatusDescription = model.UserStatus.ToString(),
            UserId = model.UserId,
            Username = model.UserName
        });
    }
}

