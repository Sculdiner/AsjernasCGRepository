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

    private readonly Dictionary<int, string> CardCollection = new Dictionary<int, string>();

    private void Awake()
    {
        var desCards = JsonConvert.DeserializeObject<List<BaseCardTemplate>>(CardTemplates.text);
        foreach (var card in desCards)
        {
            CardCollection.Add(card.CardTemplateId, Newtonsoft.Json.JsonConvert.SerializeObject(card));
        }
    }

    public BaseCardTemplate GetNewCardInstance(int cardTemplateId, int serverId)
    {
        if (CardCollection.ContainsKey(cardTemplateId))
            return JsonConvert.DeserializeObject<BaseCardTemplate>(CardCollection[cardTemplateId]);
        return null;
    }

    public GameObject GenerateCardPrefab(int cardTemplateId, int serverId)
    {
        var cardTemplate = GetNewCardInstance(cardTemplateId, serverId);
        var prefab = (GameObject)Instantiate(CardTemplatePrefab);
        var cardManager = prefab.GetComponent<CardManager>();
        return prefab;
    }
}
