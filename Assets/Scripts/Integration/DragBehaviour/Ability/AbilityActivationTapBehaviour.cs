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
        
    }

    public override void OnForceCancelAction()
    {
        
    }

    public override void OnStartDrag()
    {
        
    }
}