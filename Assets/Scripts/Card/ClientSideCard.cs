using AsjernasCG.Common.BusinessModels.CardModels;
using UnityEngine;

public class ClientSideCard : MonoBehaviour
{
    public GameObject CardViewObject { get; set; }
    public BaseCardTemplate CardStats { get; set; }
    public ParticipatorState ParticipatorState { get; set; }
    public CardLocation CurrentLocation { get; set; }
}