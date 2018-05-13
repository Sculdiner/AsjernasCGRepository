using System.Collections.Generic;

public static class EventRoutingHandlerCollection
{
    private static Dictionary<byte, IServerToClientEventHandler> _handlers = new Dictionary<byte, IServerToClientEventHandler>();
    public static void AddHandler(IServerToClientEventHandler eventHandler)
    {
        if (_handlers.ContainsKey((byte)eventHandler.RegisteredEventGroupCode))
            _handlers[(byte)eventHandler.RegisteredEventGroupCode] = eventHandler;
        else
            _handlers.Add((byte)eventHandler.RegisteredEventGroupCode, eventHandler);
    }
    public static IServerToClientEventHandler GetHandler(byte eventGroupCode)
    {
        if (_handlers.ContainsKey(eventGroupCode))
            return _handlers[eventGroupCode];
        return null;
    }
}
