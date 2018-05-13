
public interface IEventHandler
{
    byte EventCode { get; }
    void HandleEvent(View view, string serializedParameters);
}
