using DG.Tweening;

public class EquipmentCastDragBehaviour : BaseDragCardBehaviour
{
    public EquipmentCastDragBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override void OnEndDrag()
    {
        base.OnEndDrag();

        ReferencedCard.IsDragging = false;
        var handHelper = BoardView.Instance.HandSlotManagerV2;
        BoardManager.Instance.ActiveCard = null;

        ReferencedCard.CardViewObject.GetComponent<DragRotator>().enabled = false;
        ReferencedCard.KillTweens();
        handHelper.RefreshHandPositions(Ease.Linear, .35f);
    }

    public override void OnStartDrag()
    {
        base.OnStartDrag();

        ReferencedCard.IsDragging = true;
        ReferencedCard.IsHovering = false;
        ReferencedCard.CardViewObject.GetComponent<DragRotator>().enabled = true;
        BoardManager.Instance.ActiveCard = ReferencedCard;

        ReferencedCard.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Card);

        ReferencedCard.HoverComponent.ForceKillHover();
    }
}