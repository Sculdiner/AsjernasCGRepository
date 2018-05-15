using System.Collections.Generic;

public static class EventRoutingHandlerCollection
{
    private static Dictionary<byte, IEventRoutingHandler> _handlers = new Dictionary<byte, IEventRoutingHandler>();
    public static void AddHandler(IEventRoutingHandler eventHandler)
    {
        if (_handlers.ContainsKey((byte)eventHandler.RegisteredEventGroupCode))
            _handlers[(byte)eventHandler.RegisteredEventGroupCode] = eventHandler;
        else
            _handlers.Add((byte)eventHandler.RegisteredEventGroupCode, eventHandler);
    }
    public static IEventRoutingHandler GetHandler(byte eventGroupCode)
    {
        if (_handlers.ContainsKey(eventGroupCode))
            return _handlers[eventGroupCode];
        return null;
    }
}
