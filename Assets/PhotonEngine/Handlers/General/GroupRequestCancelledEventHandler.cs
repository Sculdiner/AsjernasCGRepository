using AsjernasCG.Common.BusinessModels.BasicModels;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GroupRequestCancelledEventHandler<TModel> : BaseEventHandler<TModel> where TModel : EmptyModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.GroupRequestCancelled;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (view is MainMenuView)
        {
            (view as MainMenuView).InvitationManager.CancelInvitation();
        }
        else if (view is MainMenuPlayAreaView)
        {
            (view as MainMenuPlayAreaView).InvitationManager.CancelInvitation();
        }
    }
}
