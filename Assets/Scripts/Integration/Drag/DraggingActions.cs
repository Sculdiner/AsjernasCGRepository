﻿using UnityEngine;
using System.Collections;

public abstract class DraggingActions
{
    public ClientSideCard ReferencedCard { get; private set; }
    public DraggingActions(ClientSideCard card)
    {
        ReferencedCard = card;
    }

    public Vector3? PreDragPosition { get; set; }

    public abstract void OnStartDrag();

    public abstract void OnEndDrag();

    public abstract void OnDraggingInUpdate();

    public abstract bool DragSuccessful();

    public abstract void KillCurrentActions();

}
