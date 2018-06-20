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
        var cardPrefab =  (view as BoardView).MasterCardManager.GenerateCardPrefab(model.CardTemplateId, model.GeneratedCardId);
        (view as BoardView).BoardManager.RegisterPlayerCard(cardPrefab, model.CardTemplateId, AsjernasCG.Common.BusinessModels.CardModels.CardLocation.Hand, model.OwnerId);
        //cardPrefab


        view.LogInfo("Draw Card. id:" + model.GeneratedCardId);
        view.MessageBoxManager.ShowMessage("Draw Card. id:" + model.GeneratedCardId);
    }
}