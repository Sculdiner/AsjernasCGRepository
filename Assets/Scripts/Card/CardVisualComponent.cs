using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardVisualComponent : MonoBehaviour
{
    public GameObject Visual;
    public CardVisualState VisualState;

    public void Hide()
    {
        Visual.SetActive(false);
    }

    public void Show()
    {
        Visual.SetActive(true);
    }
}
