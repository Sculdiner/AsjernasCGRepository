using AsjernasCG.Common.BusinessModels.CardModels;
using Assets.Scripts.Card;
using UnityEngine;

public class ClientSideCard
{
    public GameObject CardViewObject { get; set; }
    public ClientSideCardEvents Events {get;set;}
    public BaseCardTemplate CardStats { get; set; }
    public ParticipatorState ParticipatorState { get; set; }
    public CardLocation CurrentLocation { get; set; }
}