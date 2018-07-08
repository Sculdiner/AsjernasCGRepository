using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class HealDamageEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DamageEventModel
{
    public HealDamageEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.HealDamage;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var board = view as BoardView;
        var card = board.BoardManager.GetCard(model.CardId);
        if (card == null)
            return;

        card.HealDamage(model.Value);
    }
}