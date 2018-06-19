using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class RoundStartEventHandler<TModel> : BaseEventHandler<TModel> where TModel : RoundStartModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.RoundStart;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
