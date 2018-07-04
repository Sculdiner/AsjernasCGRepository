using AsjernasCG.Common.BusinessModels.CardModels;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HandHoverBehaviour : HoverActions
{
    public HandHoverBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override void OnHoverEnd()
    {
        if (Card.IsHovering)
        {
            Card.KillTweens();

            Card.CardManager.VisualStateManager.CurrentState.gameObject.transform.DOMove(PreHoverPosition.Value, 0.15f).SetEase(Ease.OutQuad, 0.5f, 0).OnComplete(() =>
            {
                if (Card.CurrentLocation == CardLocation.Hand && Card.IsHovering)
                {
                    Card.IsHovering = false;
                    Card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
                    Card.CardViewObject.transform.position = PreHoverPosition.Value;
                    Card.CardViewObject.transform.rotation = PreHoverRotation.Value;
                }
            });
            Card.CardViewObject.transform.rotation = PreHoverRotation.Value;
        }
    }

    public override void OnHoverStart()
    {
        if (BoardManager.Instance.ActiveCard != null)
            return;

        if (!Card.IsHovering)
        {
            Card.IsHovering = true;

            Card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Preview);
            Card.CardManager.VisualStateManager.CurrentState.gameObject.transform.position = HoverPosition.Value;
            AnimationOnEnd();
        }
    }

    //Changes visual to card
    public override void OnImmediateKill()
    {
        Card.IsHovering = false;
        Card.IsDragging = false;
        Card.CardViewObject.GetComponent<DragRotator>().DisableRotator();
        Card.KillTweens();
        if (Card.CurrentLocation == CardLocation.Hand)
        {
            Card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
        }
        //Card.CardViewObject.transform.position = PreHoverPosition.Value;
        //Card.CardViewObject.transform.rotation = PreHoverRotation.Value;
    }

    private void AnimationOnEnd()
    {
        Card.DoTweenTweening = null;
        var sequance = DOTween.Sequence();
        Card.DoTweenSequence = sequance;
        sequance.Append(Card.CardManager.VisualStateManager.CurrentState.gameObject.transform.DOMove(HoverPosition.Value + new Vector3(0, 0, 0.025f), 1f));// SetEase(Ease.OutCirc, 0.5f, 0);
        sequance.Append(Card.CardManager.VisualStateManager.CurrentState.gameObject.transform.DOMove(HoverPosition.Value + new Vector3(0, 0, 0.05f), 1f));
        sequance.Append(Card.CardManager.VisualStateManager.CurrentState.gameObject.transform.DOMove(HoverPosition.Value - new Vector3(0, 0, 0.03f), 4f));//.SetEase(Ease.InCubic, 0.5f, 0);
        sequance.OnComplete(() => { Card.DoTweenSequence = null; });
    }
}
