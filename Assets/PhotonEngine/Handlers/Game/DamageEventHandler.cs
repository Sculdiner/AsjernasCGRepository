using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class DamageEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DamageEventModel
{
    public DamageEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.DealDamage;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var board = view as BoardView;
        var card = board.BoardManager.GetCard(model.CardId);
        if (card == null)
            return;
        
        card.TakeDamage(model.Value);
    }   
}
