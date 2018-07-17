using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.ClientEventCodes;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventHandler<TModel> : BaseEventHandler<TModel> where TModel : AttackModel
{
    public AttackEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.CallbackSync;
    }
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.Attack;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        var attacker = (view as BoardView).BoardManager.GetCard(model.AttackingCard);
        var attacked = (view as BoardView).BoardManager.GetCard(model.AttackedCard);

        var seq = DOTween.Sequence();
        if (!attacker.LastPosition.HasValue)
            attacker.LastPosition = attacker.CardViewObject.transform.position;

        seq.Append(attacker.CardViewObject.transform.DOMove(attacked.CardViewObject.transform.position, 0.3f)); //go in
        seq.OnComplete(() =>
        {
            attacker.CardViewObject.transform.DOMove(attacker.LastPosition.Value, 1.5f).SetEase(Ease.InOutQuint);
            attacker.LastPosition = null;
            attacker.CardViewObject.GetComponent<DragRotator>()?.DisableRotator();
            PhotonEngine.CompletedAction();
        });
    }
}

