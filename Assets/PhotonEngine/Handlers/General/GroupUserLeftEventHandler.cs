using AsjernasCG.Common.BusinessModels.BasicModels;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GroupUserLeftEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.GroupUserLeft;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (view is MainMenuPlayAreaView)
        {
            (view as MainMenuPlayAreaView).OnLeaderRefreshView();
        }
    }
}

