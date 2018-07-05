using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
using AsjernasCG.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DrawCardDetailedEventHandler<TModel> : BaseEventHandler<TModel> where TModel : DetailedCardModel
{
    public DrawCardDetailedEventHandler()
    {
        ActionSyncType = UIActionSynchronizationType.CallbackSync;
    }

    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.DrawCardVisible;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        //PhotonEngine.AddToQueue("CardDraw", () =>
        //{
            var boardView = (view as BoardView);
            var cardPrefab = boardView.MasterCardManager.GenerateCardPrefab(model.CardTemplateId, model.GeneratedCardId);
            var ccc = boardView.BoardManager.RegisterPlayerCard(cardPrefab, cardPrefab.GetComponent<CardManager>().Template, AsjernasCG.Common.BusinessModels.CardModels.CardLocation.Hand, model.OwnerId);
            if (PhotonEngine.UserId == model.OwnerId)
            {
                boardView.HandSlotManagerV2.AddCardLast(ccc);
            }
        //});
        //view.LogInfo("Draw Card. id:" + model.GeneratedCardId);
        //view.MessageBoxManager.ShowMessage("Draw Card. id:" + model.GeneratedCardId);
    }
}