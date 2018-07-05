using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class DrawCardHiddenEventHandler<TModel> : BaseEventHandler<TModel> where TModel : HiddenCardModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.DrawCardHidden;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        view.LogInfo("Draw Hidden. id:" + model.GeneratedCardId);
        //view.MessageBoxManager.ShowMessage("Draw Hidden. id:" + model.GeneratedCardId);
    }
}
