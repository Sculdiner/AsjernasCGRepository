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

    public override void KillCurrentActions()
    {
        base.KillCurrentActions();
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
        ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
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

