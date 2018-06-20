using AsjernasCG.Common;
using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.EventModels.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : View
{
    private void Start()
    {
        Controller = new BoardController(this);
        try
        {
            MasterCardManager.LoadCards();

            //var model = BoardTransitionHelper.Instance.GameInitializationModel;
            BoardManager.RegisterPlayer(1);
            BoardManager.RegisterPlayer(2);

            //RegisterStartingCharacter(model.Player1Model.PlayerId, model.Player1Model.Character1);
            //RegisterStartingCharacter(model.Player1Model.PlayerId, model.Player1Model.Character2);
            //RegisterStartingCharacter(model.Player2Model.PlayerId, model.Player2Model.Character1);
            //RegisterStartingCharacter(model.Player2Model.PlayerId, model.Player2Model.Character2);
            var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 1);
            BoardManager.RegisterPlayerCard(cardPrefab, 1, CardLocation.Hand, 1);
            HandPlacement.GivePlayerACard(BoardManager.GetCard(cardPrefab), false);
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

    private void Awake()
    {
       

    }
    private void RegisterStartingCharacter(int userId, DetailedCardModel character)
    {
        var cardTemplate = MasterCardManager.GetNewCardInstance(character.CardTemplateId);
        var obj = MasterCardManager.GenerateCardPrefab(cardTemplate, character.GeneratedCardId);
        BoardManager.RegisterPlayerCard(obj, cardTemplate, CardLocation.PlayArea, userId);
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
    public HandVisual_Int HandPlacement;

    //public Button AttackButton { get; set; }
    //public Button QuestButton { get; set; }
    //public Button PassButton { get; set; }
    //public Button PlayCard_Field_Button { get; set; }
    //public Button PlayCard_Targeted_Button { get; set; }
    //public Button PlayCard_Attach_Button { get; set; }
    //public Button PlayCard_AttachWithTarget_Button { get; set; }

    //public int SourceCardId { get; set; }
    //public int TargetCardId { get; set; }
    //public int AttachOnId { get; set; }

    //public void AssignButtonEvents()
    //{
    //    AttackButton.onClick
    //        .AddListener(() => { _controller.Attack(SourceCardId, TargetCardId); });
    //    QuestButton.onClick
    //        .AddListener(() => { _controller.Quest(SourceCardId); });
    //    PassButton.onClick
    //        .AddListener(() => { _controller.Pass(); });
    //    PlayCard_Field_Button.onClick
    //        .AddListener(() => { _controller.Play_CardWithoutTarget(SourceCardId); });
    //    PlayCard_Targeted_Button.onClick
    //        .AddListener(() => { _controller.Play_CardWithTarget(SourceCardId, TargetCardId); });
    //    PlayCard_Attach_Button.onClick
    //        .AddListener(() => { _controller.Play_AttachmentCardWithoutTarget(SourceCardId, AttachOnId); });
    //    PlayCard_AttachWithTarget_Button.onClick
    //        .AddListener(() => { _controller.Play_AttachmentCardWithTarget(SourceCardId, AttachOnId, TargetCardId); });
    //}
}
