using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class VisualStateManager : MonoBehaviour
{
    public Material CardImage;

    public CardVisualComponent Card;
    public CardVisualComponent Preview;
    public CardVisualComponent Follower;
    public CardVisualComponent Equipment;
    public CardVisualComponent Ability;
    public CardVisualComponent Character;

    private CardVisualComponent _state;
    public CardVisualComponent State { get { return _state; } }

    public void Awake()
    {
        _state = Card;
    }

    public CardVisualComponent PreviewAndRetainOriginalState()
    {
        Preview.Show();
        return Preview;
    }

    public CardVisualComponent EndPreviewAndRetainOriginalState()
    {
        Preview.Hide();
        return Preview;
    }


    public CardVisualComponent ChangeVisual(CardVisualState newState)
    {
        switch (newState)
        {
            case CardVisualState.Card:
                Preview.Hide();
                Follower.Hide();
                Equipment.Hide();
                Ability.Hide();
                Character.Hide();
                Card.Show();
                _state = Card;
                return Card;
            case CardVisualState.Preview:
                Card.Hide();
                Preview.Show();
                Follower.Hide();
                Equipment.Hide();
                Ability.Hide();
                Character.Hide();
                _state = Preview;
                return Preview;
            case CardVisualState.Follower:
                Card.Hide();
                Preview.Hide();
                Follower.Show();
                Equipment.Hide();
                Ability.Hide();
                Character.Hide();
                _state = Follower;
                return Follower;
            case CardVisualState.Ability:
                Card.Hide();
                Preview.Hide();
                Follower.Hide();
                Equipment.Hide();
                Ability.Show();
                Character.Hide();
                _state = Ability;
                return Ability;
            case CardVisualState.Equipment:
                Card.Hide();
                Preview.Hide();
                Follower.Hide();
                Equipment.Show();
                Ability.Hide();
                Character.Hide();
                _state = Equipment;
                return Equipment;
            case CardVisualState.Character:
                Card.Hide();
                Preview.Hide();
                Follower.Hide();
                Equipment.Hide();
                Ability.Hide();
                Character.Show();
                _state = Character;
                return Character;
            default:
                throw new Exception();
        }
    }
}
