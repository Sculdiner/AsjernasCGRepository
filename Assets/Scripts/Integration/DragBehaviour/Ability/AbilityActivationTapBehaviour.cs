using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivationTapBehaviour : DraggingActions
{
    public AbilityActivationTapBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override bool AllowInSetup()
    {
        return true;
    }

    public override bool CheckResourceOnStart()
    {
        return false;
    }

    public override bool DragSuccessful()
    {
        return true;
    }

    public override void OnDraggingInUpdate()
    {

    }

    public override void OnEndDrag()
    {
        (BoardView.Instance.Controller as BoardController).ActivateAbilityWithoutTarget(ReferencedCard.CardStats.GeneratedCardId);
    }

    public override bool CustomValidationOnStartDrag()
    {
        if (!base.CustomValidationOnStartDrag())
            return false;

        if (ReferencedCard.CardStats.RemainingCooldown > 0)
            return false;
        return true;
    }

    public override void OnForceCancelAction()
    {

    }

    public override void OnStartDrag()
    {

    }
}