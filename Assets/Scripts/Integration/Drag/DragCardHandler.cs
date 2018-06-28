using Assets.Scripts.Card;
using System.Collections;
using System.Collections.Generic;
using AsjernasCG.Common.BusinessModels.CardModels;
using UnityEngine;

public class DragCardHandler : DraggingActions
{
    private BoardManager BoardManager;
    private CardManager CardManager;

    public DragCardHandler(ClientSideCard card) : base(card)
    {

    }

    // Use this for initialization
    void Start()
    {
        //BoardManager = GetComponent<BoardManager>();
        //CardManager = GetComponent<CardManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Called in OnMouseDown of Draggable
    //glow targets etc depending on card type and targetting types
    public override void OnStartDrag()
    {
        var card = BoardManager.GetCard(CardManager.InitialTemplate.GeneratedCardId);

        //*****this should be in BoardManager returning valid targets if any depending on CardType Dragging*****
        switch (card.CardStats.CardType)
        {
            case CardType.Equipment:
                //glow Characters on Board and Equipment slots on board or something
                break;
            case CardType.Ability:
                //glow Characters on Board and Ability slots on board or something
                break;
            case CardType.Event:
                var targets = BoardManager.FindValidTargetsOnBoard(card);
                break;
            default:
                break;
        }
        //******************************************************************************************************

    }

    //called in OnMouseDrag of Draggable
    public override void OnDraggingInUpdate()
    {
        //here the target gizmo is shown and handled
    }

    //called in OnMouseUp in Draggable
    public override void OnEndDrag()
    {
        if (DragSuccessful())
        {
            //boardmanager depending on card type evaluate action
            //operation depending on current target/play area etc.
        }
        else
        {
            //bounce back to hand/ cancel attack etc.
        }
    }

    //check for drag area or invalid target of targetted spell and return truefalse to OnEndDrag
    public override bool DragSuccessful()
    {
        //dummy
        return true;
    }

    public override void KillCurrentActions()
    {
    }
}
