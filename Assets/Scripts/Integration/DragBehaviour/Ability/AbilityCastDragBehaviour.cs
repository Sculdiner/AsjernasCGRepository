public class AbilityCastDragBehaviour : BaseDragCardBehaviour
{
    public AbilityCastDragBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override bool AllowInSetup()
    {
        return true;
    }
}