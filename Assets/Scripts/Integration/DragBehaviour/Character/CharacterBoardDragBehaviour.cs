using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterBoardDragBehaviour : BaseTargetingCardBehaviour
{
    protected override int Layer { get; }
    private Func<ClientSideCard, List<ClientSideCard>> targetValidationMethod;
    public CharacterBoardDragBehaviour(ClientSideCard card) : base(card)
    {
        Layer = LayerMask.GetMask("RaycastEligibleTargets");
        targetValidationMethod = BoardManager.Instance.FindValidAttackTargetsOnBoard;
    }


    public override void OnStartDrag()
    {
        base.OnStartDrag();
        ReferencedCard.CardViewObject.transform.DOMove(PreDragPosition.Value + new Vector3(0, 1f, 0), 0.2f);
    }

    public override void OnEndDrag()
    {
        base.OnEndDrag();
    }

    public override void OnAcquiredNewTarget(CardManager target)
    {
        TargetedCard.OnStartBeingTargetedForAttack(ReferencedCard);
    }

    public override void OnLoseTarget()
    {
        TargetedCard.OnStopBeingTargetedForAttack(ReferencedCard);
    }

    public override void OnSuccessfullTargetAcquisition(CardManager acquiredTarget)
    {
        var seq = DOTween.Sequence();
        seq.Append(ReferencedCard.CardViewObject.transform.DOMove(acquiredTarget.transform.position, 0.3f)); //go in
        seq.Append(ReferencedCard.CardViewObject.transform.DOMove(PreDragPosition.Value, 1.5f).SetEase(Ease.InOutQuint)); //return
        seq.OnComplete(() => { ReferencedCard.CardViewObject.GetComponent<DragRotator>().DisableRotator(); }); //disable the rotator when you arrive
    }

    public override void OnNonSuccessfullTargetAcquisition()
    {
        ReferencedCard.CardViewObject.transform.DOMove(PreDragPosition.Value, 0.3f); //return
        ReferencedCard.CardViewObject.GetComponent<DragRotator>().DisableRotator();//disable the rotator
    }

    public override Func<ClientSideCard, List<ClientSideCard>> GetTargetValidationMethod()
    {
        return targetValidationMethod;
    }
}
