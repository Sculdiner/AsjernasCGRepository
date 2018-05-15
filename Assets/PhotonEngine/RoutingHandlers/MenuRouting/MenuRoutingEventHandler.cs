using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;

public class MenuRoutingEventHandler : BaseRoutingEventHandler
{
    public MenuRoutingEventHandler()
    {
        _subEventHandlerCollection = new SubEventHandlerCollection();
        //_subEventHandlerCollection.AddHandler(new LoginEventHandler<LoginResultModel>());
    }
    public override ClientEventGroupCode RegisteredEventGroupCode
    {
        get { return ClientEventGroupCode.Menu; }
    }
    private SubEventHandlerCollection _subEventHandlerCollection;
    public override SubEventHandlerCollection SubEventHandlerCollection { get { return _subEventHandlerCollection; } }
}
