using Newtonsoft.Json;


public abstract class BaseEventHandler<TModel> : IEventHandler where TModel : class
{
    public abstract byte EventCode { get; }
    public UIActionSynchronizationType ActionSyncType { get; set; }

    public BaseEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.NoSync;
    }

    public void HandleEvent(View view, string serializedParameters)
    {
        var model = JsonConvert.DeserializeObject<TModel>(serializedParameters);

        if (ActionSyncType == UIActionSynchronizationType.NoSync)
            OnHandleEvent(view, model);
        else if (ActionSyncType == UIActionSynchronizationType.SerialSync)
        {
            PhotonEngine.AddToQueue("SerialSyncCallback", () =>
            {
                OnHandleEvent(view, model);
                PhotonEngine.CompletedAction();
            });
        }
        else if (ActionSyncType == UIActionSynchronizationType.CallbackSync)
        {
            PhotonEngine.AddToQueue("Callback", () =>
            {
                OnHandleEvent(view, model);
            });
        }
    }
    public abstract void OnHandleEvent(View view, TModel model);
}

public enum UIActionSynchronizationType
{
    //the action does not enqueue anywhere. it is invoked the moment it is handled
    NoSync = 0,
    SerialSync = 1,
    CallbackSync = 2
}