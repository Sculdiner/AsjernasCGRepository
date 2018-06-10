using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
public class PlayCardWithTargetEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayCardWithTarget;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
