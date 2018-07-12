using HighlightingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public GameObject GameObjectView;
    public Highlighter Highlighter;
    public BoardView BoardView;
    public CardManager CardManager;
    public QuestingPointBoard QuestingPointBoard;
    public ClientSideCard ClientSideCard { get; set; }
}
