using AsjernasCG.Common.BusinessModels.CardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CardManager : MonoBehaviour
{
    public ClientCardTemplate Template;
    //public CardHandHelperComponent CardHandHelperComponent;
    public VisualStateManager VisualStateManager;
    public LineRenderer TargetingGizmo;
    public Canvas TargetingRectangle;
    public PositionalSlotManager SlotManager { get; set; }
    public CharacterManager CharacterManager { get; set; }

    public bool CanBePlayed() { return true; }

    public void SetInitialTemplate(ClientCardTemplate template)
    {
        Template = template;
        VisualStateManager.CurrentState.UpdateVisual(template);
        //UpdateCardView(InitialTemplate);
    }

    public void OnStartBeingTargetedForAttack(ClientSideCard attacker)
    {
        TargetingRectangle.gameObject.SetActive(true);
    }

    public void OnStopBeingTargetedForAttack(ClientSideCard attacker)
    {
        TargetingRectangle.gameObject.SetActive(false);
    }
}
