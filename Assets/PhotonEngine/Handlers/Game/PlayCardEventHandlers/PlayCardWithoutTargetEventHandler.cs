using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common.BusinessModels.CardModels;

public class PlayCardWithoutTargetEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayCardWithoutTarget;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var card = BoardManager.Instance.GetCard(model.GeneratedCardId);
        if (card.CardStats.CardType == CardType.Follower)
        {

        }
    }
}
