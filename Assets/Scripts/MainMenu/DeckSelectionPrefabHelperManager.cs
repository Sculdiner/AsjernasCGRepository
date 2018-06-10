using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectionPrefabHelperManager : MonoBehaviour {

    public DeckListContainer DeckListContainer;
    public GameObject DeckListPopupWrapper;
    public Button DeckTogglingButton;

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
            DeckListPopupWrapper.SetActive(true);
            DeckPopupIsOpen = true;
        }
    }
}
