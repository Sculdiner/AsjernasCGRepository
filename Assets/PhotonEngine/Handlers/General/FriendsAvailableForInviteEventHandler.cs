using AsjernasCG.Common.BusinessModels;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;

public class FriendsAvailableForInviteEventHandler<TModel> : BaseEventHandler<TModel> where TModel : List<FriendStatusModel>
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.FriendsAvailableForInvite;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var mainMenuView = view as MainMenuPlayAreaView;
        var finalFriends = new Dictionary<int, string>();
        foreach (var item in model)
        {
            finalFriends.Add(item.UserId, item.UserName);
        }
        mainMenuView.UpdateInviteList(finalFriends);
    }
}