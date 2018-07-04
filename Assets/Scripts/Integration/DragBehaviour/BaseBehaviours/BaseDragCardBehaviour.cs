using Assets.Scripts.Card;
using System.Collections;
using System.Collections.Generic;
using AsjernasCG.Common.BusinessModels.CardModels;
using UnityEngine;

public abstract class BaseDragCardBehaviour : DraggingActions
{
    public ClientSideCard ControllingCard { get; set; }
    private GameObject Card { get; set; }
    private Vector3 screenSpace;
    //private Vector3 offset;

    public BaseDragCardBehaviour(ClientSideCard card) : base(card)
    {
        ControllingCard = card;
        Card = card.CardViewObject;
    }

    //Called in OnMouseDown of Draggable
    //glow targets etc depending on card type and targetting types
    public override void OnStartDrag()
    {
        ControllingCard.CardViewObject.GetComponent<DragRotator>().enabled = true;
        ControllingCard.KillTweens();
        ControllingCard.IsUnderPlayerControl = true;
        //translate the cubes position from the world to Screen Point
        screenSpace = Camera.main.WorldToScreenPoint(Card.transform.position);
        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        //offset = Card.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

    }

    //called in OnMouseDrag of Draggable
    public override void OnDraggingInUpdate()
    {
        //keep track of the mouse position
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);

        //update the position of the object in the world
        Card.transform.position = curPosition;
        Debug.Log($"OnMouseDrag: {ControllingCard.CardStats.GeneratedCardId}");
    }

    //called in OnMouseUp in Draggable
    public override void OnEndDrag()
    {
        ControllingCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();
        ControllingCard.IsUnderPlayerControl = false;
        if (DragSuccessful())
        {
            //operation depending on current target/play area etc.
        }
        else
        {
        }
    }

    public override bool DragSuccessful()
    {
        //dummy
        return true;
    }

    public override void KillCurrentActions()
    {
    }
}
