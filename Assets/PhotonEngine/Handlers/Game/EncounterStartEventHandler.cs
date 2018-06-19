using AsjernasCG.Common.BusinessModels.BasicModels;
using AsjernasCG.Common.ClientEventCodes;

public class EncounterStartEventHandler<TModel> : BaseEventHandler<TModel> where TModel : EmptyModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.EncounterStart;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}