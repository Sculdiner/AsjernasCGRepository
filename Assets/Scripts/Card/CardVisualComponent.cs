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
    public Image CostSprite;
    public TMP_Text Power;
    public Image PowerSprite;
    public TMP_Text Health;
    public Image HealthSprite;
    public TMP_Text CurrentCooldown;
    public Image RemainingCooldownSprite;
    public Image AbilityOnCooldownSprite;
    public TMP_Text RemainingCooldown;
    public TMP_Text Durability;
    public Image DurabilitySprite;
    public TMP_Text QuestTarget;
    public TMP_Text QuestProgress;
    public Image InitiativeSprite;
    public TMP_Text Initiative;
    public Image ThreatSprite;
    public TMP_Text Threat;

    private void BakeForHandOrPreviewCard(ClientCardTemplate template)
    {
        Debug.Log($"Baking visual for the card {template.CardName}. Bake sub type: {template.CardType.ToString()}");
        Name.text = template.CardName;
        Text.text = template.CardText;

        Image.texture = Resources.Load($"Images/{template.ImagePath}") as Texture2D;
        if (template.CardType == CardType.Event)
        {
            HealthSprite.enabled = false;
            DurabilitySprite.enabled = false;
            PowerSprite.enabled = false;
            RemainingCooldownSprite.enabled = false;
            ThreatSprite.gameObject.SetActive(false);
            InitiativeSprite.gameObject.SetActive(false);
            if (template.BaseResourceCost.HasValue)//template.IsAiControlled)
                Cost.text = template.BaseResourceCost.Value.ToString();
            else
                CostSprite.gameObject.SetActive(false);

        }
        else if (template.CardType == CardType.Follower)
        {
            DurabilitySprite.enabled = false;
            RemainingCooldownSprite.enabled = false;
            Power.text = template.Power.Value.ToString();
            Health.text = template.Health.Value.ToString();
            Cost.text = template.BaseResourceCost.Value.ToString();
            ThreatSprite.gameObject.SetActive(false);
            Initiative.text = template.Initiative.Value.ToString();
        }
        else if (template.CardType == CardType.Ability)
        {
            HealthSprite.enabled = false;
            DurabilitySprite.enabled = false;
            PowerSprite.enabled = false;
            RemainingCooldown.text = template.InternalCooldownTarget.Value.ToString();
            Cost.text = template.BaseResourceCost.Value.ToString();
            ThreatSprite.gameObject.SetActive(false);
            InitiativeSprite.gameObject.SetActive(false);
        }
        else if (template.CardType == CardType.Equipment)
        {
            HealthSprite.enabled = false;
            RemainingCooldownSprite.enabled = false;
            Power.text = template.Power.Value.ToString();
            Durability.text = template.Durability.Value.ToString();
            Cost.text = template.BaseResourceCost.Value.ToString();
            ThreatSprite.gameObject.SetActive(false);
            InitiativeSprite.gameObject.SetActive(false);
        }
        else if (template.CardType == CardType.Minion)
        {
            DurabilitySprite.enabled = false;
            RemainingCooldownSprite.enabled = false;
            Power.text = template.Power.Value.ToString();
            Health.text = template.Health.Value.ToString();
            CostSprite.gameObject.SetActive(false);
            ThreatSprite.gameObject.SetActive(false);
            Initiative.text = template.Initiative.ToString();
        }
        else if (template.CardType == CardType.Quest)
        {
            CostSprite.gameObject.SetActive(false);
            HealthSprite.gameObject.SetActive(false);
            PowerSprite.gameObject.SetActive(false);
            DurabilitySprite.gameObject.SetActive(false);
            RemainingCooldownSprite.gameObject.SetActive(false);
            //DurabilitySprite.enabled = false;
            //RemainingCooldownSprite.enabled = false;
            //Power.text = template.Power.Value.ToString();
            //Health.text = template.Health.Value.ToString();
        }
        else if (template.CardType == CardType.Character)
        {
            CostSprite.gameObject.SetActive(false);
            Power.text = template.Power.Value.ToString();
            Health.text = template.Health.Value.ToString();
            DurabilitySprite.gameObject.SetActive(false);
            RemainingCooldownSprite.gameObject.SetActive(false);
            Threat.text = template.Threat.Value.ToString();
            Initiative.text = template.Initiative.Value.ToString();
        }
    }

    public void BakeForAlly(ClientCardTemplate template)
    {
        Image.texture = Resources.Load($"Images/{template.ImagePath}") as Texture2D;
        Power.text = template.Power.Value.ToString();
        Health.text = template.Health.Value.ToString();
        Initiative.text = template.Initiative.Value.ToString();
    }

    public void BakeForAbility(ClientCardTemplate template)
    {
        Image.texture = Resources.Load($"Images/{template.ImagePath}") as Texture2D;
        if (template.RemainingCooldown == 0)
        {
            RemainingCooldownSprite.gameObject.SetActive(false);
            RemainingCooldown.text = null;
            //highlight
        }
        else
        {
            RemainingCooldownSprite.gameObject.SetActive(true);
            RemainingCooldown.text = template.RemainingCooldown.Value.ToString();
        }

        //if (!template.InternalCooldownCurrent.HasValue)
        //{
        //    template.InternalCooldownCurrent = 0;
        //}
        //var cd = template.InternalCooldownTarget.Value - template.InternalCooldownCurrent.Value;
        //if (cd > 0)
        //{
        //    RemainingCooldown.text = (template.InternalCooldownTarget.Value - template.InternalCooldownCurrent.Value).ToString();
        //    RemainingCooldownSprite.gameObject.SetActive(true);
        //}
        //else
        //{
        //    RemainingCooldown.text = template.InternalCooldownTarget.Value.ToString() + ("Max");
        //    RemainingCooldownSprite.gameObject.SetActive(false);
        //}
    }

    public void BakeForEquipment(ClientCardTemplate template)
    {
        Image.texture = Resources.Load($"Images/{template.ImagePath}") as Texture2D;
        //highlight on low durability
    }

    public void BakeForCharacter(ClientCardTemplate template)
    {
        Image.texture = Resources.Load($"Images/{template.ImagePath}") as Texture2D;
        Power.text = template.Power.Value.ToString();
        Health.text = template.Health.Value.ToString();
        Threat.text = template.Threat.Value.ToString();
        Initiative.text = template.Initiative.Value.ToString();
    }

    public void BakeForQuest(ClientCardTemplate template)
    {
        QuestTarget.text = template.QuestObjectiveTarget?.ToString();
        QuestProgress.text = template.CurrentQuestPoints?.ToString();
    }

    public void UpdateVisual(ClientCardTemplate template)
    {
        Debug.Log($"Updating visual for the card {template.CardName}. Bake type: {VisualState.ToString()}");
        switch (VisualState)
        {
            case CardVisualState.None:
                break;
            case CardVisualState.Card:
                BakeForHandOrPreviewCard(template);
                break;
            case CardVisualState.Preview:
                BakeForHandOrPreviewCard(template);
                break;
            case CardVisualState.Follower:
                BakeForAlly(template);
                break;
            case CardVisualState.Ability:
                BakeForAbility(template);
                break;
            case CardVisualState.Equipment:
                BakeForEquipment(template);
                break;
            case CardVisualState.Character:
                BakeForCharacter(template);
                break;
            case CardVisualState.Quest:
                BakeForQuest(template);
                break;
            default:
                break;
        }

        //if (Name != null)
        //    Name.text = template.CardName;
        //if (Image != null)
        //    Image.texture = Resources.Load($"Images/{template.ImagePath}") as Texture2D;

        //if (Power != null)
        //{
        //    PowerSprite.gameObject.SetActive(true);
        //    Power.text = template.Power?.ToString();
        //}
        //else
        //{
        //    if (PowerSprite != null)
        //        PowerSprite.gameObject.SetActive(false);
        //}


        //if (Health != null)
        //{
        //    HealthSprite.gameObject.SetActive(true);
        //    Health.text = template.Health?.ToString();
        //}
        //else
        //{
        //    if (HealthSprite != null)
        //    {
        //        HealthSprite.gameObject.SetActive(false);
        //    }
        //}

        //if (Cost != null)
        //{
        //    CostSprite.gameObject.SetActive(true);
        //    Cost.text = template.BaseResourceCost?.ToString();
        //}
        //else
        //{
        //    if (CostSprite != null)
        //    {
        //        CostSprite.gameObject.SetActive(false);
        //    }
        //}

        //if (template.CardType == CardType.Equipment && DurabilitySprite != null)
        //{
        //    DurabilitySprite.gameObject.SetActive(true);
        //    Durability.text = template.Durability?.ToString();
        //}
        //else
        //{
        //    if (DurabilitySprite != null)
        //    {
        //        CostSprite.gameObject.SetActive(false);
        //    }
        //}



        //if (Text != null)
        //{
        //    Text.text = template.CardText;
        //}

        //if (QuestTarget != null)
        //{
        //    QuestTarget.text = template.QuestObjectiveTarget?.ToString();
        //    //QuestProgress.text = template.CurrentQuestPoints?.ToString();
        //}
        //if (QuestProgress != null)
        //{
        //    QuestProgress.text = template.CurrentQuestPoints?.ToString();
        //}

        //if (template.CardType == CardType.Ability)
        //{
        //    if (!template.InternalCooldownCurrent.HasValue)
        //    {
        //        template.InternalCooldownCurrent = 0;
        //    }
        //    RemainingCooldown.text = (template.InternalCooldownTarget.Value - template.InternalCooldownCurrent.Value).ToString();
        //}
        //else
        //{
        //    if (RemainingCooldownSprite != null)
        //    {
        //        RemainingCooldownSprite.gameObject.SetActive(false);
        //    }
        //}
        //if (template.CardType == CardType.Ability && AbilityOnCooldownSprite != null)
        //{
        //    if (!template.InternalCooldownCurrent.HasValue)
        //    {
        //        template.InternalCooldownCurrent = 0;
        //    }
        //    var remainingCd = template.InternalCooldownTarget.Value - template.InternalCooldownCurrent.Value;
        //    if (remainingCd > 0)
        //    {
        //        AbilityOnCooldownSprite.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        AbilityOnCooldownSprite.gameObject.SetActive(false);
        //    }
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
