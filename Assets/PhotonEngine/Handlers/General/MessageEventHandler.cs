using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MessageEventHandler<TModel> : BaseEventHandler<TModel> where TModel : MessageEventModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGeneralEventCode.Message;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (view.MessageBoxManager != null)
        {
            view.MessageBoxManager.ShowMessage(model.MessageCode.ToString() + model.Description);
        }
    }
}

