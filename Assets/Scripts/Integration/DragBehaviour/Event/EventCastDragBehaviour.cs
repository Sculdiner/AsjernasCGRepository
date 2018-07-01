using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EventCastDragBehaviour : BaseDragCardBehaviour
{
    public EventCastDragBehaviour(ClientSideCard card) : base(card)
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
    }

    public override void OnStartDrag()
    {
        base.OnStartDrag();
    }
}

