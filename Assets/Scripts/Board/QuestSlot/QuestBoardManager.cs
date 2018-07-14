using UnityEngine;

public class QuestBoardManager : MonoBehaviour
{
    public QuestManager CurrentQuestingManager { get; set; }
    public Transform QuestTransform;
    public BoardManager BoardManager;
    public void SetQuest(QuestManager quest)
    {
        CurrentQuestingManager = quest;
        //CurrentQuestingManager = quest;
    }
    public void ProgressQuest(int points)
    {
    }
}