using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
public class GameCancelationEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GameCancelationModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.CancelGame;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}