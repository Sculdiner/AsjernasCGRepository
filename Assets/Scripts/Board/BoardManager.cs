using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.EventModels.Game;
using Assets.Scripts.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public MasterCardManager MasterCardManager;
    private BoardCardReferenceCollection CardReferenceCollection;
    private Dictionary<int, ParticipatorState> ParticipatorReferenceCollection;
    private PlayerState CurrentUserPlayerState;
    private PlayerState TeammatePlayerState;
    private AIState AiState;

    public BoardManager()
    {
        CardReferenceCollection = new BoardCardReferenceCollection();
        ParticipatorReferenceCollection = new Dictionary<int, ParticipatorState>();
    }

    public ClientSideCard RegisterPlayerCard(GameObject gameObject, BaseCardTemplate card, CardLocation location, int userId)
    {
        var clientSideCard = new ClientSideCard()
        {
            CardStats = card,
            CardViewObject = gameObject,
            CurrentLocation = location,
            CardManager = gameObject.GetComponent<CardManager>()
        };
        clientSideCard.CardManager.CardHandHelperComponent.Card = clientSideCard;

        var partState = ParticipatorReferenceCollection[userId];
        var eventHandling = gameObject.GetComponent<ClientSideCardEvents>();
        gameObject.GetComponent<Draggable>().ControllingCard = clientSideCard;
        return RegisterCard(clientSideCard, eventHandling, partState);
    }

    public ClientSideCard RegisterPlayerCard(GameObject gameObject, int cardTemplateId, CardLocation location, int userId)
    {
        var cardInstance = MasterCardManager.GetNewCardInstance(cardTemplateId);
        return RegisterPlayerCard(gameObject, cardInstance, location, userId);
    }

    private ClientSideCard RegisterCard(ClientSideCard card, ClientSideCardEvents eventHandling, ParticipatorState participatorState)
    {
        card.ParticipatorState = participatorState;
        card.Events = eventHandling;
        card.Events.ControllingCard = card;
        card.CardViewObject.GetComponent<Draggable>().BoardManager = this;
        card.CardViewObject.name = card.CardStats.GeneratedCardId.ToString();
        CardReferenceCollection.RegisterCardToGame(card);
        if (participatorState is PlayerState)
        {
            ParticipatorReferenceCollection[((PlayerState)participatorState).UserId].Deck.Add(card);
        }
        else
        {
            AiState.Deck.Add(card);
        }
        return card;
    }

    public void RegisterPlayer(int userId)
    {
        var state = new PlayerState() { UserId = userId };
        if (userId == PhotonEngine.UserId)
        {
            var ownFollowerArea = GameObject.Find("RightPlayerAllySlotContainer");
            CurrentUserPlayerState = state;
            ((AllySlotManager)ownFollowerArea.GetComponent<AllySlotManager>()).OwningPlayer = state;
        }
        else
        {
            var teammateArea = GameObject.Find("LeftPlayerAllySlotContainer");
            ((AllySlotManager)teammateArea.GetComponent<AllySlotManager>()).OwningPlayer = state;
            TeammatePlayerState = state;
        }
        ParticipatorReferenceCollection.Add(userId, state);
    }

    public ClientSideCard GetCard(int cardId)
    {
        return CardReferenceCollection.GetCard(cardId);
    }

    public ClientSideCard GetCard(RaycastHit collidingObject)
    {
        var collidedObject = collidingObject.transform.gameObject;
        if (collidedObject == null)
            return null;
        return GetCard(collidedObject);
    }

    public ClientSideCard GetCard(GameObject gameObject)
    {
        var component = gameObject.GetComponent<CardManager>();
        if (component == null)
            return null;

        var generatedCardId = (component as CardManager).InitialTemplate.GeneratedCardId;
        return GetCard(generatedCardId);
    }

    public bool CurrentUserIsCardOwner(ClientSideCard card)
    {
        if (card.ParticipatorState is PlayerState)
        {
            var castedState = (PlayerState)card.ParticipatorState;
            if (castedState.UserId == PhotonEngine.UserId)
                return true;
        }
        return false;
    }

    public bool CurrentUserIsCardOwner(int cardId)
    {
        return CurrentUserIsCardOwner(GetCard(cardId));
    }

    public bool CheckIfCardHasValidTargetsOnBoard(ClientSideCard card)
    {
        switch (card.CardStats.CastTargetOwningType.Value)
        {
            case CardCastTargetOwningType.Enemy:
                if (AiState.Deck.Any(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)))
                    return true;
                break;
            case CardCastTargetOwningType.Own:
                var ownBoardCards = CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea);
                if (ownBoardCards != null && ownBoardCards.Any(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)))
                    return true;
                break;
            case CardCastTargetOwningType.Teammate:
                var teammateBoardCards = TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea);
                if (teammateBoardCards != null && teammateBoardCards.Any(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)))
                    return true;
                break;
            case CardCastTargetOwningType.Friendly:
                var ownBoardCardsF = CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea);
                if (ownBoardCardsF != null && ownBoardCardsF.Any(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)))
                    return true;
                var teammateBoardCardsF = TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea);
                if (teammateBoardCardsF != null && teammateBoardCardsF.Any(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)))
                    return true;
                break;
            case CardCastTargetOwningType.All:
                var ownBoardCardsA = CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea);
                if (ownBoardCardsA != null && ownBoardCardsA.Any(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)))
                    return true;
                var teammateBoardCardsA = TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea);
                if (teammateBoardCardsA != null && teammateBoardCardsA.Any(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)))
                    return true;
                if (AiState.Deck.Any(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)))
                    return true;
                break;
        }
        return false;
    }

    public List<ClientSideCard> FindValidTargetsOnBoard(ClientSideCard card)
    {
        var validTargetsList = new List<ClientSideCard>();

        switch (card.CardStats.CastTargetOwningType.Value)
        {
            case CardCastTargetOwningType.Enemy:
                return AiState.Deck.Where(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)).ToList();
            case CardCastTargetOwningType.Own:
                return CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.CastValidTargets.Contains(f.CardStats.CardType)).ToList();
            case CardCastTargetOwningType.Teammate:
                return TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.CastValidTargets.Contains(f.CardStats.CardType)).ToList();
            case CardCastTargetOwningType.Friendly:
                var ownValidCards = CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.CastValidTargets.Contains(f.CardStats.CardType)).ToList();
                if (ownValidCards != null && ownValidCards.Any())
                    validTargetsList.AddRange(ownValidCards);

                var teammateValidCards = TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.CastValidTargets.Contains(f.CardStats.CardType)).ToList();
                if (teammateValidCards != null && teammateValidCards.Any())
                    validTargetsList.AddRange(teammateValidCards);
                return validTargetsList;
            case CardCastTargetOwningType.All:
                var ownValidACards = CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.CastValidTargets.Contains(f.CardStats.CardType)).ToList();
                if (ownValidACards != null && ownValidACards.Any())
                    validTargetsList.AddRange(ownValidACards);

                var teammateValidACards = TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.CastValidTargets.Contains(f.CardStats.CardType)).ToList();
                if (teammateValidACards != null && teammateValidACards.Any())
                    validTargetsList.AddRange(teammateValidACards);

                var aiValidCards = AiState.Deck.Where(c => card.CardStats.CastValidTargets.Contains(c.CardStats.CardType)).ToList();
                if (aiValidCards != null && aiValidCards.Any())
                    validTargetsList.AddRange(aiValidCards);

                return validTargetsList;
        }
        return validTargetsList;
    }

    public enum CardOwnerType
    {
        Own = 0,
        Teammate = 1,
        Enemy = 2
    }

    public CardOwnerType GetCardRelativeOwnerType(PlayerState sourcePlayerState, ClientSideCard targetCard)
    {
        if (targetCard.CardStats.IsAiControlled)
            return CardOwnerType.Enemy;

        if ((targetCard.ParticipatorState as PlayerState).UserId == sourcePlayerState.UserId)
            return CardOwnerType.Own;
        else
            return CardOwnerType.Teammate;
    }

    public bool TargetedCastOwningTypeIsValid(ClientSideCard sourceCard, ClientSideCard targetCard)
    {
        var cardOwnerType = GetCardRelativeOwnerType((PlayerState)sourceCard.ParticipatorState, targetCard);
        switch (sourceCard.CardStats.CastTargetOwningType)
        {
            case CardCastTargetOwningType.Enemy:
                if (cardOwnerType == CardOwnerType.Enemy)
                    break;
                return false;
            case CardCastTargetOwningType.Own:
                if (cardOwnerType == CardOwnerType.Own)
                    break;
                return false;
            case CardCastTargetOwningType.Teammate:
                if (cardOwnerType == CardOwnerType.Teammate)
                    break;
                return false;
            case CardCastTargetOwningType.Friendly:
                if (cardOwnerType == CardOwnerType.Own || cardOwnerType == CardOwnerType.Teammate)
                    break;
                return false;
            case CardCastTargetOwningType.All:
                break;
            default:
                break;
        }
        return true;
    }

    public bool TargetedCastTargetTypeIsValid(ClientSideCard sourceCard, ClientSideCard targetCard)
    {
        if (sourceCard.CardStats.CastValidTargets.Contains(targetCard.CardStats.CardType))
            return true;
        return false;
    }

    public bool TargetCastIsValid(ClientSideCard sourceCard, ClientSideCard targetCard)
    {
        if (sourceCard.CardStats.CardType == CardType.Event || sourceCard.CardStats.CardType == CardType.Action || targetCard != null)
        {
            if (targetCard == null)
                return false;

            if (!TargetedCastOwningTypeIsValid(sourceCard, targetCard))
                return false;

            if (!TargetedCastTargetTypeIsValid(sourceCard, targetCard))
                return false;

            return true;
        }
        else
        {
            //NO EVENTS - ACTIONS HERE
            //ONLY FOLLOWERS THAT HAVE NOT PROVIDED A TARGET CAN REACH THIS CODE
            //Note: It is permitted to play a permanent without any targets (without activating its effect) if there are NO valid targets.
            if (CheckIfCardHasValidTargetsOnBoard(sourceCard))
                return false;
            return true;
        }
    }

    public List<ClientSideCard> GetMyHand()
    {
        return CurrentUserPlayerState.Deck.Where(s => s.CurrentLocation == CardLocation.Hand).ToList();
    }

    public static Action<ClientSideCard> OnCursorEntersCard;
    public static Action<ClientSideCard> OnCursorExitsCard;
}


