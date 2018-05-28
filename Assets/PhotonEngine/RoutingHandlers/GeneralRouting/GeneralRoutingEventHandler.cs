using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using System.Collections.Generic;

public class GeneralRoutingEventHandler : BaseRoutingEventHandler
{
    public GeneralRoutingEventHandler()
    {
        _subEventHandlerCollection = new SubEventHandlerCollection();
        _subEventHandlerCollection.AddHandler(new FriendStatusChangedEventHandler<FriendStatusModel>());
        _subEventHandlerCollection.AddHandler(new FriendListUpdateEventHandler<List<FriendStatusModel>>());
    }
    public override ClientEventGroupCode RegisteredEventGroupCode
    {
        get { return ClientEventGroupCode.General; }
    }
    private SubEventHandlerCollection _subEventHandlerCollection;
    public override SubEventHandlerCollection SubEventHandlerCollection { get { return _subEventHandlerCollection; } }
}
