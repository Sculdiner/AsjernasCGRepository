using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCastDragBehaviour : BaseTargetingCardBehaviour
{
    protected override int Layer { get; }

    private Func<ClientSideCard, List<ClientSideCard>> targetValidationMethod;
    public AbilityCastDragBehaviour(ClientSideCard card) : base(card)
    {
        Layer = LayerMask.GetMask("RaycastEligibleTargets");
        targetValidationMethod = BoardManager.Instance.FindValidAbOrEquipmentTargetsOnBoard;
    }


    public override bool AllowInSetup()
    {
        return true;
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

    public override void OnNonSuccessfullTargetAcquisition()
    {
        var handHelper = BoardView.Instance.HandSlotManagerV2;
        BoardManager.Instance.ActiveCard = null;

        ReferencedCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();
        ReferencedCard.KillTweens();
        handHelper.RefreshHandPositions(Ease.Linear, .35f);
    }

    public override void OnSuccessfullTargetAcquisition(CardManager acquiredTarget)
    {
        var boardView = BoardView.Instance;
        var controller = boardView.Controller as BoardController;
        if (boardView.IsArtistDebug)
        {
            acquiredTarget.CharacterManager.CharacterAbilityManager.AddAbility(ReferencedCard);
        }
        else
        {
            controller.Play_AttachmentCardWithoutTarget(ReferencedCard.CardStats.GeneratedCardId, acquiredTarget.Template.GeneratedCardId);
        }
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

    public override bool CheckResourceOnStart()
    {
        return true;
    }
}