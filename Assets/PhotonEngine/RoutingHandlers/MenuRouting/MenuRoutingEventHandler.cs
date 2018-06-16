using AsjernasCG.Common.BusinessModels.Deck;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using AsjernasCG.Common.EventModels.MainMenuPlay;
using System.Collections.Generic;

public class MenuRoutingEventHandler : BaseRoutingEventHandler
{
    public MenuRoutingEventHandler()
    {
        _subEventHandlerCollection = new SubEventHandlerCollection();
        _subEventHandlerCollection.AddHandler(new UserDecksEventHandler<List<DeckModel>>());
        _subEventHandlerCollection.AddHandler(new GameInitiationReadyStatusChangedEventHandler<GameInitiationReadyStatusChangedModel>());
        _subEventHandlerCollection.AddHandler(new DeckSelectionChangedEventHandler<DeckSelectionModel>());
    }
    public override ClientEventGroupCode RegisteredEventGroupCode
    {
        get { return ClientEventGroupCode.Menu; }
    }
    private SubEventHandlerCollection _subEventHandlerCollection;
    public override SubEventHandlerCollection SubEventHandlerCollection { get { return _subEventHandlerCollection; } }
}
