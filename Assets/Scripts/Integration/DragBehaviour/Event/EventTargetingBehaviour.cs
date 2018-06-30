using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EventTargetingBehaviour : BaseTargetingCardBehaviour
{
    public EventTargetingBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override bool DragSuccessful()
    {
        return base.DragSuccessful();
    }

    public override void KillCurrentActions()
    {
        base.KillCurrentActions();
    }

    public override void OnDraggingInUpdate()
    {
        base.OnDraggingInUpdate();
    }

    public override void OnEndDrag()
    {
        base.OnEndDrag();
        ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
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
}

