using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GroupRequestDeniedByServerEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GroupRequestDeniedByServerModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.GroupRequestDeniedByServer;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        view.MessageBoxManager.ShowMessage("Group request declined. " + model.DenialReason.ToString());

    }
}
