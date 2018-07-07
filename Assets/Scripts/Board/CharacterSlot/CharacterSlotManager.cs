using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterSlotManager : MonoBehaviour
{
    public CharacterManager Player1Character1Manager;
    public CharacterManager Player1Character2Manager;
    public CharacterManager Player2Character1Manager;
    public CharacterManager Player2Character2Manager;
    public Transform EquipmentInitialPosition;

    public CharacterManager InitializeCharacter(ClientSideCard card)
    {
        if (Player1Character1Manager.CardManager == null)
        {
            Player1Character1Manager.ClientSideCard = card;
            Player1Character1Manager.CardManager = card.CardManager;
            SetCharacterPosition(Player1Character1Manager);
            return Player1Character1Manager;
        }
        else if (Player1Character2Manager.CardManager == null)
        {
            Player1Character2Manager.ClientSideCard = card;
            Player1Character2Manager.CardManager = card.CardManager;
            SetCharacterPosition(Player1Character2Manager);
            return Player1Character2Manager;
        }
        else if (Player2Character1Manager.CardManager == null)
        {
            Player2Character1Manager.ClientSideCard = card;
            Player2Character1Manager.CardManager = card.CardManager;
            SetCharacterPosition(Player2Character1Manager);
            return Player2Character1Manager;
        }
        else if (Player2Character2Manager.CardManager == null)
        {
            Player2Character2Manager.ClientSideCard = card;
            Player2Character2Manager.CardManager = card.CardManager;
            SetCharacterPosition(Player2Character2Manager);
            return Player2Character2Manager;
        }
        else
        {
            throw new Exception();
        }
    }

    public void SetCharacterPosition(CharacterManager character)
    {
        character.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Character);
        character.CardManager.transform.position = character.transform.position;
    }
}
