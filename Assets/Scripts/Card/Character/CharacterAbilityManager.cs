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

    public List<AbilityCardManager> Abilities = new List<AbilityCardManager>();

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

    public void Move(AbilityCardManager allyCardManager, Transform position)
    {
        allyCardManager.transform.position = position.position;
    }
}
