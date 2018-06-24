using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HandSlotPositionContainer : SerializedMonoBehaviour
{
    [OdinSerialize]
    public Dictionary<PlacementPosition, HandSlotWithCollider> OddSlots;
    [OdinSerialize]
    public Dictionary<PlacementPosition, HandSlotWithCollider> EvenSlots;
}
