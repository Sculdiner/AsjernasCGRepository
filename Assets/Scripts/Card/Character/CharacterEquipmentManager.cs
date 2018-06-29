using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterEquipmentManager : SerializedMonoBehaviour
{
    [OdinSerialize]
    public Dictionary<PlacementPosition, Transform> PositionalSlots;

    private List<EquipmentCardManager> Equipments = new List<EquipmentCardManager>();

    public void UpdatePositions()
    {
        switch (Equipments.Count)
        {
            case 0:
                break;
            case 1:
                SetCardPositionSlot(Equipments[0], PlacementPosition.OddMiddleLeft);
                break;
            case 2:
                SetCardPositionSlot(Equipments[0], PlacementPosition.OddMiddleLeft);
                SetCardPositionSlot(Equipments[1], PlacementPosition.OddMiddle);
                break;
            case 3:
                SetCardPositionSlot(Equipments[0], PlacementPosition.OddMiddleLeft);
                SetCardPositionSlot(Equipments[1], PlacementPosition.OddMiddle);
                SetCardPositionSlot(Equipments[2], PlacementPosition.OddMiddleRight);
                break;
            default:
                break;
        }
    }


    public void SetCardPositionSlot(EquipmentCardManager cardManager, PlacementPosition position)
    {
        var slot = PositionalSlots[position];
        cardManager.transform.position = slot.position;
    }
}