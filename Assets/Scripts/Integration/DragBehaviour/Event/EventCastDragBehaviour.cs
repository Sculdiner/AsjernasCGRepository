using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventCastDragBehaviour : BaseDragCardBehaviour
{
    public int Layer => LayerMask.GetMask("BoardEventPlayArea");
    private bool eventPlacementValidated = false;
    public EventCastDragBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override bool AllowInSetup()
    {
        return true;
    }

    public override bool CheckResourceOnStart()
    {
        return true;
    }

    public override bool DragSuccessful()
    {
        return base.DragSuccessful();
    }

    public override void OnCancelAction()
    {
        var handHelper = BoardView.Instance.HandSlotManagerV2;
        BoardManager.Instance.ActiveCard = null;

        ReferencedCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();
        ReferencedCard.KillTweens();
        handHelper.RefreshHandPositions(Ease.Linear, .35f);
    }

    public override void OnDraggingInUpdate()
    {
        base.OnDraggingInUpdate();
        //Debug.DrawLine(transform.position, t, Color.green);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitObject;
        //var hit = Physics.RaycastAll(ray, out hitObject, 30f, Layer);
        var hits = Physics.RaycastAll(ray, 30f, Layer);

        //getcomponent ally play area to find the play area manager and it's owner
        if (hits.Length == 1)
        {
            eventPlacementValidated = true;
        }
        else
        {
            eventPlacementValidated = false;
        }
    }

    public override void OnEndDrag()
    {
        base.OnEndDrag();
        ReferencedCard.IsDragging = false;
        var handHelper = BoardView.Instance.HandSlotManagerV2;
        BoardManager.Instance.ActiveCard = null;

        if (eventPlacementValidated)
        {
            eventPlacementValidated = false;
            ReferencedCard.CardManager.VisualStateManager.DissolvePlayParticleSystem.Play(true);
            if (BoardView.Instance.IsArtistDebug)
            {

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
    }
}

