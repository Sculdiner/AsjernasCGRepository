using AsjernasCG.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : View
{
    private void Start()
    {
        Controller = new BoardController(this);
        try
        {
            MasterCardManager.LoadCards();

            var model = BoardTransitionHelper.Instance.GameInitializationModel;
            var c1p1Template = model.Player1Model.Character1;
            var c2p1Template = model.Player1Model.Character2;
            var c1p2Template = model.Player2Model.Character1;
            var c2p2Template = model.Player2Model.Character2;

            var c1p1 = MasterCardManager.GenerateCardPrefab(c1p1Template.CardTemplateId, c1p1Template.GeneratedCardId);
            var c2p1 = MasterCardManager.GenerateCardPrefab(c2p1Template.CardTemplateId, c2p1Template.GeneratedCardId);
            var c1p2 = MasterCardManager.GenerateCardPrefab(c1p2Template.CardTemplateId, c1p2Template.GeneratedCardId);
            var c2p2 = MasterCardManager.GenerateCardPrefab(c2p2Template.CardTemplateId, c2p2Template.GeneratedCardId);

            _controller.SendClientReady();
        }
        catch (System.Exception ex)
        {

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

    private BoardController _controller;
    public override IViewController Controller { get { return (IViewController)_controller; } protected set { _controller = value as BoardController; } }
    public override RoutingOperationCode GetRoutingOperationCode() { return RoutingOperationCode.Game; }

    public override void OnFriendStatusUpdate(FriendListItemViewModel friendStatusModel)
    {
        FriendListViewManager.FriendListMasterModel.FriendListContainer.UpdateFriendItem(friendStatusModel);
    }

    public Vector3 GetPositionRelativeToObject(GameObject obj)
    {
        return Camera.main.ScreenToWorldPoint(obj.transform.position);
    }

    public FriendListViewManager FriendListViewManager;
    public MasterCardManager MasterCardManager;
    public BoardManager BoardManager;
}
