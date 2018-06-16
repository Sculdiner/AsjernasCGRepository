using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckListItem : MonoBehaviour {

    public Text DeckName;
    public Button DeckSelection;
    public int DeckId;

    public void Awake()
    {
        DeckSelection.onClick.AddListener(OnDeckClicked);
    }

    public void OnDeckClicked()
    {
        OnDeckSelected(DeckId,DeckName.text);
    }

    public Action<int,string> OnDeckSelected;
}
