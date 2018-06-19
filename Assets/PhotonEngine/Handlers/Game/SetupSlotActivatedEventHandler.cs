using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;

public class SetupSlotActivatedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.SetupSlotActivated;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}