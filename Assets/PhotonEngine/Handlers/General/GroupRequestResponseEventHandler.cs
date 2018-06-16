using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GroupRequestResponseEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GroupRequestResponseModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.GroupRequestResponse;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (model.GroupAccepted)
        {
            GroupManager.Instance.AddNonLeaderPlayer(model.UserId, model.UserName);
            if (view is MainMenuPlayAreaView)
            {
                var castedView = ((MainMenuPlayAreaView)view);
                castedView.InviteToGroupFunctionalityArea.SetActive(false);
                castedView.TeammateArea.LoadArea(false, false, model.UserId, model.UserName, view as MainMenuPlayAreaView);
            }
        }
        else
        {
            view.MessageBoxManager.ShowMessage("Group request was declined");
        }

    }
}
