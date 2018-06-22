using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class EncounterSlotsPositionContainer : SerializedMonoBehaviour
{
    [OdinSerialize]
    public Dictionary<OddPlacementPosition, EncounterSlot> OddSlots;
    [OdinSerialize]
    public Dictionary<EvenPlacementPosition, EncounterSlot> EvenSlots;
}
