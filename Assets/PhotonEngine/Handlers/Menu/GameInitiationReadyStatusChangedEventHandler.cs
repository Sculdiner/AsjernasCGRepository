using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.MainMenuPlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GameInitiationReadyStatusChangedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GameInitiationReadyStatusChangedModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientMenuEventCode.GameInitiationReadyStatusChanged;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (view is MainMenuPlayAreaView)
        {
            var castedView = (MainMenuPlayAreaView)view;

            castedView.OnTeammateGameInititiationReadyStatusChanged(model.IsReady);
        }
    }
}