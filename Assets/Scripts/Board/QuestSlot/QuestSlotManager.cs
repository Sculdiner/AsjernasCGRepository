using System;
using AsjernasCG.Common.BusinessModels.CardModels;
using UnityEngine;

public class QuestSlotManager : PositionalSlotManager
{
    public ClientSideCard CurrentQuest { get; private set; }
    public Transform QuestPosition;
    public void ChangeQuest(ClientSideCard clientSideCard)
    {
        if (CurrentQuest!=null)
            RemoveSlot(CurrentQuest.CardStats.GeneratedCardId);

        CurrentQuest = clientSideCard;
        clientSideCard.CardManager.SlotManager = this;
        clientSideCard.SetLocation(CardLocation.PlayArea);
        clientSideCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Quest);

        UpdatePosition();
        PhotonEngine.CompletedAction();
    }

    private void UpdatePosition()
    {
        CurrentQuest.CardViewObject.transform.position = QuestPosition.position;
        CurrentQuest.CardViewObject.transform.rotation = QuestPosition.rotation;
        CurrentQuest.CardViewObject.transform.localScale = QuestPosition.localScale;
    }

    //public QuestManager CurrentQuestingManager { get; set; }
    //public BoardManager BoardManager;
    //public void SetQuest(QuestManager quest)
    //{
    //    CurrentQuestingManager = quest;
    //    //CurrentQuestingManager = quest;
    //}

    public void ProgressQuest(int points)
    {
        CurrentQuest.CardStats.CurrentQuestPoints = CurrentQuest.CardStats.CurrentQuestPoints.Value + points;
        CurrentQuest.CardManager.VisualStateManager.CurrentState.UpdateVisual(CurrentQuest.CardStats);
        //visual
        PhotonEngine.CompletedAction();
    }


    public override void RemoveSlot(int cardId)
    {
        GameObject.Destroy(CurrentQuest.CardViewObject);
    }
}