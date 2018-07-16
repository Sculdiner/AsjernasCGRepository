using HighlightingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeSlot : MonoBehaviour
{
    public RawImage Image;
    public Highlighter Highlighter;
    private bool BoardCardWasAlreadyHighlighted;
    public Gradient CardHighlightColor { get; set; }
    public ClientSideCard ReferencedCard { get; private set; }

    public void SetCardInfo(ClientSideCard card)
    {
        ReferencedCard = card;
        Image.texture = Resources.Load($"Images/{card.CardManager.Template.ImagePath}") as Texture2D;
    }

    public void OnMouseEnter()
    {
        if (ReferencedCard != null)
        {
            BoardCardWasAlreadyHighlighted = ReferencedCard.CardManager.VisualStateManager.IsHighlighted;
            ReferencedCard.CardManager.VisualStateManager.Hightlight(CardHighlightColor);
        }
    }

    public void OnMouseExit()
    {
        if (ReferencedCard != null)
        {
            if (BoardCardWasAlreadyHighlighted)
            {
                BoardCardWasAlreadyHighlighted = false;
                ReferencedCard.CardManager.VisualStateManager.Hightlight();
            }
            else
            {
                ReferencedCard.CardManager.VisualStateManager.EndHighlight();
            }
        }
    }
}
