using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BaseTargetingCardBehaviour : DraggingActions
{
    public BaseTargetingCardBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override bool DragSuccessful()
    {
        return true;
    }

    public override void KillCurrentActions()
    {

    }

    public override void OnDraggingInUpdate()
    {

    }

    public override void OnEndDrag()
    {

    }

    public override void OnStartDrag()
    {

    }
}

