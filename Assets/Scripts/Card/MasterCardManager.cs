using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Assets.Scripts.Card;
using AsjernasCG.Common.BusinessModels.CardModels;

public class MasterCardManager : MonoBehaviour
{

    public GameObject CardTemplatePrefab;

    public TextAsset CardTemplates;

    private readonly Dictionary<int, string> CardTemplateCollection = new Dictionary<int, string>();
    private readonly Dictionary<int, CardManager> Cards = new Dictionary<int, CardManager>();

    public void LoadCards()
    {
        var desCards = JsonConvert.DeserializeObject<List<BaseCardTemplate>>(CardTemplates.text);
        foreach (var card in desCards)
            CardTemplateCollection.Add(card.CardTemplateId, Newtonsoft.Json.JsonConvert.SerializeObject(card));
    }

    public BaseCardTemplate GetNewCardInstance(int cardTemplateId)
    {
        if (CardTemplateCollection.ContainsKey(cardTemplateId))
            return JsonConvert.DeserializeObject<BaseCardTemplate>(CardTemplateCollection[cardTemplateId]);
        return null;
    }

    //public GameObject GenerateCardPrefab(BaseCardTemplate cardTemplate, int generatedCardId)
    //{
    //    var prefab = (GameObject)Instantiate(CardTemplatePrefab);
    //    var cardManager = prefab.GetComponent<CardManager>();
    //    cardManager.SetInitialTemplate(cardTemplate);
    //    //cardManager.UpdateCardView(cardTemplate);
    //    Cards.Add(generatedCardId, cardManager);
    //    return prefab;
    //}

    public GameObject GenerateCardPrefab(int cardTemplateId, int generatedCardId)
    {
        var cardTemplate = GetNewCardInstance(cardTemplateId);
        GameObject prefab = null;
        prefab = (GameObject)Instantiate(CardTemplatePrefab);
        prefab.layer = LayerMask.NameToLayer("RaycastEligibleTargets");
        var cardManager = prefab.GetComponent<CardManager>();
        cardTemplate.GeneratedCardId = generatedCardId;
        cardManager.SetInitialTemplate(cardTemplate);
        //cardManager.UpdateCardView(cardTemplate);
        Cards.Add(generatedCardId, cardManager);
        return prefab;
    }

    public CardManager GetCardManager(int generatedCardId)
    {
        if (Cards.ContainsKey(generatedCardId))
            return Cards[generatedCardId];
        return null;
    }
}
