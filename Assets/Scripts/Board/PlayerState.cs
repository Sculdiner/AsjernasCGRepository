public class PlayerState : ParticipatorState
{
    public int UserId { get; set; }

    public int Resources { get; set; } 

    public AllySlotManager AllySlotManager { get; set; }
    public PlayerInfoManager PlayerInfoManager { get; set; }
    public void UpdateResources()
    {
        PlayerInfoManager.Resources.text = Resources.ToString();
    }
}
