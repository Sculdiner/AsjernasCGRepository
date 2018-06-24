using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HandSlotPreviewPositionContainer : SerializedMonoBehaviour
{
    [OdinSerialize]
    public Dictionary<PlacementPosition, Transform> Slots;
}
