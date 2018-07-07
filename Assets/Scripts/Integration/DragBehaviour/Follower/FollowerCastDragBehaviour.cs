using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//FollowerCastDragBehaviour
//FollowerBoardDragBehaviour
public class FollowerCastDragBehaviour : BaseDragCardBehaviour
{
    public int Layer => LayerMask.GetMask("AllyPlayArea");
    public GameObject TargetedArea { get; private set; }
    private AllySlotManager PlacementTarget;

    public FollowerCastDragBehaviour(ClientSideCard card) : base(card)
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
        var handHelper = BoardView.Instance.HandSlotManagerV2;
        BoardManager.Instance.ActiveCard = null;

        if (PlacementTarget != null && PlacementTarget.OwningPlayer.UserId == BoardManager.Instance.GetCurrentUserPlayerState().UserId)
        {
            ReferencedCard.CardManager.VisualStateManager.DissolvePlayParticleSystem.Play(true);
            if (BoardView.Instance.IsArtistDebug)
            {
                handHelper.RemoveCard(ReferencedCard.CardStats.GeneratedCardId);
                //operation and dissolve.
                //boardmanager to validate play
                PlacementTarget.AddAllyCardLast(ReferencedCard);
            }
            else
            {
                ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);
                (BoardView.Instance.Controller as BoardController).Play_CardWithoutTarget(ReferencedCard.CardStats.GeneratedCardId);
            }
        }
        else
        {
            ReferencedCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();
            ReferencedCard.KillTweens();
            handHelper.RefreshHandPositions(Ease.Linear, .35f);
            //ReferencedCard.CardViewObject.transform.DOMove(handHelper.handPosition, 0.35f).OnComplete(() =>
            //{
            //    ReferencedCard.CardViewObject.transform.rotation = handHelper.handRotation;
            //});
        }
    }

    public override void OnStartDrag()
    {
        ReferencedCard.HoverComponent.ForceKillHover();

        base.OnStartDrag();

        ReferencedCard.IsDragging = true;
        ReferencedCard.IsHovering = false;
        BoardManager.Instance.ActiveCard = ReferencedCard;

        ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);


        //ReferencedCard.CardViewObject.transform.position = handHelper.handPosition;
        //ReferencedCard.CardViewObject.transform.rotation = handHelper.handRotation;
    }

    public override bool DragSuccessful()
    {
        //what are thoooooooose
        return true;
    }

    public override void KillCurrentActions()
    {

    }

    public override bool AllowInSetup()
    {
        return true;
    }
}

