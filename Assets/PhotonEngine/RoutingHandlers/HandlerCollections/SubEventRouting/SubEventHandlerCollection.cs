using System.Collections.Generic;

public class SubEventHandlerCollection
{
    private Dictionary<byte, IEventHandler> _handlers;
    public SubEventHandlerCollection()
    {
        _handlers = new Dictionary<byte, IEventHandler>();
    }

    public void AddHandler(IEventHandler eventHandler)
    {
        if (_handlers.ContainsKey(eventHandler.EventCode))
            _handlers[eventHandler.EventCode] = eventHandler;
        else
            _handlers.Add(eventHandler.EventCode, eventHandler);
    }

    public IEventHandler GetHandler(byte eventCode)
    {
        if (_handlers.ContainsKey(eventCode))
            return _handlers[eventCode];
        return null;
    }
}