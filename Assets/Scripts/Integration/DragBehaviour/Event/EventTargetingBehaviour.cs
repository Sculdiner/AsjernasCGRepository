using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventTargetingBehaviour : BaseTargetingCardBehaviour
{
    protected override int Layer { get; }
    private Func<ClientSideCard, List<ClientSideCard>> targetValidationMethod;
    public EventTargetingBehaviour(ClientSideCard card) : base(card)
    {
        Layer = LayerMask.GetMask("RaycastEligibleTargets");
        targetValidationMethod = BoardManager.Instance.FindValidTargetsOnBoard;
    }

    public override bool DragSuccessful()
    {
        return base.DragSuccessful();
    }


    public override void OnAcquiredNewTarget(CardManager target)
    {
        TargetedCard.OnStartBeingTargetedForAttack(ReferencedCard);
    }

    public override void OnLoseTarget()
    {
        TargetedCard.OnStopBeingTargetedForAttack(ReferencedCard);
    }

    public override void OnStartDrag()
    {
        base.OnStartDrag();
        ReferencedCard.IsDragging = true;
        ReferencedCard.IsHovering = false;
        BoardManager.Instance.ActiveCard = ReferencedCard;
        ReferencedCard.HoverComponent.ForceKillHover();
        ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);
    }

    public override void OnSuccessfullTargetAcquisition(CardManager acquiredTarget)
    {
        var boardManager = BoardManager.Instance;
        var boardView = BoardView.Instance;
        if (boardView.IsArtistDebug)
        {
            //ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
            var targetTransform = GameObject.Find("OpponentPlayedCardold").transform;

            var sequence = DOTween.Sequence();
            ReferencedCard.CardManager.VisualStateManager.DeactivatePreview();
            ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
            sequence.Insert(0, ReferencedCard.CardViewObject.transform.DOMove(targetTransform.position, 1f));
            sequence.Insert(0, ReferencedCard.CardViewObject.transform.DORotate(targetTransform.rotation.eulerAngles, 1f));
            sequence.Insert(0, ReferencedCard.CardViewObject.transform.DOScale(targetTransform.localScale, 1f));
            sequence.Insert(1, ReferencedCard.CardViewObject.transform.DOScale(targetTransform.localScale, 0.6f));
            sequence.Insert(1.6f, ReferencedCard.CardViewObject.transform.DOScale(0f, 1f));
            sequence.InsertCallback(2.6f, () =>
            {
                ReferencedCard.CardManager.SlotManager?.RemoveSlot(ReferencedCard.CardStats.GeneratedCardId);
                ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);

            });
        }
        else
        {
            (boardView.Controller as BoardController).Play_CardWithTarget(ReferencedCard.CardStats.GeneratedCardId, acquiredTarget.Template.GeneratedCardId);
        }
    }

    public override void OnNonSuccessfullTargetAcquisition()
    {
        ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
    }

    public override Func<ClientSideCard, List<ClientSideCard>> GetTargetValidationMethod()
    {
        return targetValidationMethod;
    }

    public override bool AllowInSetup()
    {
        return true;
    }
}

