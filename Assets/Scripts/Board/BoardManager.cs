using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.EventModels.Game;
using Assets.Scripts.Card;
using DG.Tweening;
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
    public static BoardManager Instance;
    public ClientSideCard ActiveCard;
    public Gradient EnemyActivationHighlightColor;
    public Gradient OwnActivationHighlightColor;
    public Gradient TeammateActivationHighlightColor;
    public PlayerState ActiveSetupSlotPlayer { get; private set; }
    public ClientSideCard ActiveInitiativeCard { get; set; }

    public void Awake()
    {
        Instance = this;
    }

    public BoardManager()
    {
        CardReferenceCollection = new BoardCardReferenceCollection();
        ParticipatorReferenceCollection = new Dictionary<int, ParticipatorState>();
        AiState = new AIState();
    }

    public ClientSideCard RegisterEncounterCard(GameObject gameObject, ClientCardTemplate card, CardLocation location)
    {
        var cardManager = gameObject.GetComponent<CardManager>();
        var clientSideCard = new ClientSideCard()
        {
            CardStats = card,
            CardViewObject = gameObject,
            CardManager = cardManager
        };
        clientSideCard.CardManager.VisualStateManager.OriginalGradientHightlightColor = EnemyActivationHighlightColor;
        if (card.CardType == CardType.Quest)
        {
            if (!card.CurrentQuestPoints.HasValue)
                card.CurrentQuestPoints = 0;
            cardManager.VisualStateManager.ChangeVisual(CardVisualState.Quest);
        }
        clientSideCard.SetLocation(location);

        var partState = AiState;
        var eventHandling = gameObject.GetComponent<ClientSideCardEvents>();

        Destroy(gameObject.GetComponent<Draggable>());
        Destroy(gameObject.GetComponent<DragRotator>());
        //Debug.Log("Destroyed draggable component because the card is controlled by the AI");

        return RegisterCard(clientSideCard, eventHandling, partState);
    }

    public ClientSideCard RegisterPlayerCard(GameObject gameObject, ClientCardTemplate card, CardLocation location, int userId)
    {
        var clientSideCard = new ClientSideCard()
        {
            CardStats = card,
            CardViewObject = gameObject,
            CardManager = gameObject.GetComponent<CardManager>()
        };
        clientSideCard.CardManager.VisualStateManager.OriginalGradientHightlightColor = PhotonEngine.UserId == userId ? OwnActivationHighlightColor : TeammateActivationHighlightColor;
        clientSideCard.SetLocation(location);

        var partState = ParticipatorReferenceCollection[userId];
        var eventHandling = gameObject.GetComponent<ClientSideCardEvents>();

        if (userId == PhotonEngine.UserId)
            gameObject.GetComponent<Draggable>().ControllingCard = clientSideCard;
        else
        {
            Destroy(gameObject.GetComponent<Draggable>());
            //Debug.Log("Destroyed draggable component because the card is not of the current player");
        }

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

    public void RegisterPlayer(int userId, AllySlotManager allySlotManager, PlayerInfoManager piManager)
    {
        var state = new PlayerState() { UserId = userId };
        if (userId == PhotonEngine.UserId)
        {
            CurrentUserPlayerState = state;
            allySlotManager.OwningPlayer = state;
            CurrentUserPlayerState.AllySlotManager = allySlotManager;
            CurrentUserPlayerState.PlayerInfoManager = piManager;
        }
        else
        {
            allySlotManager.OwningPlayer = state;
            TeammatePlayerState = state;
            TeammatePlayerState.AllySlotManager = allySlotManager;
            TeammatePlayerState.PlayerInfoManager = piManager;
        }
        ParticipatorReferenceCollection.Add(userId, state);
    }

    public ClientSideCard GetCard(int cardId)
    {
        return CardReferenceCollection.GetCard(cardId);
    }

    public int DEBUG_SHOWCARDCOUNT()
    {
        return CardReferenceCollection.GetCardCount();
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

        var generatedCardId = (component as CardManager).Template.GeneratedCardId;
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

    public List<ClientSideCard> FindValidAttackOrQuestTargetsOnBoard(ClientSideCard card)
    {
        var validTargets = FindValidAttackTargetsOnBoard(card);
        validTargets.AddRange(FindValidQuestingTargetsOnBoard(card));
        return validTargets;
    }

    public List<ClientSideCard> FindValidAttackTargetsOnBoard(ClientSideCard card)
    {
        var validTargetsList = new List<ClientSideCard>();

        if (card.CardStats.CardType == CardType.Character || card.CardStats.CardType == CardType.Follower)
        {
            var minions = AiState.Deck.Where(s => s.CurrentLocation == CardLocation.PlayArea && s.CardStats.CardType == CardType.Minion);
            if (minions != null && minions.Any())
                validTargetsList.AddRange(minions);
        }
        return validTargetsList;
    }

    public List<ClientSideCard> FindValidQuestingTargetsOnBoard(ClientSideCard card)
    {
        var validTargetsList = new List<ClientSideCard>();

        if (card.CardStats.CardType == CardType.Character)
        {
            validTargetsList.Add(BoardView.Instance.QuestSlotManager.CurrentQuest);
        }

        return validTargetsList;
    }

    public List<ClientSideCard> FindValidAbOrEquipmentTargetsOnBoard(ClientSideCard card)
    {
        return (card.ParticipatorState as PlayerState).Deck?.Where(s => s.CardStats.CardType == CardType.Character && s.CurrentLocation == CardLocation.PlayArea)?.ToList();
    }

    public List<ClientSideCard> FindValidSecondaryTargetsOnBoard(ClientSideCard card)
    {
        var validTargetsList = new List<ClientSideCard>();
        if (!card.CardStats.SecondaryEffectTargetOwningType.HasValue)
            return validTargetsList;

        switch (card.CardStats.SecondaryEffectTargetOwningType.Value)
        {
            case CardCastTargetOwningType.Enemy:
                return AiState.Deck.Where(c => card.CardStats.SecondaryEffectValidTargets.Contains(c.CardStats.CardType)).ToList();
            case CardCastTargetOwningType.Own:
                return CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.SecondaryEffectValidTargets.Contains(f.CardStats.CardType)).ToList();
            case CardCastTargetOwningType.Teammate:
                return TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.SecondaryEffectValidTargets.Contains(f.CardStats.CardType)).ToList();
            case CardCastTargetOwningType.Friendly:
                var ownValidCards = CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.SecondaryEffectValidTargets.Contains(f.CardStats.CardType)).ToList();
                if (ownValidCards != null && ownValidCards.Any())
                    validTargetsList.AddRange(ownValidCards);

                var teammateValidCards = TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.SecondaryEffectValidTargets.Contains(f.CardStats.CardType)).ToList();
                if (teammateValidCards != null && teammateValidCards.Any())
                    validTargetsList.AddRange(teammateValidCards);
                return validTargetsList;
            case CardCastTargetOwningType.All:
                var ownValidACards = CurrentUserPlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.SecondaryEffectValidTargets.Contains(f.CardStats.CardType)).ToList();
                if (ownValidACards != null && ownValidACards.Any())
                    validTargetsList.AddRange(ownValidACards);

                var teammateValidACards = TeammatePlayerState.Deck.Where(f => f.CurrentLocation == CardLocation.PlayArea && card.CardStats.SecondaryEffectValidTargets.Contains(f.CardStats.CardType)).ToList();
                if (teammateValidACards != null && teammateValidACards.Any())
                    validTargetsList.AddRange(teammateValidACards);

                var aiValidCards = AiState.Deck.Where(c => card.CardStats.SecondaryEffectValidTargets.Contains(c.CardStats.CardType)).ToList();
                if (aiValidCards != null && aiValidCards.Any())
                    validTargetsList.AddRange(aiValidCards);

                return validTargetsList;
        }
        return validTargetsList;
    }


    public List<ClientSideCard> FindValidTargetsOnBoard(ClientSideCard card)
    {
        var validTargetsList = new List<ClientSideCard>();
        if (!card.CardStats.CastTargetOwningType.HasValue)
            return validTargetsList;

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

    public PlayerState GetPlayerStateById(int userId)
    {
        return ParticipatorReferenceCollection[userId] as PlayerState;
    }

    public PlayerState GetCurrentUserPlayerState()
    {
        return CurrentUserPlayerState;
    }

    public void ClearActiveCharacterSlot()
    {
        ActiveInitiativeCard?.CardManager.VisualStateManager.EndHighlight();
        ActiveInitiativeCard = null;
    }

    public void SetupSlotActivated(int playerId)
    {
        CurrentActiveInitiativeSlots.Clear();
        TurnStatus = TurnStatus.Setup;
        if (playerId == PhotonEngine.UserId)
        {
            BoardView.Instance.TurnButton.FlipToPass();
            BoardView.Instance.TurnMessenger.Show("Your Setup");
        }
        else
        {
            BoardView.Instance.TurnButton.FlipToWait();
            BoardView.Instance.TurnMessenger.Show("Teammate's Setup");
        }
        ActiveSetupSlotPlayer = GetPlayerStateById(playerId);
    }

    public List<ClientSideCard> CurrentActiveInitiativeSlots = new List<ClientSideCard>();

    public void ActivateInitiativeSlot(int cardId)
    {
        ActiveCard?.CardManager.GetComponent<Draggable>()?.ForceKillDraggingAction();
        TurnStatus = TurnStatus.Encounter;
        ActiveInitiativeCard?.CardManager.VisualStateManager.EndHighlight();
        CurrentActiveInitiativeSlots.Clear();
        var card = GetCard(cardId);
        CurrentActiveInitiativeSlots.Add(card);
        //the slot is of the current user
        ActiveInitiativeCard = card;
        if (card.ParticipatorState is PlayerState && ((PlayerState)card.ParticipatorState).UserId == PhotonEngine.UserId)
        {
            //BoardView.Instance.TurnMessenger.Show($"{card.CardStats.CardName} turn");
            ActiveInitiativeCard?.CardManager.VisualStateManager.Highlight(HighlightType.InitiativeSlotActivated);
            BoardView.Instance.TurnButton.FlipToPass();
            //Debug.Log($"Flipped to Pass side because the current active slot ({card.CardStats.CardName} - id: {card.CardStats.GeneratedCardId}) is mine");
            var hand = card.ParticipatorState.Deck.Where(s => s.CurrentLocation == CardLocation.Hand);
            if (hand != null && hand.Any())
                CurrentActiveInitiativeSlots.AddRange(hand);
            if (ActiveInitiativeCard?.CardManager.CharacterManager != null)
            {
                var abilities = ActiveInitiativeCard.CardManager.CharacterManager.CharacterAbilityManager.Abilities;
                if (abilities!=null && abilities.Any())
                {
                    CurrentActiveInitiativeSlots.AddRange(abilities);
                }
            }
            
        }
        else
        {
            card.CardManager.VisualStateManager.Highlight(HighlightType.Enemy); //not correct
            BoardView.Instance.TurnButton.FlipToWait();
            Debug.Log($"Flipped to Wait side because the current active slot ({card.CardStats.CardName} - id: {card.CardStats.GeneratedCardId}) is not mine");
        }

        BoardView.Instance.InitiativeManager.ActivateSlot(cardId);
    }

    public void Pass()
    {
        ActiveInitiativeCard?.CardManager.VisualStateManager.EndHighlight();
        (BoardView.Instance.Controller as BoardController).Pass();
    }

    public void RemoveCardFromPlay(int cardId)
    {
        var card = GetCard(cardId);
        if (card != null)
        {
            card.CardManager.SlotManager?.RemoveSlot(cardId);
            card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);
            card.SetLocation(CardLocation.DiscardPile);
            card.CardViewObject.SetActive(false);
        }
    }

    public void ChangeResources(int userId, int resources)
    {
        var player = GetPlayerStateById(userId);
        player.Resources = resources;
        player.UpdateResources();
        HighlightAvailableForPlayCards();
    }

    public void EncounterCard(int cardTemplateId, int generatedCardId, Action onEffectCompletion)
    {
        var cardPrefab = MasterCardManager.GenerateCardPrefab(cardTemplateId, generatedCardId);
        var cardManager = MasterCardManager.GetCardManager(generatedCardId);

        var ccc = RegisterEncounterCard(cardPrefab, cardManager.Template, CardLocation.PlayArea);
        if (cardManager.Template.CardType == CardType.Minion)
            BoardView.Instance.EncounterSlotManager.AddEncounterCardToASlot(ccc);
        else
            DisplayCardPlayEffect(ccc, onEffectCompletion);
    }

    private void DisplayCardPlayEffect(ClientSideCard card, Action onEffectFinishCallback)
    {
        var targetTransform = GameObject.Find("OpponentPlayedCardold").transform;

        var sequence = DOTween.Sequence();
        card.CardManager.VisualStateManager.DeactivatePreview();
        card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);
        sequence.Insert(0, card.CardViewObject.transform.DOMove(targetTransform.position, 1f));
        sequence.Insert(0, card.CardViewObject.transform.DORotate(targetTransform.rotation.eulerAngles, 1f));
        sequence.Insert(0, card.CardViewObject.transform.DOScale(targetTransform.localScale, 1f));
        sequence.Insert(1, card.CardViewObject.transform.DOScale(targetTransform.localScale, 0.6f));
        sequence.InsertCallback(1.6f, () =>
        {
            card.CardManager.SlotManager?.RemoveSlot(card.CardStats.GeneratedCardId);
        });
        sequence.Insert(1.6f, card.CardViewObject.transform.DOScale(0f, 1f));
        sequence.InsertCallback(2.6f, () =>
        {
            card.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);
            onEffectFinishCallback?.Invoke();
            PhotonEngine.CompletedAction();
        });
    }
    public void DisplayTeammateCardPlayEffect(ClientSideCard card, Action onEffectCompletion)
    {
        DisplayCardPlayEffect(card, onEffectCompletion);
    }

    public void DisplayTeammateCardPlayEffect(int cardTemplateId, int generatedCardId, int? ownerId, Action onEffectCompletion)
    {
        var cardPrefab = MasterCardManager.GenerateCardPrefab(cardTemplateId, generatedCardId);
        var template = cardPrefab.GetComponent<CardManager>().Template;
        var card = RegisterPlayerCard(cardPrefab, cardPrefab.GetComponent<CardManager>().Template, CardLocation.DiscardPile, ownerId.Value);

        DisplayCardPlayEffect(card, onEffectCompletion);
    }

    public void HighlightAvailableForPlayCards()
    {
        var state = GetCurrentUserPlayerState();
        if (state.Deck != null && state.Deck.Any())
        {
            var hand = state.Deck.Where(s => s.CurrentLocation == CardLocation.Hand);
            if (hand!=null && hand.Any())
            {
                foreach (var _card in hand)
                {
                    if (_card.CardStats.BaseResourceCost <= state.Resources)
                    {
                        _card.CardManager.VisualStateManager.Highlight(HighlightType.AvailableToPlay);
                    }
                    else
                    {
                        _card.CardManager.VisualStateManager.EndHighlight();
                    }
                }
            }
        }
    }

    public TurnStatus TurnStatus = TurnStatus.PreGameStart;
    public static Action<ClientSideCard> OnCursorEntersCard;
    public static Action<ClientSideCard> OnCursorExitsCard;
}


