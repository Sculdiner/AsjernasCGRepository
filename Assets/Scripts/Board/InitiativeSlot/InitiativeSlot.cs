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
    public Gradient CardHighlightColor { get; set; }
    public ClientSideCard ReferencedCard { get; private set; }
    private bool BoardCardWasAlreadyHighlighted;
    private HighlightType? AlreadyActiveHighlightType;

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
            AlreadyActiveHighlightType = ReferencedCard.CardManager.VisualStateManager.HighlightType;
            ReferencedCard.CardManager.VisualStateManager.Highlight(HighlightType.InitiativeSlotHoveringIndicator);
        }
    }

    public void OnMouseExit()
    {
        if (ReferencedCard != null)
        {
            if (BoardCardWasAlreadyHighlighted && AlreadyActiveHighlightType.HasValue)
            {
                BoardCardWasAlreadyHighlighted = false;
                ReferencedCard.CardManager.VisualStateManager.Highlight(AlreadyActiveHighlightType.Value);
                AlreadyActiveHighlightType = null;
            }
            else
            {
                ReferencedCard.CardManager.VisualStateManager.EndHighlight();
            }
        }
    }
}
