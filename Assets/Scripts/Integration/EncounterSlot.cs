using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class EncounterSlot : MonoBehaviour
{
    public ClientSideCard EncounterCard { get; set; }

    public bool IsFilled { get; set; }

    public Vector3 GetMyWorldPosition()
    {
        return this.transform.position;
    }
}
