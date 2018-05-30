using AsjernasCG.Common.BusinessModels;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using System;
using System.Collections.Generic;

public class FriendListUpdateEventHandler<TModel> : BaseEventHandler<TModel> where TModel : List<FriendStatusModel>
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.FriendListUpdate;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        foreach (var friend in model)
        {
            var _model = new FriendListItemViewModel()
            {
                UserId = friend.UserId,
                Status = friend.UserStatus,
                Username = friend.UserName
            };
            if (friend.UserStatus == UserStatusModel.Disconnected)
            {
                var offlineDesc = "Offline";
                if (friend.LastConnection.HasValue)
                {
                    var tDif = DateTime.UtcNow - friend.LastConnection.Value;
                    var minutesTillOffline = tDif.TotalMinutes;
                    var daysTillOffline = tDif.TotalDays;
                    var hoursTillOffline = tDif.TotalHours;
                    var secsTillOffline = tDif.TotalSeconds;

                    if (daysTillOffline > 0) { offlineDesc = String.Format("Offline for {0} day(s)", daysTillOffline); }
                    else if (hoursTillOffline > 0) { offlineDesc = String.Format("Offline for {0} hour(s)", hoursTillOffline); }
                    else if (minutesTillOffline > 0) { offlineDesc = String.Format("Offline for {0} minute(s)", minutesTillOffline); }
                    else if (secsTillOffline > 0) { offlineDesc = "Offline for less than a minute"; }
                }
                _model.StatusDescription = offlineDesc;
            }
            else
            {
                _model.StatusDescription = friend.UserStatus.ToString();
            }
            view.OnFriendStatusUpdate(_model);
        }


    }
}