using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DrawCardDetailedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.DrawCardVisible;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        view.LogInfo("Draw Card. id:" + model.GeneratedCardId);
        view.MessageBoxManager.ShowMessage("Draw Card. id:" + model.GeneratedCardId);
    }
}