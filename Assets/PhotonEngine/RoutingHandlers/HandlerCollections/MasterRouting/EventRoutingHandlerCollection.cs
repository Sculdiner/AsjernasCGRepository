using System.Collections.Generic;

public static class EventRoutingHandlerCollection
{
    private static Dictionary<byte, IRoutingEventHandler> _handlers = new Dictionary<byte, IRoutingEventHandler>();
    public static void AddHandler(IRoutingEventHandler eventHandler)
    {
        if (_handlers.ContainsKey((byte)eventHandler.RegisteredEventGroupCode))
            _handlers[(byte)eventHandler.RegisteredEventGroupCode] = eventHandler;
        else
            _handlers.Add((byte)eventHandler.RegisteredEventGroupCode, eventHandler);
    }
    public static IRoutingEventHandler GetHandler(byte eventGroupCode)
    {
        if (_handlers.ContainsKey(eventGroupCode))
            return _handlers[eventGroupCode];
        return null;
    }
}
