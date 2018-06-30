using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterAbilityManager : SerializedMonoBehaviour
{
    public Transform PositionSlot1;
    public Transform PositionSlot2;

    public Material DefaultAbilityMaterial;

    public List<ClientSideCard> Abilities = new List<ClientSideCard>();

    public void UpdatePositions()
    {
        switch (Abilities.Count)
        {
            case 0:
                break;
            case 1:
                Move(Abilities[0], PositionSlot1);
                break;
            case 2:
                Move(Abilities[0], PositionSlot1);
                Move(Abilities[1], PositionSlot2);
                break;
            default:
                break;
        }
    }


    public void AddAbility(ClientSideCard abilityToAdd)
    {
        abilityToAdd.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Ability);

        Abilities.Add(abilityToAdd);
        UpdatePositions();
    }

    public void Move(ClientSideCard abilityCardManager, Transform position)
    {

       // abilityCardManager.transform.position = position.position;
    }
}
