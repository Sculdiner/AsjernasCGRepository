using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common.BusinessModels.CardModels;

public class PlayAttachmentCardWithoutTargetEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public PlayAttachmentCardWithoutTargetEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.CallbackSync;
    }
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayAttachmentCardWithoutTarget;
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
        if (card.CardStats.CardType == CardType.Equipment)
        {
            var cardToAttachTo = boardView.BoardManager.GetCard(model.AttachOn.Value);
            cardToAttachTo.CardManager.CharacterManager.CharacterEquipmentManager.AddEquipment(card);
            //boardView.HandSlotManagerV2.RemoveCard(model.GeneratedCardId);
        }
        else if (card.CardStats.CardType == CardType.Ability)
        {
            var cardToAttachTo = boardView.BoardManager.GetCard(model.AttachOn.Value);
            cardToAttachTo.CardManager.CharacterManager.CharacterAbilityManager.AddAbility(card);
        }
    }
}
