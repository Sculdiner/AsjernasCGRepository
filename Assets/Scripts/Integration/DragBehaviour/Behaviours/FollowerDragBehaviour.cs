using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//FollowerCastDragBehaviour
//FollowerBoardDragBehaviour
public class FollowerDragBehaviour : BaseDragCardBehaviour
{
    public int Layer => LayerMask.GetMask("AllyPlayArea");
    public GameObject TargetedArea { get; private set; }
    private AllySlotManager PlacementTarget;

    public FollowerDragBehaviour(ClientSideCard card) : base(card)
    {
      
    }

    public override void OnDraggingInUpdate()
    {
        base.OnDraggingInUpdate();
        //Debug.DrawLine(transform.position, t, Color.green);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitObject;
        //var hit = Physics.RaycastAll(ray, out hitObject, 30f, Layer);
        var hits = Physics.RaycastAll(ray, 30f, Layer);
        if (TargetedArea != null)
        {
            Debug.DrawLine(ray.origin, TargetedArea.transform.position, Color.magenta);
        }

        //getcomponent ally play area to find the play area manager and it's owner
        if (hits.Length == 1)
        {
            var hitLayer = hits[0];
            var slotComponent = hitLayer.transform.gameObject.GetComponent<AllySlotManager>();
            if (slotComponent != null)
            {
                if (slotComponent.IsCurrentPlayerArea())
                {
                    PlacementTarget = slotComponent;
                    return;
                }
            }
        }
        PlacementTarget = null;

    }

    public override void OnEndDrag()
    {
        base.OnEndDrag();
        ReferencedCard.IsDragging = false;
        var handHelper = ReferencedCard.CardManager.CardHandHelperComponent;
        if (handHelper.HandSlotManager.ActiveCard != null)
            handHelper.HandSlotManager.ActiveCard = null;

        if (PlacementTarget != null)
        {
            ReferencedCard.CardManager.CardHandHelperComponent.HandSlotManager.RemoveCard(ReferencedCard.CardStats.GeneratedCardId);
            //operation and dissolve.
            //boardmanager to validate play
            PlacementTarget.AddAllyCardLast(ReferencedCard);
        }
        else
        {
            ReferencedCard.CardViewObject.GetComponent<DragRotator>().enabled = false;
            ReferencedCard.KillTweens();
            ReferencedCard.CardViewObject.transform.DOMove(handHelper.handPosition, 0.35f).OnComplete(() =>
            {
                ReferencedCard.CardViewObject.transform.rotation = handHelper.handRotation;
            });
        }
    }

    public override void OnStartDrag()
    {
        base.OnStartDrag();
        var handHelper = ReferencedCard.CardManager.CardHandHelperComponent;

        ReferencedCard.IsDragging = true;
        ReferencedCard.IsHovering = false;
        ReferencedCard.CardViewObject.GetComponent<DragRotator>().enabled = true;
        handHelper.HandSlotManager.ActiveCard = ReferencedCard;

        ReferencedCard.KillTweens();

        ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
        
        ReferencedCard.CardViewObject.transform.position = handHelper.handPosition;
        ReferencedCard.CardViewObject.transform.rotation = handHelper.handRotation;
    }

    public override bool DragSuccessful()
    {
        //what are thoooooooose
        return true;
    }

    public override void KillCurrentActions()
    {

    }
}

