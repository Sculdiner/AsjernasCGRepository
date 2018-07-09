using HighlightingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public MeshRenderer QuestMesh;
    public Highlighter Highlighter;
    public BoardView BoardView;
    public CardManager CardManager;
    public QuestingPointBoard QuestingPointBoard;

    public ClientSideCard CurrentQuest { get; set; }

    public void SetQuest(ClientSideCard card)
    {
        CurrentQuest = card;
        CardManager = card.CardManager;
    }

    public void Quest()
    {

    }
}