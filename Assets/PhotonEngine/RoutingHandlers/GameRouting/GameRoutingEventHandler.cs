using AsjernasCG.Common.BusinessModels.BasicModels;
using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common.OperationModels.BasicModels;
using System.Collections.Generic;

public class GameRoutingEventHandler : BaseRoutingEventHandler
{
    public GameRoutingEventHandler()
    {
        _subEventHandlerCollection = new SubEventHandlerCollection();
        _subEventHandlerCollection.AddHandler(new GameCancelationEventHandler<GameCancelationModel>());
        _subEventHandlerCollection.AddHandler(new GameInitializationEventHandler<GameInitializeModel>());
        _subEventHandlerCollection.AddHandler(new GameStartingEventHandler<StringModel>());
        _subEventHandlerCollection.AddHandler(new CardPlayCancelationEventHandler<CardPlayCancelationReasonModel>());
        _subEventHandlerCollection.AddHandler(new PlayAttachmentCardWithoutTargetEventHandler<DetailedCardModel>());
        _subEventHandlerCollection.AddHandler(new PlayAttachmentCardWithTargetEventHandler<DetailedCardModel>());
        _subEventHandlerCollection.AddHandler(new PlayCardWithoutTargetEventHandler<DetailedCardModel>());
        _subEventHandlerCollection.AddHandler(new PlayCardWithTargetEventHandler<DetailedCardModel>());
        _subEventHandlerCollection.AddHandler(new CardElementBatchValueChangeEventHandler<List<CardElementValueChange>>());
        _subEventHandlerCollection.AddHandler(new CardElementValueChangeEventHandler<CardElementValueChange>());
        _subEventHandlerCollection.AddHandler(new DrawCardHiddenEventHandler<HiddenCardModel>());
        _subEventHandlerCollection.AddHandler(new DrawCardDetailedEventHandler<DetailedCardModel>());
        _subEventHandlerCollection.AddHandler(new AttackCancellationEventHandler<IntegerModel>());
        _subEventHandlerCollection.AddHandler(new AttackEventHandler<AttackModel>());
        _subEventHandlerCollection.AddHandler(new ChangeEncounterQuestEventHandler<DetailedCardModel>());
        _subEventHandlerCollection.AddHandler(new DiscardCardEventHandler<IntegerModel>());
        _subEventHandlerCollection.AddHandler(new EncounterCardEventHandler<DetailedCardModel>());
        _subEventHandlerCollection.AddHandler(new GameEndEventHandler<GameEndWithRewardsModel>());
        _subEventHandlerCollection.AddHandler(new InitiativeSlotsEventHandler<InitiativeSlotsModel>());
        _subEventHandlerCollection.AddHandler(new InitiativeSlotActivatedEventHandler<IntegerModel>());
        _subEventHandlerCollection.AddHandler(new ResourceChangeEventHandler<ResourceChangeModel>());
        _subEventHandlerCollection.AddHandler(new ResourceChangeBatchEventHandler<List<ResourceChangeModel>>());
        _subEventHandlerCollection.AddHandler(new RoundStartEventHandler<RoundStartModel>());
        _subEventHandlerCollection.AddHandler(new EncounterStartEventHandler<EmptyModel>());
        _subEventHandlerCollection.AddHandler(new SetupSlotActivatedEventHandler<IntegerModel>());
        _subEventHandlerCollection.AddHandler(new RemoveCardEventHelperEventHandler<IntegerModel>());
        _subEventHandlerCollection.AddHandler(new DamageEventHandler<DamageEventModel>());
        _subEventHandlerCollection.AddHandler(new HealDamageEventHandler<DamageEventModel>());
    }
    public override ClientEventGroupCode RegisteredEventGroupCode
    {
        get { return ClientEventGroupCode.Game; }
    }
    private SubEventHandlerCollection _subEventHandlerCollection;
    public override SubEventHandlerCollection SubEventHandlerCollection { get { return _subEventHandlerCollection; } }
}
