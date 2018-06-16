using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GroupRequestNotificationEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GroupRequestInitializeModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.GroupRequestInitialize;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (view is MainMenuPlayAreaView)
        {
            ((MainMenuPlayAreaView)view).OnInvited(model.GroupLeaderId, model.UserName);
        }
        else if (view is MainMenuView)
        {
            ((MainMenuView)view).OnInvited(model.GroupLeaderId, model.UserName);
        }
    }
}
