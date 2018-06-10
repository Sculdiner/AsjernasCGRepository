using AsjernasCG.Common.OperationModels;
using AsjernasCG.Common.OperationModels.BasicModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : ViewController
{


    public MainMenuController(View controlledView) : base(controlledView)
    {
    }

    public void SendGroupInviteRequest(int userId)
    {
        
        var model = new PlayerInteractionOperationModel()
        {
            UserId = userId
        };
        var helper = new AsjernasCG.Common.OperationHelpers.General.GroupRequestOperationHelper<PlayerInteractionOperationModel>(model);
        SendOperation(helper, true, 0, false);
    }

    public void GetFriendListForInvite()
    {
        var helper = new AsjernasCG.Common.OperationHelpers.General.GetFriendsAvailableForInviteOperationHelper<StringModel>(new StringModel());
        SendOperation(helper, true, 0, false);
    }

    public void SendDeclineGroupInvitation(int groupLeaderId)
    {
        var model = new GroupRequestReplyOperationModel()
        {
            AcceptGroup = false,
            RequestingUserGroupId = groupLeaderId
        };
        var helper = new AsjernasCG.Common.OperationHelpers.General.GroupRequestReplyOperationHelper<GroupRequestReplyOperationModel>(model);
        SendOperation(helper, true, 0, false);
    }

    public void SendAcceptGroupInvitation(int groupLeaderId, string username)
    {
        var model = new GroupRequestReplyOperationModel()
        {
            AcceptGroup = true,
            RequestingUserGroupId = groupLeaderId
        };
        var helper = new AsjernasCG.Common.OperationHelpers.General.GroupRequestReplyOperationHelper<GroupRequestReplyOperationModel>(model);
        SendOperation(helper, true, 0, false);


        //redirect to play area scene
        GroupManager.Instance.ClearGroup();
        GroupManager.Instance.NewGroup(groupLeaderId, username);
        GroupManager.Instance.AddNonLeaderPlayer(PhotonEngine.Instance.UserId, PhotonEngine.Instance.UserName);

        this.ControlledView.ChangeScene("PlayMenu");
    }
}
