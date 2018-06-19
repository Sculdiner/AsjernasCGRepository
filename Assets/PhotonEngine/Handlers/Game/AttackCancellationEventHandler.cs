using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;

public class AttackCancellationEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.AttackCancellation;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
