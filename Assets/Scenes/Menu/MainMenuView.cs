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

    }

    void Update()
    {
        //timer++;
        //if (timer==100 || timer == 200 || timer == 300 || timer == 400)
        //{
        //    FriendListViewManager.AddFriendItem();
        //}
    }

    private MainMenuController _controller;
    public override IViewController Controller { get { return (IViewController)_controller; } protected set { _controller = value as MainMenuController; } }
    public override RoutingOperationCode GetRoutingOperationCode() { return RoutingOperationCode.Menu; }

    public FriendListViewManager FriendListViewManager;
}
