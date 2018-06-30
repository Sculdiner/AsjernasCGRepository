using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseTargetingCardBehaviour : DraggingActions
{
    private LineRenderer TargetingGizmo;
    private Vector3 screenSpace;
    public BaseTargetingCardBehaviour(ClientSideCard card) : base(card)
    {
        TargetingGizmo = card.CardManager.TargetingGizmo;
    }

    public override void OnStartDrag()
    {
        TargetingGizmo.enabled = true;
        screenSpace = Camera.main.WorldToScreenPoint(ReferencedCard.CardViewObject.transform.position);
        TargetingGizmo.SetPosition(0, ReferencedCard.CardViewObject.transform.position);
        TargetingGizmo.SetPosition(1, ReferencedCard.CardViewObject.transform.position);
    }

    public override void OnDraggingInUpdate()
    {
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        TargetingGizmo.SetPosition(1, Camera.main.ScreenToWorldPoint(curScreenSpace));
    }

    public override void OnEndDrag()
    {
        TargetingGizmo.enabled = false;
    }

    public override bool DragSuccessful()
    {
        return true;
    }

    public override void KillCurrentActions()
    {
    }
}

