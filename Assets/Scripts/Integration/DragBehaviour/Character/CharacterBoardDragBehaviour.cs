using AsjernasCG.Common.BusinessModels.CardModels;
using DG.Tweening;
using HighlightingSystem;
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
        targetValidationMethod = BoardManager.Instance.FindValidAttackOrQuestTargetsOnBoard;
        //targetValidationMethod += BoardManager.Instance.FindValidQuestingTargetsOnBoard;
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
        if (target.Template.CardType == AsjernasCG.Common.BusinessModels.CardModels.CardType.Minion)
        {
            Cursor.SetCursor(BoardView.Instance.AttackCursor, Vector2.zero, CursorMode.Auto);
            TargetedCard.OnStartBeingTargetedForAttack(ReferencedCard);
        }
        else if (target.Template.CardType == AsjernasCG.Common.BusinessModels.CardModels.CardType.Quest)
        {
            Cursor.SetCursor(BoardView.Instance.QuestCursor, Vector2.zero, CursorMode.Auto);
            target.GetComponent<Highlighter>().constant = true;
        }

    }

    public override void OnLoseTarget()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        if (TargetedCard.Template.CardType == CardType.Minion)
        {
            TargetedCard.OnStopBeingTargetedForAttack(ReferencedCard);
        }
        else if (TargetedCard.Template.CardType == CardType.Quest)
        {
            TargetedCard.GetComponent<Highlighter>().constant = false;
        }
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
            if (TargetedCard.Template.CardType == CardType.Minion)
            {
                ReferencedCard.LastPosition = PreDragPosition.Value;
                (BoardView.Instance.Controller as BoardController).Attack(ReferencedCard.CardStats.GeneratedCardId, acquiredTarget.Template.GeneratedCardId);
            }
            else if (TargetedCard.Template.CardType == CardType.Quest)
            {
                ReferencedCard.LastPosition = PreDragPosition.Value;
                (BoardView.Instance.Controller as BoardController).Quest(ReferencedCard.CardStats.GeneratedCardId);
            }
        }
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

    public override bool AllowInSetup()
    {
        return false;
    }
}
