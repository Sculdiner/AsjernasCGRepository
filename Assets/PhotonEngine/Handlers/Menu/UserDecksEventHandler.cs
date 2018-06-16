using AsjernasCG.Common.BusinessModels.Deck;
using AsjernasCG.Common.ClientEventCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UserDecksEventHandler<TModel> : BaseEventHandler<TModel> where TModel : List<DeckModel>
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientMenuEventCode.UserDecks;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        if (view is MainMenuPlayAreaView)
        {
            var castedView = (MainMenuPlayAreaView)view;
            Dictionary<int, string> deckList = new Dictionary<int, string>();
            foreach (var deck in model)
            {
                deckList.Add(deck.DeckId.Value, deck.Name);
            }
            castedView.UpdateUserDecks(deckList);
        }
    }
}
