using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.ClientEventCodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventHandler<TModel> : BaseEventHandler<TModel> where TModel : AttackModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.Attack;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        throw new System.NotImplementedException();
    }
}
