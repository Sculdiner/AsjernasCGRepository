using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;

public class PlayableInitiativeSlotActivatedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayableInitiativeSlotActivated;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
