using AsjernasCG.Common.BusinessModels.BasicModels;
using AsjernasCG.Common.ClientEventCodes;

public class EncounterStartEventHandler<TModel> : BaseEventHandler<TModel> where TModel : EmptyModel
{
    public EncounterStartEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.EncounterStart;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardview = view as BoardView;
        boardview.InitiativeManager.gameObject.SetActive(true);
    }
}