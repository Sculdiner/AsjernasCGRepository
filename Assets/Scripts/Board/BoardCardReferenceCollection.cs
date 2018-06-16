using System.Collections.Generic;

public class BoardCardReferenceCollection
{
    private readonly Dictionary<int, ClientSideCard> RegisteredCards = new Dictionary<int, ClientSideCard>();

    public void RegisterCardToGame(ClientSideCard card)
    {
        RegisteredCards.Add(card.CardStats.GeneratedCardId, card);
    }

    public ClientSideCard GetCard(int uniqueCardId)
    {
        if (RegisteredCards.ContainsKey(uniqueCardId))
            return RegisteredCards[uniqueCardId];
        return null;
    }
}
