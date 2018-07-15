using HighlightingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class VisualStateManager : MonoBehaviour
{
    public Material CardImage;
    public CardManager ControllingCardManager;
    public CardVisualComponent Card;
    public CardVisualComponent Preview;
    public CardVisualComponent Follower;
    public CardVisualComponent Equipment;
    public CardVisualComponent Ability;
    public CardVisualComponent Character;
    public CardVisualComponent Quest;
    public ParticleSystem DissolvePlayParticleSystem;
    public ParticleSystem SmokePlayParticleSystem;

    private CardVisualComponent _state;
    public CardVisualComponent CurrentState { get { return _state; } }

    private bool AllowPreviewing;

    public void Awake()
    {
        //if (ControllingCardManager.Template.CardType == AsjernasCG.Common.BusinessModels.CardModels.CardType.Quest)
        //    _state = Quest;
        //else
        _state = Card;
        AllowPreviewing = true;
    }


    public void DeactivatePreview()
    {
        Preview?.Hide();
        AllowPreviewing = false;
    }
    public void AllowPreview()
    {
        AllowPreviewing = true;
    }


    public CardVisualComponent PreviewAndRetainOriginalState()
    {
        if (AllowPreviewing)
        {
            Preview.UpdateVisual(ControllingCardManager.Template);
            Preview.Show();
        }
        return Preview;
    }

    public CardVisualComponent EndPreviewAndRetainOriginalState()
    {
        Preview.Hide();
        return Preview;
    }

    public void Hightlight()
    {
        var highlighter = this.ControllingCardManager.GetComponent<Highlighter>();
        if (highlighter != null)
        {
            highlighter.tween = true;
        }

    }

    public void EndHighlight()
    {
        var highlighter = this.ControllingCardManager.GetComponent<Highlighter>();
        if (highlighter != null)
            highlighter.tween = false;
    }

    public void ChangeVisual(CardVisualState newState)
    {
        ////Debug.Log($"Switching visual to: {newState.ToString()}");
        switch (newState)
        {
            case CardVisualState.None:
                Preview?.Hide();
                Follower?.Hide();
                Equipment?.Hide();
                Ability?.Hide();
                Character?.Hide();
                Card?.Hide();
                Quest?.Hide();
                AllowPreviewing = false;
                _state = Card;
                break;
            case CardVisualState.Card:
                Preview?.Hide();
                Follower?.Hide();
                Equipment?.Hide();
                Ability?.Hide();
                Character?.Hide();
                Card.Show();
                Quest?.Hide();
                AllowPreviewing = true;
                _state = Card;
                break;
            case CardVisualState.Preview:
                if (AllowPreviewing)
                {
                    Card?.Hide();
                    Preview.Show();
                    Follower?.Hide();
                    Equipment?.Hide();
                    Ability?.Hide();
                    Character?.Hide();
                    Quest?.Hide();
                    _state = Preview;
                }
                break;
            case CardVisualState.Follower:
                Card?.Hide();
                Preview?.Hide();
                Follower.Show();
                Equipment?.Hide();
                Ability?.Hide();
                Character?.Hide();
                Quest?.Hide();
                AllowPreviewing = true;
                _state = Follower;
                break;
            case CardVisualState.Ability:
                Card?.Hide();
                Preview?.Hide();
                Follower?.Hide();
                Equipment?.Hide();
                Ability.Show();
                Character?.Hide();
                Quest?.Hide();
                AllowPreviewing = true;
                _state = Ability;
                break;
            case CardVisualState.Equipment:
                Card?.Hide();
                Preview?.Hide();
                Follower?.Hide();
                Equipment.Show();
                Ability?.Hide();
                Character?.Hide();
                Quest?.Hide();
                AllowPreviewing = true;
                _state = Equipment;
                break;
            case CardVisualState.Character:
                Card?.Hide();
                Preview?.Hide();
                Follower?.Hide();
                Equipment?.Hide();
                Ability?.Hide();
                Character.Show();
                Quest?.Hide();
                AllowPreviewing = true;
                _state = Character;
                break;
            case CardVisualState.Quest:
                Card?.Hide();
                Preview?.Hide();
                Follower?.Hide();
                Equipment?.Hide();
                Ability?.Hide();
                Character?.Hide();
                Quest.Show();
                AllowPreviewing = true;
                _state = Quest;
                break;
            default:
                throw new Exception();
        }
        CurrentState.UpdateVisual(ControllingCardManager.Template);
    }
}
