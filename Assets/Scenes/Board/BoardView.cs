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


            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 1);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(1).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManager.AddCardLast(card);
            });
            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 2);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(2).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManager.AddCardLast(card);
            });

            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 3);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(3).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManager.AddCardLast(card);
            });
            //PhotonEngine.AddToQueue(() =>
            //{
            //    HandSlotManager.HandPreviewOn = true;
            //    HandSlotManager.ForceUpdatePositions();
            //});

            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 4);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(4).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManager.AddCardLast(card);
            });

            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 5);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(5).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManager.AddCardLast(card);
            });

            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 6);
                var card = BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(6).InitialTemplate, CardLocation.Hand, 1);
                HandSlotManager.AddCardToPosition(card,3);
            });
            //PhotonEngine.AddToQueue(() =>
            //{
            //    HandSlotManager.RemoveCard(3);
            //});


            //PhotonEngine.AddToQueue(() =>
            //{
            //    var cardPrefab = MasterCardManager.GenerateCardPrefab(1, 1);
            //    BoardManager.RegisterPlayerCard(cardPrefab, MasterCardManager.GetCardManager(1).InitialTemplate, CardLocation.Hand, 1);
            //    HandPlacement.DrawNewCard(BoardManager.GetCard(cardPrefab));
            //});

            //PhotonEngine.AddToQueue(() =>
            //{
            //    var cardPrefab2 = MasterCardManager.GenerateCardPrefab(1, 2);
            //    BoardManager.RegisterPlayerCard(cardPrefab2, MasterCardManager.GetCardManager(2).InitialTemplate, CardLocation.Hand, 1);
            //    HandPlacement.DrawNewCard(BoardManager.GetCard(cardPrefab2));
            //});

            //PhotonEngine.AddToQueue(() =>
            //{
            //    var cardPrefab3 = MasterCardManager.GenerateCardPrefab(1, 3);
            //    BoardManager.RegisterPlayerCard(cardPrefab3, MasterCardManager.GetCardManager(3).InitialTemplate, CardLocation.Hand, 1);
            //    HandPlacement.DrawNewCard(BoardManager.GetCard(cardPrefab3));
            //});
            //PhotonEngine.AddToQueue(() =>
            //{
            //    var cardPrefab4 = MasterCardManager.GenerateCardPrefab(1, 4);
            //    BoardManager.RegisterPlayerCard(cardPrefab4, MasterCardManager.GetCardManager(4).InitialTemplate, CardLocation.Hand, 1);
            //    HandPlacement.DrawNewCard(BoardManager.GetCard(cardPrefab4));
            //});
            //PhotonEngine.AddToQueue(() =>
            //{
            //    var cardPrefab5 = MasterCardManager.GenerateCardPrefab(1, 5);
            //    BoardManager.RegisterPlayerCard(cardPrefab5, MasterCardManager.GetCardManager(5).InitialTemplate, CardLocation.Hand, 1);
            //    HandPlacement.DrawNewCard(BoardManager.GetCard(cardPrefab5));
            //});
            //PhotonEngine.AddToQueue(() =>
            //{
            //    var cardPrefab6 = MasterCardManager.GenerateCardPrefab(1, 6);
            //    BoardManager.RegisterPlayerCard(cardPrefab6, MasterCardManager.GetCardManager(6).InitialTemplate, CardLocation.Hand, 1);
            //    HandPlacement.DrawNewCard(BoardManager.GetCard(cardPrefab6));
            //});

            //StartCoroutine(RemoveCardTest());

            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab7 = MasterCardManager.GenerateCardPrefab(1, 7);
                BoardManager.RegisterPlayerCard(cardPrefab7, MasterCardManager.GetCardManager(7).InitialTemplate, CardLocation.Hand, 1);
                EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab7));
            });
            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab8 = MasterCardManager.GenerateCardPrefab(1, 8);
                BoardManager.RegisterPlayerCard(cardPrefab8, MasterCardManager.GetCardManager(8).InitialTemplate, CardLocation.Hand, 1);
                EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab8));
            });

            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab9 = MasterCardManager.GenerateCardPrefab(1, 9);
                BoardManager.RegisterPlayerCard(cardPrefab9, MasterCardManager.GetCardManager(9).InitialTemplate, CardLocation.Hand, 1);
                EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab9));
            });
            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab10 = MasterCardManager.GenerateCardPrefab(1, 10);
                BoardManager.RegisterPlayerCard(cardPrefab10, MasterCardManager.GetCardManager(10).InitialTemplate, CardLocation.Hand, 1);
                EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab10));

            });
            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab11 = MasterCardManager.GenerateCardPrefab(1, 11);
                BoardManager.RegisterPlayerCard(cardPrefab11, MasterCardManager.GetCardManager(11).InitialTemplate, CardLocation.Hand, 1);
                EncounterSlotManager.AddEncounterCardToASlot(BoardManager.GetCard(cardPrefab11));
            });

            PhotonEngine.AddToQueue(() =>
            {
                EncounterSlotManager.RemoveEncounterCard(8);
            });

            PhotonEngine.AddToQueue(() =>
            {
                var cardPrefab12 = MasterCardManager.GenerateCardPrefab(1, 12);
                BoardManager.RegisterPlayerCard(cardPrefab12, MasterCardManager.GetCardManager(12).InitialTemplate, CardLocation.Hand, 1);
                EncounterSlotManager.AddEncounterCardToPosition(BoardManager.GetCard(cardPrefab12), 3);
            });

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

    private IEnumerator RemoveCardTest()
    {
        PhotonEngine.AddToQueue(() =>
        {
            HandPlacement.PrepareCardToPlay(2, new Vector3(0, 0, 0));
        });
        PhotonEngine.AddToQueue(() =>
        {
            HandPlacement.CardPlayed(2);
        });


        PhotonEngine.AddToQueue(() =>
        {
            HandPlacement.PrepareCardToPlay(4, new Vector3(2, 0, 0));
        });


        PhotonEngine.AddToQueue(() =>
        {
            HandPlacement.PrepareCardToPlay(5, new Vector3(3, 0, 0));
        });
        PhotonEngine.AddToQueue(() =>
        {
            HandPlacement.CardPlayed(5);
        });

        //yield return new WaitForSeconds(2);
        //PhotonEngine.AddToQueue(() =>
        //{
        //    HandPlacement.CancelCardPlay(4);
        //});

        yield return new WaitForSeconds(0.1f);
        PhotonEngine.AddToQueue(() =>
        {
            HandPlacement.CardPlayed(4);
        });

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

    public GameObject PlayArea;
    public FriendListViewManager FriendListViewManager;
    public MasterCardManager MasterCardManager;
    public BoardManager BoardManager;
    public HandVisual_Int HandPlacement;
    public HandSlotManager HandSlotManager;
    public EncounterSlotManager EncounterSlotManager;

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
