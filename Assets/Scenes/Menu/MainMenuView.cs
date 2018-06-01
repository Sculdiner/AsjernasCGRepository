using System.Collections;
using System.Collections.Generic;
using AsjernasCG.Common;
using UnityEngine;

public class MainMenuView : View
{
    private int timer = 0;
    private void Start()
    {
        Controller = new MainMenuController(this);
        try
        {
            MasterCardManager.LoadCards();
        }
        catch (System.Exception ex)
        {
            
        }
        MasterCardManager.GenerateCardPrefab(0, 9870);
        MasterCardManager.GenerateCardPrefab(0, 9871);
        MasterCardManager.GenerateCardPrefab(5, 9872);
        MasterCardManager.GenerateCardPrefab(1, 9873);
        //FriendListViewManager.AddFriendItem(new AsjernasCG.Common.EventModels.FriendStatusModel()
        //{
        //    UserName = "asdasd",
        //    UserStatus = AsjernasCG.Common.BusinessModels.UserStatusModel.Connected
        //});
    }

    void Update()
    {
    }

    private MainMenuController _controller;
    public override IViewController Controller { get { return (IViewController)_controller; } protected set { _controller = value as MainMenuController; } }
    public override RoutingOperationCode GetRoutingOperationCode() { return RoutingOperationCode.Menu; }

    public override void OnFriendStatusUpdate(FriendListItemViewModel friendStatusModel)
    {
        FriendListViewManager.FriendListMasterModel.FriendListContainer.UpdateFriendItem(friendStatusModel);
    }

    public FriendListViewManager FriendListViewManager;
    public MasterCardManager MasterCardManager;
}
