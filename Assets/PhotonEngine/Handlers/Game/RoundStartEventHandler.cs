using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;

public class RoundStartEventHandler<TModel> : BaseEventHandler<TModel> where TModel : RoundStartModel
{
    public RoundStartEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.RoundStart;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardview = view as BoardView;
        boardview.InitiativeManager.ClearAllHighlights();
        //var text = PhotonEngine.UserId == model.ActiveUser ? "Your setup turn" : "Teammate's setup turn";
        //boardview.TurnMessenger.Show(text);
    }
}
