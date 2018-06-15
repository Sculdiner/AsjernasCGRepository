using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.MainMenuPlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DeckSelectionChangedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DeckSelectionModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientMenuEventCode.TeammateDeckSelectionChanged;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (view is MainMenuPlayAreaView)
        {
            var castedView = (MainMenuPlayAreaView)view;

            castedView.OnTeammateDeckSelectionChanged(model.DeckId, model.DeckName);
        }
    }
}