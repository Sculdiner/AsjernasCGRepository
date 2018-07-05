using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common.BusinessModels.CardModels;

public class PlayCardWithoutTargetEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public PlayCardWithoutTargetEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.CallbackSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayCardWithoutTarget;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardView = (view as BoardView);
        var card = boardView.BoardManager.GetCard(model.GeneratedCardId);
        if (card == null)
        {
            var cardPrefab = boardView.MasterCardManager.GenerateCardPrefab(model.CardTemplateId, model.GeneratedCardId);
            card = boardView.BoardManager.RegisterPlayerCard(cardPrefab, cardPrefab.GetComponent<CardManager>().Template, CardLocation.PlayArea, model.OwnerId);
        }
        if (card.CardStats.CardType == CardType.Follower)
        {
            boardView.HandSlotManagerV2.RemoveCard(model.GeneratedCardId);
            boardView.BoardManager.GetPlayerStateById(model.OwnerId)?.AllySlotManager.AddAllyCardLast(card);
        }
    }
}
