using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Scripts.Extensions;

public class Deck : MonoBehaviour {

    public List<CardAsset> cards = new List<CardAsset>();

    void Awake()
    {
        cards.Shuffle();
    }
	
}
