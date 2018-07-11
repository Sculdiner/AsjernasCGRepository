using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common.BusinessModels.CardModels;

public class CardPlayCancelationEventHandler<TModel> : BaseEventHandler<TModel> where TModel : CardPlayCancelationReasonModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayCardFromHandCancelation;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        
    }
}