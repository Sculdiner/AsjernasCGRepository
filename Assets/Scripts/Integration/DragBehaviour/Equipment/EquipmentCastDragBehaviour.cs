using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EquipmentCastDragBehaviour : BaseTargetingCardBehaviour
{
    protected override int Layer { get; }
    private Func<ClientSideCard, List<ClientSideCard>> targetValidationMethod;
    public EquipmentCastDragBehaviour(ClientSideCard card) : base(card)
    {
        Layer = LayerMask.GetMask("RaycastEligibleTargets");
        targetValidationMethod = BoardManager.Instance.FindValidTargetsOnBoard;
    }
   
    public override Func<ClientSideCard, List<ClientSideCard>> GetTargetValidationMethod()
    {
        return targetValidationMethod;
    }

    public override void OnAcquiredNewTarget(CardManager target)
    {
        
    }
    public override void OnLoseTarget()
    {
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
        acquiredTarget.CharacterManager.CharacterEquipmentManager.AddEquipment(ReferencedCard);
    }

    public override void OnNonSuccessfullTargetAcquisition()
    {
        var handHelper = BoardView.Instance.HandSlotManagerV2;
        BoardManager.Instance.ActiveCard = null;

        ReferencedCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();
        ReferencedCard.KillTweens();
        handHelper.RefreshHandPositions(Ease.Linear, .35f);
    }
}