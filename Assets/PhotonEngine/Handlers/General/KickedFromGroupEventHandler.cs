using AsjernasCG.Common.BusinessModels.BasicModels;
using AsjernasCG.Common.ClientEventCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class KickedFromGroupEventHandler<TModel> : BaseEventHandler<TModel> where TModel : EmptyModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.KickedFromGroup;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        GroupManager.Instance.ClearGroup();
        view.ChangeScene("MainMenu");
    }
}

