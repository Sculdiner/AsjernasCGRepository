using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class InitiativeSlotsEventHandler<TModel> : BaseEventHandler<TModel> where TModel : InitiativeSlotsModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.InitiativeList;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
