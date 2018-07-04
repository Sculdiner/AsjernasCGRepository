using AsjernasCG.Common.BusinessModels.CardModels;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisualComponent : MonoBehaviour
{
    public GameObject Visual;
    public CardVisualState VisualState;

    public TMP_Text Name;
    public TMP_Text Text;
    public RawImage Image;
    public TMP_Text Cost;
    public TMP_Text Power;
    public TMP_Text Health;

    public void UpdateVisual(ClientCardTemplate template)
    {
        if (Name != null)
        {
            Name.text = template.CardName;
        }
        if (Image != null)
        {
            Image.texture = Resources.Load($"Images/{template.ImagePath}") as Texture2D;
        }
    }

    public void Hide()
    {
        Visual.SetActive(false);
    }

    public void Show()
    {
        Visual.SetActive(true);
    }
}
