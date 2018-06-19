using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class GameEndEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GameEndWithRewardsModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.GameEnd;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
