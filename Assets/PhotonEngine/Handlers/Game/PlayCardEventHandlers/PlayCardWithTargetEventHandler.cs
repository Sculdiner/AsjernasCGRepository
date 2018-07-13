using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common.BusinessModels.CardModels;
using DG.Tweening;

public class PlayCardWithTargetEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public PlayCardWithTargetEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.CallbackSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.PlayCardWithTarget;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var boardView = (view as BoardView);
        var card = boardView.BoardManager.GetCard(model.GeneratedCardId);
        if (card == null)
        {
            boardView.BoardManager.DisplayTeammateCardPlayEffect(model.CardTemplateId, model.GeneratedCardId, model.OwnerId, () =>
            {
                PhotonEngine.CompletedAction();
            });
            //var cardPrefab = boardView.MasterCardManager.GenerateCardPrefab(model.CardTemplateId, model.GeneratedCardId);
            //card = boardView.BoardManager.RegisterPlayerCard(cardPrefab, cardPrefab.GetComponent<CardManager>().Template, CardLocation.DiscardPile, model.OwnerId);

            //var targetTransform = GameObject.Find("OpponentPlayedCardold").transform;

            //var sequence = DOTween.Sequence();
            //card.CardManager.VisualStateManager.DeactivatePreview();
            //card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
            //sequence.Insert(0, card.CardViewObject.transform.DOMove(targetTransform.position, 1f));
            //sequence.Insert(0, card.CardViewObject.transform.DORotate(targetTransform.rotation.eulerAngles, 1f));
            //sequence.Insert(0, card.CardViewObject.transform.DOScale(targetTransform.localScale, 1f));
            //sequence.Insert(1, card.CardViewObject.transform.DOScale(targetTransform.localScale, 0.6f));
            //sequence.InsertCallback(1.6f, () =>
            //{
            //    card.CardManager.SlotManager?.RemoveSlot(card.CardStats.GeneratedCardId);
            //});
            //sequence.Insert(1.6f, card.CardViewObject.transform.DOScale(0f, 1f));
            //sequence.InsertCallback(2.6f, () =>
            //{
            //    card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);
            //    PhotonEngine.CompletedAction();
            //});

        }
        else
        {
            //play card effects
            card.CardManager.SlotManager?.RemoveSlot(card.CardStats.GeneratedCardId);
            card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);
            PhotonEngine.CompletedAction();
        }

        //play any effects
    }
}
