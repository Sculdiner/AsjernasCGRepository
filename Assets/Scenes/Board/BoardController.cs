using AsjernasCG.Common.BusinessModels.BasicModels;
using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.OperationHelpers.Game;
using AsjernasCG.Common.OperationModels;
using AsjernasCG.Common.OperationModels.BasicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BoardController : ViewController
{
    public BoardView _view;

    public BoardController(View controlledView) : base(controlledView)
    {
        _view = controlledView as BoardView;
    }

    public void SendClientReady()
    {
        SendOperation(new GameOperationHelper<GameStatusModel>(new GameStatusModel() { ClientGameSetupDone = true }), true, 0, false);
    }

    public void Play_CardWithoutTarget(int cardId)
    {
        SendOperation(new CardCastFieldOperationHelper<IntegerModel>(new IntegerModel() { Value = cardId }), true, 0, false);
    }

    public void Play_CardWithTarget(int cardId, int targetId)
    {
        SendOperation(new CardCastTargetedOperationHelper<TargetedCastModel>(new TargetedCastModel()
        {
            CastSourceId = cardId,
            TargetId = targetId
        }), true, 0, false);
    }

    public void Play_AttachmentCardWithoutTarget(int cardId, int attachOnId)
    {
        SendOperation(new CardCastAttachmentOperationHelper<AttachmentCastModel>(new AttachmentCastModel()
        {
            CastSourceAttachmentId = cardId,
            AttachedOnCardId = attachOnId
        }), true, 0, false);
    }

    public void Play_AttachmentCardWithTarget(int cardId, int attachOnId, int targetId)
    {
        SendOperation(new CardCastAttachmentWithTargetOperationHelper<AttachmentWithTargetCastModel>(new AttachmentWithTargetCastModel()
        {
            CastSourceAttachmentId = cardId,
            AttachedOnCardId = attachOnId,
            CastEffectTargetId = targetId
        }), true, 0, false);
    }

    public void ActivateAbilityWithTarget(int cardId, int targetId)
    {
        SendOperation(new AsjernasCG.Common.OperationHelpers.Game.AbilityCastWithTargetOperationHelper<TargetedCastModel>(new TargetedCastModel()
        {
            CastSourceId = cardId,
            TargetId = targetId
        }), true, 0, false);
    }

    public void ActivateAbilityWithoutTarget(int cardId)
    {
        SendOperation(new AbilityCastWithoutTargetOperationHelper<IntegerModel>(new IntegerModel()
        {
            Value = cardId
        }), true, 0, false);
    }

    public void Pass()
    {
        _view.BoardManager.ClearActiveCharacterSlot();
        SendOperation(new PassOperationHelper<EmptyModel>(new EmptyModel()), true, 0, false);
    }

    public void Attack(int sourceId, int targetId)
    {
        _view.BoardManager.ClearActiveCharacterSlot();
        SendOperation(new AttackOperationHelper<AttackModel>(new AttackModel()
        {
            AttackingCard = sourceId,
            AttackedCard = targetId
        }), true, 0, false);
    }

    public void Quest(int questingCardId)
    {
        _view.BoardManager.ClearActiveCharacterSlot();
        SendOperation(new QuestOperationHelper<IntegerModel>(new IntegerModel()
        {
            Value = questingCardId
        }), true, 0, false);
    }


}
