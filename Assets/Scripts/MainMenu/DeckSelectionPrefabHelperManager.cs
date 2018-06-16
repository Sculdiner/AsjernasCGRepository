using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectionPrefabHelperManager : MonoBehaviour {

    public DeckListContainer DeckListContainer;
    public GameObject DeckListPopupWrapper;
    public Button DeckTogglingButton;
    private bool getDeckListRequestInitialized;
    private bool getDeckListRequestPending;
    private bool DeckPopupIsOpen;

    public void Awake()
    {
        DeckTogglingButton.onClick.AddListener(Toggle);
    }

    public void Toggle()
    {
        if (DeckPopupIsOpen)
        {
            DeckListPopupWrapper.SetActive(false);
            DeckPopupIsOpen = false;
        }
        else
        {
            if (!getDeckListRequestPending && !getDeckListRequestInitialized)
            {
                getDeckListRequestInitialized = true;
                getDeckListRequestPending = true;
            }
            DeckListPopupWrapper.SetActive(true);
            DeckPopupIsOpen = true;
        }
    }

    public void LoadDeckListArea(Dictionary<int,string> deckList)
    {
        getDeckListRequestPending = false;
        getDeckListRequestInitialized = true;
        foreach (var key in deckList.Keys)
        {
            DeckListContainer.AddDeck(key, deckList[key]);
        }
    }
}
