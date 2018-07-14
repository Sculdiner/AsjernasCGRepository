using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class NoDragBehaviour : DraggingActions
{
    public NoDragBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override bool AllowInSetup()
    {
        return false;
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
