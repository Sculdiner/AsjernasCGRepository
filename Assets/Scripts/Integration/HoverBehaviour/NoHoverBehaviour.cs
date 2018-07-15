using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class NoHoverBehaviour : HoverActions
{
    public NoHoverBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override void OnHoverEnd()
    {
        
    }

    public override void OnHoverStart()
    {
        
    }

    public override void OnImmediateKill()
    {
        
    }
}
