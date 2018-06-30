using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DragRotatorAxisInfo
{
    public float m_ForceMultiplier = 15;
    public float m_MinDegrees = 45f;
    public float m_MaxDegrees = 45f;
    public float m_RestSeconds = 2.5f;
}