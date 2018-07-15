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
    public GameObject QuestTemplatePrefab;

    public TextAsset CardTemplates;

    private readonly Dictionary<int, string> CardTemplateCollection = new Dictionary<int, string>();
    private readonly Dictionary<int, CardManager> Cards = new Dictionary<int, CardManager>();

    public void LoadCards()
    {
        var desCards = JsonConvert.DeserializeObject<List<ClientCardTemplate>>(CardTemplates.text);
        foreach (var card in desCards)
            CardTemplateCollection.Add(card.CardTemplateId, Newtonsoft.Json.JsonConvert.SerializeObject(card));
    }

    public ClientCardTemplate GetNewCardInstance(int cardTemplateId)
    {
        if (CardTemplateCollection.ContainsKey(cardTemplateId))
        {
            var preProcessedCard =  JsonConvert.DeserializeObject<ClientCardTemplate>(CardTemplateCollection[cardTemplateId]);
            switch (preProcessedCard.CardType)
            {
                case CardType.Equipment:
                    //preProcessedCard.CardCastAttachTargetOwningType = CardCastTargetOwningType.Own;
                    //preProcessedCard.AttachmentValidTargets = new List<CardType>() { CardType.Character};
                    break;
                case CardType.Ability:
                    preProcessedCard.InternalCooldownCurrent = preProcessedCard.InternalCooldownTarget;
                    //preProcessedCard.CardCastAttachTargetOwningType = CardCastTargetOwningType.Own;
                    //preProcessedCard.AttachmentValidTargets = new List<CardType>() { CardType.Character };
                    break;
                case CardType.Follower:
                    break;
                case CardType.Event:
                    break;
                case CardType.Minion:
                    break;
                case CardType.Quest:
                    break;
                case CardType.Character:
                    break;
                default:
                    break;
            }
            return preProcessedCard;
        }
        return null;
    }

    //public GameObject GenerateCardPrefab(ClientCardTemplate cardTemplate, int generatedCardId)
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
        //cardManager.UpdateCardView(cardTemplate);
        Cards.Add(generatedCardId, cardManager);
        cardManager.SetInitialTemplate(cardTemplate);
        return prefab;
    }
    //public GameObject GenerateQuestPrefab(int cardTemplateId, int generatedCardId)
    //{
    //    var cardTemplate = GetNewCardInstance(cardTemplateId);
    //    GameObject prefab = null;
    //    prefab = (GameObject)Instantiate(QuestTemplatePrefab);
    //    prefab.layer = LayerMask.NameToLayer("RaycastEligibleTargets");
    //    var cardManager = prefab.GetComponent<CardManager>();
    //    cardTemplate.GeneratedCardId = generatedCardId;
    //    //cardManager.UpdateCardView(cardTemplate);
    //    Cards.Add(generatedCardId, cardManager);
    //    cardManager.SetInitialTemplate(cardTemplate);
    //    return prefab;
    //}

    public CardManager GetCardManager(int generatedCardId)
    {
        if (Cards.ContainsKey(generatedCardId))
            return Cards[generatedCardId];
        return null;
    }
}
