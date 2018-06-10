using AsjernasCG.Common.OperationModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : ViewController
{

    public MainMenuView _view;

    public MainMenuController(View controlledView) : base(controlledView)
    {
        _view = controlledView as MainMenuView;
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

    public void SendDeclineGroupInvitation(string groupId)
    {
        var model = new GroupRequestReplyOperationModel()
        {
            AcceptGroup = false,
            RequestingUserGroupId = groupId
        };
        var helper = new AsjernasCG.Common.OperationHelpers.General.GroupRequestReplyOperationHelper<GroupRequestReplyOperationModel>(model);
        SendOperation(helper, true, 0, false);
    }

    public void SendAcceptGroupInvitation(string groupId)
    {
        var model = new GroupRequestReplyOperationModel()
        {
            AcceptGroup = true,
            RequestingUserGroupId = groupId
        };
        var helper = new AsjernasCG.Common.OperationHelpers.General.GroupRequestReplyOperationHelper<GroupRequestReplyOperationModel>(model);
        SendOperation(helper, true, 0, false);
    }
}
