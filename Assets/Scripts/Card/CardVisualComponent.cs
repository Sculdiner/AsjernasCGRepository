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
    public TMP_Text CurrentCooldown;
    public TMP_Text CooldownTarget;
    public TMP_Text QuestTarget;
    public TMP_Text QuestProgress;

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
        if (Power!=null)
        {
            Power.text = template.Power?.ToString();
        }
        if (Health != null)
        {
            Health.text = template.Health?.ToString();
        }
        if (Cost != null)
        {
            Cost.text = template.BaseResourceCost?.ToString();
        }

        if (Text != null)
        {
            Text.text = template.CardText;
        }

        if (QuestTarget != null)
        {
            QuestTarget.text = template.QuestObjectiveTarget?.ToString();
        }
        if (QuestProgress != null)
        {
            QuestProgress.text = template.CurrentQuestPoints?.ToString();
        }

        //if (Cooldown !=null)
        //{
        //    Cooldown.text = template.Co
        //}
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
