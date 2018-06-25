using AsjernasCG.Common;
using AsjernasCG.Common.BusinessModels.CardModels;
using AsjernasCG.Common.EventModels.Game;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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


            PhotonEngine.AddToQueue("CardDraw", () =>
             {
                 var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 1);
                 var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(1).InitialTemplate, CardLocation.Hand, 1);
                 HandSlotManagerV2.AddCardLast(card);
             });
            PhotonEngine.AddToQueue("CardDraw", () =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 2);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(2).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManagerV2.AddCardLast(card);
            });

            PhotonEngine.AddToQueue("CardDraw", () =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 3);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(3).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManagerV2.AddCardLast(card);
            });

            PhotonEngine.AddToQueue("CardDraw", () =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 4);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(4).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManagerV2.AddCardLast(card);
            });

            PhotonEngine.AddToQueue("CardDraw", () =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 5);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(5).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManagerV2.AddCardLast(card);
            });
            //PhotonEngine.AddToQueue("CardDraw", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 905);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(905).InitialTemplate, CardLocation.Hand, 1);
            //    HandSlotManager.AddCardLast(card);
            //});
            //PhotonEngine.AddToQueue("CardDraw", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 906);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(906).InitialTemplate, CardLocation.Hand, 1);
            //    HandSlotManager.AddCardLast(card);
            //});
            //PhotonEngine.AddToQueue("RemoveCard", () =>
            //{
            //    HandSlotManager.RemoveCard(3);
            //});
            //PhotonEngine.AddToQueue("CardDrawToPosition", () =>
            // {
            //     var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 6);
            //     var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(6).InitialTemplate, CardLocation.Hand, 1);
            //     HandSlotManager.AddCardToPosition(card, 3);
            // });


            ////Left ally
            //PhotonEngine.AddToQueue("AllyPlay", () =>
            // {
            //     var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 7);
            //     var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(7).InitialTemplate, CardLocation.Hand, 1);
            //     LeftPlayerAllySlotManager.AddAllyCardLast(card);
            // });
            //PhotonEngine.AddToQueue("AllyPlay", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 8);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(8).InitialTemplate, CardLocation.Hand, 1);
            //    LeftPlayerAllySlotManager.AddAllyCardLast(card);
            //});
            //PhotonEngine.AddToQueue("AllyPlay", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 9);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(9).InitialTemplate, CardLocation.Hand, 1);
            //    LeftPlayerAllySlotManager.AddAllyCardLast(card);
            //});
            //PhotonEngine.AddToQueue("AllyPlay", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 10);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(10).InitialTemplate, CardLocation.Hand, 1);
            //    LeftPlayerAllySlotManager.AddAllyCardLast(card);
            //});
            //PhotonEngine.AddToQueue("AllyRemove", () =>
            //{
            //    LeftPlayerAllySlotManager.RemoveAllyCard(8);
            //});
            //PhotonEngine.AddToQueue("AllyPlayAtPosition", () =>
            // {
            //     var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 11);
            //     var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(11).InitialTemplate, CardLocation.Hand, 1);
            //     LeftPlayerAllySlotManager.AddAllyCardToPosition(card, 1);
            // });

            //////Right ally
            //PhotonEngine.AddToQueue("AllyPlay", () =>
            // {
            //     var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 12);
            //     var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(12).InitialTemplate, CardLocation.Hand, 1);
            //     RightPlayerAllySlotManager.AddAllyCardLast(card);
            // });
            //PhotonEngine.AddToQueue("AllyPlay", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 13);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(13).InitialTemplate, CardLocation.Hand, 1);
            //    RightPlayerAllySlotManager.AddAllyCardLast(card);
            //});
            //PhotonEngine.AddToQueue("AllyPlay", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 14);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(14).InitialTemplate, CardLocation.Hand, 1);
            //    RightPlayerAllySlotManager.AddAllyCardLast(card);
            //});
            //PhotonEngine.AddToQueue("AllyPlay", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 15);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(15).InitialTemplate, CardLocation.Hand, 1);
            //    RightPlayerAllySlotManager.AddAllyCardLast(card);
            //});
            //PhotonEngine.AddToQueue("AllyRemove", () =>
            //{
            //    RightPlayerAllySlotManager.RemoveAllyCard(14);
            //});
            //PhotonEngine.AddToQueue("AllyPlayToPosition", () =>
            // {
            //     var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 16);
            //     var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(16).InitialTemplate, CardLocation.Hand, 1);
            //     RightPlayerAllySlotManager.AddAllyCardToPosition(card, 1);
            // });

            //PhotonEngine.AddToQueue("EncounterCard", () =>
            //{
            //    var cardPrefab7 = MasterCardManager.GenerateCardPrefab(1, 107);
            //    BoardManager.RegisterPlayerCard(cardPrefab7, MasterCardManager.GetCardManager(107).InitialTemplate, CardLocation.Hand, 1);
            //    EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab7));
            //});
            //PhotonEngine.AddToQueue("EncounterCard", () =>
            //{
            //    var cardPrefab8 = MasterCardManager.GenerateCardPrefab(1, 108);
            //    BoardManager.RegisterPlayerCard(cardPrefab8, MasterCardManager.GetCardManager(108).InitialTemplate, CardLocation.Hand, 1);
            //    EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab8));
            //});

            //PhotonEngine.AddToQueue("EncounterCard", () =>
            //{
            //    var cardPrefab9 = MasterCardManager.GenerateCardPrefab(1, 109);
            //    BoardManager.RegisterPlayerCard(cardPrefab9, MasterCardManager.GetCardManager(109).InitialTemplate, CardLocation.Hand, 1);
            //    EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab9));
            //});
            //PhotonEngine.AddToQueue("EncounterCard", () =>
            //{
            //    var cardPrefab10 = MasterCardManager.GenerateCardPrefab(1, 110);
            //    BoardManager.RegisterPlayerCard(cardPrefab10, MasterCardManager.GetCardManager(110).InitialTemplate, CardLocation.Hand, 1);
            //    EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab10));

            //});
            //PhotonEngine.AddToQueue("EncounterCard", () =>
            //{
            //    var cardPrefab11 = MasterCardManager.GenerateCardPrefab(1, 111);
            //    BoardManager.RegisterPlayerCard(cardPrefab11, MasterCardManager.GetCardManager(111).InitialTemplate, CardLocation.Hand, 1);
            //    EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab11));
            //});

            //PhotonEngine.AddToQueue("RemoveEncounterCard", () =>
            //{
            //    EncounterSlotManager.RemoveEncounterCard(108);
            //});

            //PhotonEngine.AddToQueue("EncounterCardToPosition", () =>
            //{
            //    var cardPrefab12 = MasterCardManager.GenerateCardPrefab(1, 112);
            //    BoardManager.RegisterPlayerCard(cardPrefab12, MasterCardManager.GetCardManager(112).InitialTemplate, CardLocation.Hand, 1);
            //    EncounterSlotManager.AddEncounterCardToPosition(BoardManager.GetCard(cardPrefab12), 3);
            //});

            //PhotonEngine.AddToQueue("CardDraw", () =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 500);
            //    var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(500).InitialTemplate, CardLocation.Hand, 1);
            //    HandSlotManager.AddCardLast(card);
            //});

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

    //private IEnumerator RemoveCardTest()
    //{
    //    PhotonEngine.AddToQueue(() =>
    //    {
    //        HandPlacement.PrepareCardToPlay(2, new Vector3(0, 0, 0));
    //    });
    //    PhotonEngine.AddToQueue(() =>
    //    {
    //        HandPlacement.CardPlayed(2);
    //    });


    //    PhotonEngine.AddToQueue(() =>
    //    {
    //        HandPlacement.PrepareCardToPlay(4, new Vector3(2, 0, 0));
    //    });


    //    PhotonEngine.AddToQueue(() =>
    //    {
    //        HandPlacement.PrepareCardToPlay(5, new Vector3(3, 0, 0));
    //    });
    //    PhotonEngine.AddToQueue(() =>
    //    {
    //        HandPlacement.CardPlayed(5);
    //    });

    //    //yield return new WaitForSeconds(2);
    //    //PhotonEngine.AddToQueue(() =>
    //    //{
    //    //    HandPlacement.CancelCardPlay(4);
    //    //});

    //    yield return new WaitForSeconds(0.1f);
    //    PhotonEngine.AddToQueue(() =>
    //    {
    //        HandPlacement.CardPlayed(4);
    //    });

    //}

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

    public GameObject PlayArea;
    public FriendListViewManager FriendListViewManager;
    public MasterCardManager MasterCardManager;
    public BoardManager BoardManager;
    public HandVisual_Int HandPlacement;
    public SimpleHandSlotManager HandSlotManager;
    public SimpleHandSlotManagerV2 HandSlotManagerV2;
    public EncounterSlotManager EncounterSlotManager;
    public AllySlotManager LeftPlayerAllySlotManager;
    public AllySlotManager RightPlayerAllySlotManager;

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
