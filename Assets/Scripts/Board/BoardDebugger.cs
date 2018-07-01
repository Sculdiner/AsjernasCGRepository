using AsjernasCG.Common.BusinessModels.CardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class BoardDebugger : MonoBehaviour
{
    public BoardManager BoardManager;
    public MasterCardManager MasterCardManager;
    public EncounterSlotManager EncounterSlotManager;
    public HandVisual_Int HandSlotManager;
    public View ControlledView;

    private int prefabgeneratedid = 1000;

    private int GetNewPrefabId()
    {
        prefabgeneratedid--;
        return prefabgeneratedid;
    }


    public ClientSideCard GenerateCard()
    {
        var id = GetNewPrefabId();
        var prefab = MasterCardManager.GenerateCardPrefab(1, id);
        return BoardManager.RegisterPlayerCard(prefab, MasterCardManager.GetCardManager(7).Template, CardLocation.Hand, 1);
    }

    //public void EncounterCard()
    //{
    //    PhotonEngine.AddToQueue(() =>
    //    {
    //        EncounterSlotManager.AddEncounterCardToASlot(GenerateCard());
    //    });
    //}

    //public void DrawCard()
    //{
    //    PhotonEngine.AddToQueue(() =>
    //    {
    //        HandSlotManager.DrawNewCard(GenerateCard());
    //    });
    //}
}
