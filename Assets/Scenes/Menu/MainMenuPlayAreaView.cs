using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AsjernasCG.Common;
using AsjernasCG.Common.EventModels.General;
using AsjernasCG.Common.OperationModels;
using UnityEngine;

public class MainMenuPlayAreaView : View
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
        var groupCount = GroupManager.Instance.Group.Count;
        if (groupCount < 2)
        {
            GroupManager.Instance.ClearGroup();
            GroupManager.Instance.NewGroup(PhotonEngine.Instance.UserId, PhotonEngine.Instance.UserName);
        }

        var group = GroupManager.Instance.Group;

        var groupLeader = group.FirstOrDefault(s => s.IsGroupLeader);
        LeaderArea.LoadArea(groupLeader.UserId == PhotonEngine.Instance.UserId, true, groupLeader.UserId, groupLeader.UserName, this);
        var nonLeader = group.FirstOrDefault(s => !s.IsGroupLeader);
        if (nonLeader != null)
        {
            InviteToGroupFunctionalityArea.SetActive(false);
            TeammateArea.LoadArea(nonLeader.UserId == PhotonEngine.Instance.UserId, false, nonLeader.UserId, nonLeader.UserName, this);
        }
        var existingGroupStatus = GroupManager.Instance.LoadGroupStatus();
        if (existingGroupStatus != null)
        {
            ChangeStatus(existingGroupStatus);
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

    public void OnTeammateDeckSelectionChanged(int deckId, string deckName)
    {
        var teammateArea = FindMyTeammateArea();
        if (teammateArea == null)
            return;

        teammateArea.SelectedDeckName.text = deckName;
    }

    public void OnTeammateGameInititiationReadyStatusChanged(bool isReady)
    {
        var teammateArea = FindMyTeammateArea();
        if (teammateArea == null)
            return;

        teammateArea.ChangeGameInitiationReadyStatus(isReady);
    }

    public void UpdateUserDecks(Dictionary<int, string> deckList)
    {
        FindMyGroupArea().DeckListSelectionHelperManager.LoadDeckListArea(deckList);
    }

    public void SendGroupInvitationRequest(int userId)
    {
        _controller.SendGroupInviteRequest(userId);
        GroupManager.Instance.ClearGroup();
        GroupManager.Instance.NewGroup(userId, PhotonEngine.Instance.UserName);
    }

    public void OnInvited(int groupId, string username)
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
            InvitationManager.InitializeInvitation(groupId, username);
        }
    }

    public GroupAreaViewManager FindMyGroupArea()
    {
        if (LeaderArea.areasUserId == PhotonEngine.Instance.UserId)
        {
            return LeaderArea;
        }
        else
        {
            return TeammateArea;
        }
    }

    public GroupAreaViewManager FindMyTeammateArea()
    {
        if (LeaderArea.areasUserId == PhotonEngine.Instance.UserId)
        {
            return TeammateArea;
        }
        else
        {
            return LeaderArea;
        }
    }

    public void StartGame()
    {
        if (LeaderArea.PlayerReady && TeammateArea.PlayerReady)
        {
            //_controller.Send
        }
    }

    public void ChangeStatus(GroupStatusModel model)
    {
        if (model.GroupLeaderDeckId.HasValue)
        {
            LeaderArea.ChangeSelectedDeck(model.GroupLeaderDeckId.Value, model.GroupLeaderDeckName);
            LeaderArea.ChangeGameInitiationReadyStatus(model.GroupLeaderReady);
        }
        if (model.TeammateDeckId.HasValue)
        {
            TeammateArea.ChangeSelectedDeck(model.TeammateDeckId.Value, model.TeammateDeckName);
            TeammateArea.ChangeGameInitiationReadyStatus(model.TeammateReady);
        }
    }

    public void OnLeaderRefreshView()
    {
        var storedStatus = GroupManager.Instance.LoadGroupStatus();
        var tempLeaderStatus = new AsjernasCG.Common.EventModels.General.GroupStatusModel()
        {
            GroupLeaderConnectionStatus = true,
            GroupLeaderDeckId = storedStatus.GroupLeaderDeckId,
            GroupLeaderDeckName = storedStatus.GroupLeaderDeckName,
            GroupLeaderId = storedStatus.GroupLeaderId,
            GroupLeaderReady = storedStatus.GroupLeaderReady
        };

        GroupManager.Instance.ClearGroup();
        GroupManager.Instance.NewGroup(PhotonEngine.Instance.UserId, PhotonEngine.Instance.UserName);
        GroupManager.Instance.StoreGroupStatus(tempLeaderStatus);
        FindMyTeammateArea().gameObject.SetActive(false);
        //ChangeScene("PlayMenu");
    }

    public FriendListViewManager FriendListViewManager;
    public MasterCardManager MasterCardManager;
    public InviteListManager InviteListManager;
    public InvitationManager InvitationManager;
    public GroupAreaViewManager LeaderArea;
    public GroupAreaViewManager TeammateArea;
    public GameObject InviteToGroupFunctionalityArea;
}
