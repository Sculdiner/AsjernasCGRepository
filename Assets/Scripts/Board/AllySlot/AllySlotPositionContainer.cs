using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AllySlotPositionContainer : SerializedMonoBehaviour
{
    [OdinSerialize]
    public Dictionary<OddPlacementPosition, GameObject> OddSlots;
    [OdinSerialize]
    public Dictionary<EvenPlacementPosition, GameObject> EvenSlots;
}
