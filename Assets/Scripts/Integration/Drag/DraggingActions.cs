using UnityEngine;
using System.Collections;

public abstract class DraggingActions
{
    public ClientSideCard ReferencedCard { get; private set; }
    public DraggingActions(ClientSideCard card)
    {
        ReferencedCard = card;
    }

    public abstract void OnStartDrag();

    public abstract void OnEndDrag();

    public abstract void OnDraggingInUpdate();

    protected abstract bool DragSuccessful();
    
    public void KillCurrentActions() //abstract
    {

    }
}
