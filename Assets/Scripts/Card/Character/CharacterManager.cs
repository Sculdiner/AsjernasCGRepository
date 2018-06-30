using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CardManager CardManager;
    public BoxCollider ColliderInformation;
    public CharacterEquipmentManager CharacterEquipmentManager;
    public CharacterAbilityManager CharacterAbilityManager;

    public void SetCharacter(ClientSideCard card)
    {
        card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Character);
        card.CardViewObject.transform.position = this.gameObject.transform.position;
        CardManager = card.CardManager;

        CardManager.CharacterManager = this;
    }

    public bool SetEquipment(ClientSideCard equipment)
    {
        return CharacterEquipmentManager.AddEquipment(equipment);
    }

    public bool SetAbility(ClientSideCard ability)
    {
        return false;
    }
}