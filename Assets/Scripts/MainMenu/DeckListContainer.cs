using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListContainer : MonoBehaviour
{

    public GameObject DeckListItemPrefab;

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    public void AddDeck(int deckId, string deckName)
    {
        var prefab = (GameObject)Instantiate(DeckListItemPrefab);
        var comp = prefab.GetComponent<DeckListItem>();
        comp.DeckId = deckId;
        comp.DeckName.text = deckName;
        comp.OnDeckSelected = DeckSelected;
        prefab.transform.SetParent(this.transform);
        prefab.transform.localScale = new Vector3(1, 1, 1);
    }

    public void DeckSelected(int deckId, string deckName)
    {
        OnDeckSelected(deckId, deckName);
    }

    public Action<int,string> OnDeckSelected;
}
