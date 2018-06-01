using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using AsjernasCG.Common.CardModels;
using System;
using Assets.Scripts.Card;

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

    public BaseCardTemplate GetNewCardInstance(int cardTemplateId, int generatedCardId)
    {
        if (CardTemplateCollection.ContainsKey(cardTemplateId))
            return JsonConvert.DeserializeObject<BaseCardTemplate>(CardTemplateCollection[cardTemplateId]);
        return null;
    }

    public GameObject GenerateCardPrefab(int cardTemplateId, int generatedCardId)
    {
        var cardTemplate = GetNewCardInstance(cardTemplateId, generatedCardId);
        var prefab = (GameObject)Instantiate(CardTemplatePrefab);
        var cardManager = prefab.GetComponent<CardManager>();
        cardManager.UpdateCardView(cardTemplate);
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
