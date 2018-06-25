using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HandSlotPositionContainerV2 : SerializedMonoBehaviour
{
    [OdinSerialize]
    public Dictionary<PlacementPosition, Transform> OddSlots;
    [OdinSerialize]
    public Dictionary<PlacementPosition, Transform> EvenSlots;
}
