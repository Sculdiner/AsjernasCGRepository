using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common.OperationModels.BasicModels;

public class GameStartingEventHandler<TModel> : BaseEventHandler<TModel> where TModel : StringModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.GameStarting;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}