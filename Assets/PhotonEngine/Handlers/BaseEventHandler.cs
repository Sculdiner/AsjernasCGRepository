using Newtonsoft.Json;


public abstract class BaseEventHandler<TModel> : IEventHandler where TModel : class
{
    public abstract byte EventCode { get; }

    public void HandleEvent(View view, string serializedParameters)
    {
        var model = JsonConvert.DeserializeObject<TModel>(serializedParameters);
        OnHandleEvent(view, model);
    }
    public abstract void OnHandleEvent(View view, TModel model);
}