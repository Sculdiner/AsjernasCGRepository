using AsjernasCG.Common.BusinessModels.CardModels;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public MasterCardManager MasterCardManager;
    private BoardCardReferenceCollection CardReferenceCollection;

    public BoardManager()
    {
        CardReferenceCollection = new BoardCardReferenceCollection();
    }

    public void RegisterCard(BaseCardTemplate card, ClientSideParticipatorState participatorState)
    {
        CardReferenceCollection.RegisterCardToGame(new ClientSideCard()
        {
            CardStats = card,
            ParticipatorState = participatorState
        });
    }

    public ClientSideCard GetCard(int cardId)
    {
        return CardReferenceCollection.GetCard(cardId);
    }
}
