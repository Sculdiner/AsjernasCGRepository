using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//FollowerCastDragBehaviour
//FollowerBoardDragBehaviour
public class FollowerTargetingBehaviour : BaseTargetingCardBehaviour
{
    protected override int Layer { get; }
    private Func<ClientSideCard, List<ClientSideCard>> targetValidationMethod;
    public FollowerTargetingBehaviour(ClientSideCard card) : base(card)
    {
        Layer = LayerMask.GetMask("RaycastEligibleTargets");
        targetValidationMethod = BoardManager.Instance.FindValidAttackTargetsOnBoard;
    }

    public override void OnAcquiredNewTarget(CardManager newTarget)
    {
        Cursor.SetCursor(BoardView.Instance.AttackCursor, Vector2.zero, CursorMode.Auto);
        TargetedCard.OnStartBeingTargetedForAttack(ReferencedCard);
    }
    public override void OnLoseTarget()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        TargetedCard.OnStopBeingTargetedForAttack(ReferencedCard);
    }

    public override void OnSuccessfullTargetAcquisition(CardManager acquiredTarget)
    {
        if (BoardView.Instance.IsArtistDebug)
        {
            var seq = DOTween.Sequence();
            seq.Append(ReferencedCard.CardViewObject.transform.DOMove(acquiredTarget.transform.position, 0.3f)); //go in
            seq.Append(ReferencedCard.CardViewObject.transform.DOMove(PreDragPosition.Value, 1.5f).SetEase(Ease.InOutQuint)); //return
            seq.OnComplete(() => { ReferencedCard.CardViewObject.GetComponent<DragRotator>().DisableRotator(); }); //disable the rotator when you arrive
        }
        else
        {
            ReferencedCard.LastPosition = PreDragPosition.Value;
            (BoardView.Instance.Controller as BoardController).Attack(ReferencedCard.CardStats.GeneratedCardId, acquiredTarget.Template.GeneratedCardId);
        }
        
    }

    public override void OnNonSuccessfullTargetAcquisition()
    {
        ReferencedCard.CardViewObject.transform.DOMove(PreDragPosition.Value, 0.3f); //return
        ReferencedCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();//disable the rotator
    }

    public override void OnStartDrag()
    {
        base.OnStartDrag();
        ReferencedCard.CardViewObject.transform.DOMove(PreDragPosition.Value + new Vector3(0, 1f, 0), 0.2f);
    }

    public override bool DragSuccessful()
    {
        return true;
    }

    public override Func<ClientSideCard, List<ClientSideCard>> GetTargetValidationMethod()
    {
        return targetValidationMethod;
    }

    public override bool AllowInSetup()
    {
        return false;
    }

    public override bool CheckResourceOnStart()
    {
        return true;
    }
}
