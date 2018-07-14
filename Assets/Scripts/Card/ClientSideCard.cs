using AsjernasCG.Common.BusinessModels.CardModels;
using Assets.Scripts.Card;
using DG.Tweening;
using System;
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
            HoverComponent.SetAction<HandHoverBehaviour>();
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
        if (CardStats.Health <0 )
            CardStats.Health = 0;

        CardManager.VisualStateManager.CurrentState.UpdateVisual(CardStats);
    }

    public void HealDamage(int healAmount)
    {
        CardStats.Health += healAmount;
        if (CardStats.Health < 0)
            CardStats.Health = 0;

        CardManager.VisualStateManager.CurrentState.UpdateVisual(CardStats);
    }

    public void ChangeCardElement(CardElementType type, string value)
    {
        switch (type)
        {
            case CardElementType.CardName:
                break;
            case CardElementType.CardType:
                break;
            case CardElementType.BaseResourceCost:
                CardStats.BaseResourceCost = Int32.Parse(value);
                break;
            case CardElementType.CardText:
                break;
            case CardElementType.Durability:
                CardStats.Durability = Int32.Parse(value);
                break;
            case CardElementType.Traits:
                break;
            case CardElementType.Faction:
                break;
            case CardElementType.Image:
                break;
            case CardElementType.Power:
                CardStats.Power = Int32.Parse(value);
                break;
            case CardElementType.MaxHealth:
                //CardStats.BaseResourceCost = Int32.Parse(value);
                break;
            case CardElementType.Threat:
                break;
            case CardElementType.Initiative:
                break;
            case CardElementType.MaxCooldown:
                break;
            case CardElementType.CurrentCooldown:
                break;
            default:
                break;
        }
        CardManager.VisualStateManager.CurrentState.UpdateVisual(CardStats);
    }
}