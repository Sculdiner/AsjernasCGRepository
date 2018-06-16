using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsjernasCG.Common.ClientEventCodes;
using AsjernasCG.Common.EventModels.Game;
public class GameInitializationEventHandler<TModel> : BaseEventHandler<TModel> where TModel : GameInitializeModel
{
    public override byte EventCode
    {
        get
        {
            return (byte)ClientGameEventCode.InitializeGame;
        }
    }

    public override void OnHandleEvent(View view, TModel model)
    {
        BoardTransitionHelper.InitializeInstance();
        BoardTransitionHelper.Instance.StoreGameInformation(model);
        view.ChangeScene("LazGameScene");
    }
}