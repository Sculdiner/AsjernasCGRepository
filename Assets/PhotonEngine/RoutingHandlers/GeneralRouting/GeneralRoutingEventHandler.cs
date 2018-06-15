using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using AsjernasCG.Common.EventModels.General;
using Assets.PhotonEngine.Handlers.General;
using System.Collections.Generic;

public class GeneralRoutingEventHandler : BaseRoutingEventHandler
{
    public GeneralRoutingEventHandler()
    {
        _subEventHandlerCollection = new SubEventHandlerCollection();
        _subEventHandlerCollection.AddHandler(new FriendStatusChangedEventHandler<FriendStatusModel>());
        _subEventHandlerCollection.AddHandler(new FriendListUpdateEventHandler<List<FriendStatusModel>>());
        _subEventHandlerCollection.AddHandler(new FriendsAvailableForInviteEventHandler<List<FriendStatusModel>>());
        _subEventHandlerCollection.AddHandler(new GroupRequestDeniedByServerEventHandler<GroupRequestDeniedByServerModel>());
        _subEventHandlerCollection.AddHandler(new GroupRequestNotificationEventHandler<GroupRequestInitializeModel>());
        _subEventHandlerCollection.AddHandler(new GroupRequestResponseEventHandler<GroupRequestResponseModel>());
        _subEventHandlerCollection.AddHandler(new GroupStatusEventHandler<GroupStatusModel>());

    }
    public override ClientEventGroupCode RegisteredEventGroupCode
    {
        get { return ClientEventGroupCode.General; }
    }
    private SubEventHandlerCollection _subEventHandlerCollection;
    public override SubEventHandlerCollection SubEventHandlerCollection { get { return _subEventHandlerCollection; } }
}
