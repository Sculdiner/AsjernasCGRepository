using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardElementValueBatchChangeEventHandler<TModel> : BaseEventHandler<TModel> where TModel : List<CardElementValueChange>
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.ChangeCardElementsBatch;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
