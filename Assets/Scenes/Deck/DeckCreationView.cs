using System.Collections;
using System.Collections.Generic;
using AsjernasCG.Common;
using UnityEngine;

public class DeckCreationView : View
{
    private DeckCreationController _controller;
    public override IViewController Controller { get { return (IViewController)_controller; } protected set { _controller = value as DeckCreationController; } }
    public override RoutingOperationCode GetRoutingOperationCode() { return RoutingOperationCode.Menu; }

    public FriendListViewManager FriendListViewManager;
    public MasterCardManager MasterCardManager;
    public CardCollectionPageViewManager CardCollectionPageViewManager;

    private void Start()
    {
        Controller = new DeckCreationController(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnFriendStatusUpdate(FriendListItemViewModel friendStatusModel)
    {
        FriendListViewManager.FriendListMasterModel.FriendListContainer.UpdateFriendItem(friendStatusModel);
    }
}