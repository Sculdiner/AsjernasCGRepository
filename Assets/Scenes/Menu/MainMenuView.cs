﻿using System.Collections;
using System.Collections.Generic;
using AsjernasCG.Common;
using AsjernasCG.Common.OperationModels;
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
        if (FriendListViewManager != null)
        {
            RequestFriendListUpdate();
        }
        //MasterCardManager.GenerateCardPrefab(0, 9870);
        //MasterCardManager.GenerateCardPrefab(0, 9871);
        //MasterCardManager.GenerateCardPrefab(5, 9872);
        //MasterCardManager.GenerateCardPrefab(1, 9873);
        //FriendListViewManager.AddFriendItem(new AsjernasCG.Common.EventModels.FriendStatusModel()
        //{
        //    UserName = "asdasd",
        //    UserStatus = AsjernasCG.Common.BusinessModels.UserStatusModel.Connected
        //});
        GroupManager.InitializeManager();
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
        if (InviteListManager != null)
        {
            _controller.GetFriendListForInvite();
        }
    }

    public void UpdateInviteList(Dictionary<int, string> inviteList)
    {
        if (InviteListManager != null)
        {
            InviteListManager.Container.Clear();
            foreach (var key in inviteList.Keys)
            {
                InviteListManager.Container.Add(key, inviteList[key], SendGroupInvitationRequest);
            }
        }
    }

    public void SendGroupInvitationRequest(int userId)
    {
        _controller.SendGroupInviteRequest(userId);
    }

    public void OnInvited(int groupLeaderId, string username)
    {
        if (InvitationManager != null)
        {
            InvitationManager.OnGroupAccept = (u) =>
            {
                _controller.SendAcceptGroupInvitation(u, username);
            };
            InvitationManager.OnGroupDecline = (u) =>
            {
                _controller.SendDeclineGroupInvitation(u);
            };
            InvitationManager.InitializeInvitation(groupLeaderId, username);
        }
    }

    public void GoToPlayScene()
    {
        ChangeScene("PlayMenu");
    }

    public FriendListViewManager FriendListViewManager;
    public MasterCardManager MasterCardManager;
    public InviteListManager InviteListManager;
    public InvitationManager InvitationManager;
}
