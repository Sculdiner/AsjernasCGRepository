using AsjernasCG.Common.BusinessModels.CardModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CardManager : MonoBehaviour
{
    public ClientCardTemplate Template;
    //public CardHandHelperComponent CardHandHelperComponent;
    public VisualStateManager VisualStateManager;
    public LineRenderer TargetingGizmo;
    public Canvas TargetingRectangle;
    public Canvas TakeDamageEffect;
    public PositionalSlotManager SlotManager { get; set; }
    public CharacterManager CharacterManager { get; set; }

    public bool CanBePlayed() { return true; }

    public void SetInitialTemplate(ClientCardTemplate template)
    {
        Template = template;
        VisualStateManager.CurrentState?.UpdateVisual(template);
        //UpdateCardView(InitialTemplate);
    }

    public void OnStartBeingTargetedForAttack(ClientSideCard attacker)
    {
        TargetingRectangle.gameObject.SetActive(true);
    }

    public void OnStopBeingTargetedForAttack(ClientSideCard attacker)
    {
        TargetingRectangle.gameObject.SetActive(false);
    }
    public void DoDamageEffect(int damageAmount, bool isHeal = false)
    {
        if (!isHeal)
        {
            TakeDamageEffect.gameObject.GetComponentInChildren<Image>().color = Color.red;
            TakeDamageEffect.gameObject.GetComponentInChildren<TMP_Text>().text = $"-{damageAmount}";
            TakeDamageEffect.gameObject.SetActive(true);
            TakeDamageEffect.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
        else
        {
            TakeDamageEffect.gameObject.GetComponentInChildren<Image>().color = Color.green;
            TakeDamageEffect.gameObject.GetComponentInChildren<TMP_Text>().text = $"+{damageAmount}";
            TakeDamageEffect.gameObject.SetActive(true);
            TakeDamageEffect.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
        var coroutine = WaitAndSetDamageEffectInactive();
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitAndSetDamageEffectInactive()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.3f);
            TakeDamageEffect.gameObject.SetActive(false);
        }
    }
}
