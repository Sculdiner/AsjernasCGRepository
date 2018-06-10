using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardElementValueChangeEventHandler<TModel> : BaseEventHandler<TModel> where TModel : CardElementValueChange
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.ChangeCardElement;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
