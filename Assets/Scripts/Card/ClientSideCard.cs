using AsjernasCG.Common.BusinessModels.CardModels;
using Assets.Scripts.Card;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ClientSideCard
{
    public GameObject CardViewObject { get; set; }
    public CardManager CardManager { get; set; }
    public ClientSideCardEvents Events { get; set; }
    public ClientCardTemplate CardStats { get; set; }
    public ParticipatorState ParticipatorState { get; set; }
    public CardLocation CurrentLocation { get; private set; }
    public bool IsUnderPlayerControl { get; set; }
    public bool IsHovering { get; set; }
    public bool IsDragging { get; set; }
    public Vector3? LastPosition { get; set; }
    public Hoverable HoverComponent { get; set; }
    /// <summary>
    /// IMPORTANT: the following forces the card to CardVisual if the location is set to hand
    /// </summary>
    /// <param name="location"></param>
    public void SetLocation(CardLocation location)
    {
        CurrentLocation = location;

        if (HoverComponent == null)
        {
            HoverComponent = CardViewObject.gameObject.AddComponent<Hoverable>();
            HoverComponent.ControllingCard = this;
        }
        //warning: the following forces the card to CardVisual if the location is Hand
        HoverComponent.ForceKillHover();

        if (location == CardLocation.Hand)
        {
            HoverComponent.SetAction<HandHoverBehaviour>();
            if (ParticipatorState is PlayerState && CardStats.BaseResourceCost <= (ParticipatorState as PlayerState).Resources)
            {
                this.CardManager.VisualStateManager.Highlight(HighlightType.AvailableToPlay);
            }
        }
        else if (this.CardStats.CardType == CardType.Quest)
            HoverComponent.SetAction<QuestHoverBehaviour>();
        else if (location == CardLocation.PlayArea)
            HoverComponent.SetAction<BoardHoverBehaviour>();
        else if (location == CardLocation.DiscardPile)
            HoverComponent.SetAction<NoHoverBehaviour>();
    }
    public Sequence DoTweenSequence { get; set; }
    public Tween DoTweenTweening { get; set; }

    public void KillTweens()
    {
        if (DoTweenTweening != null)
        {
            DoTweenTweening.Kill();
            DoTweenTweening = null;
        }

        if (DoTweenSequence != null)
        {
            DoTweenSequence.Kill();
            DoTweenSequence = null;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        CardStats.Health -= damageAmount;
        if (CardStats.Health < 0)
            CardStats.Health = 0;
        CardManager.DoDamageEffect(damageAmount);

        CardManager.VisualStateManager.CurrentState.UpdateVisual(CardStats);
    }

    public void HealDamage(int healAmount)
    {
        CardStats.Health += healAmount;
        if (CardStats.Health < 0)
            CardStats.Health = 0;
        CardManager.DoDamageEffect(healAmount, true);
        CardManager.VisualStateManager.CurrentState.UpdateVisual(CardStats);
    }
}