using AsjernasCG.Common.BusinessModels.CardModels;
using Assets.Scripts.Card;
using DG.Tweening;
using UnityEngine;

public class ClientSideCard
{
    public GameObject CardViewObject { get; set; }
    public ClientSideCardEvents Events {get;set;}
    public BaseCardTemplate CardStats { get; set; }
    public ParticipatorState ParticipatorState { get; set; }
    public CardLocation CurrentLocation { get; set; }
    public bool IsUnderPlayerControl { get; set; }
    public bool IsHovering { get; set; }
    public Vector3 LastPosition { get; set; }

    public Sequence DoTweenSequence { get; set; }
    public Tween DoTweenTweening { get; set; }
}