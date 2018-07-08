using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.OperationModels.BasicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RemoveCardEventHelperEventHandler<TModel> : BaseEventHandler<TModel> where TModel : IntegerModel
{
    public RemoveCardEventHelperEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.SerialSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.RemoveCardFromBoard;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardView = view as BoardView;
        boardView.BoardManager.RemoveCardFromPlay(model.Value);
        boardView.InitiativeManager.RemoveSlot(model.Value);
    }
}