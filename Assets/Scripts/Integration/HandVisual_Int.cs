﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using AsjernasCG.Common.BusinessModels.CardModels;

public class HandVisual_Int : MonoBehaviour
{
    // PUBLIC FIELDS
    public AreaPosition owner;
    public bool TakeCardsOpenly = true;
    public SameDistanceChildren slots;

    public BoardView View;

    [Header("Transform References")]
    public Transform DrawPreviewSpot;
    public Transform DeckTransform;
    public Transform OtherCardDrawSourceTransform;
    public Transform PlayPreviewSpot;

    // PRIVATE : a list of all card visual representations as GameObjects


    // ADDING OR REMOVING CARDS FROM HAND

    // add a new card GameObject to hand
    public void AddCard(GameObject card)
    {
        // parent this card to our Slots GameObject
        card.transform.SetParent(slots.transform);

        // re-calculate the position of the hand
        PlaceCardsOnNewSlots();
        UpdatePlacementOfSlots();
    }

    // remove a card GameObject from hand
    public void RemoveCardFromHand(ClientSideCard card)
    {
        // remove a card from the list


        // re-calculate the position of the hand
        PlaceCardsOnNewSlots();
        UpdatePlacementOfSlots();
    }

    // remove card with a given index from hand
    public void RemoveCardAtIndex(int index)
    {
        //CardsInHand.RemoveAt(index);
        // re-calculate the position of the hand
        PlaceCardsOnNewSlots();
        UpdatePlacementOfSlots();
    }

    // get a card GameObject with a given index in hand
    public GameObject GetCardAtIndex(int index)
    {
        return null;
    }

    // MANAGING CARDS AND SLOTS

    // move Slots GameObject according to the number of cards in hand
    void UpdatePlacementOfSlots()
    {
        float posX;
        var cardsInHand = View.BoardManager.GetMyHand();
        if (cardsInHand.Count > 0)
            posX = (slots.Children[0].transform.localPosition.x - slots.Children[cardsInHand.Count - 1].transform.localPosition.x) / 2f;
        else
            posX = 0f;

        // tween Slots GameObject to new position in 0.3 seconds
        slots.gameObject.transform.DOLocalMoveX(posX, 0.3f);
    }

    // shift all cards to their new slots
    void PlaceCardsOnNewSlots()
    {
        var handCardList = View.BoardManager.GetMyHand();
        foreach (ClientSideCard g in handCardList)
        {
            // tween this card to a new Slot
            g.CardViewObject.transform.DOLocalMoveX(slots.Children[handCardList.IndexOf(g)].transform.localPosition.x, 0.3f);

            // apply correct sorting order and HandSlot value for later 
            //WhereIsTheCardOrCreature w = g.GetComponent<WhereIsTheCardOrCreature>();
            //w.Slot = CardsInHand.IndexOf(g);
            //w.SetHandSortingOrder();
        }
    }

    // CARD DRAW METHODS

    // creates a card and returns a new card as a GameObject
    GameObject MoveToDeckAtPosition(ClientSideCard c, Vector3 position, Vector3 eulerAngles)
    {
        // Instantiate a card depending on its type
        c.CardViewObject.transform.position = position;
        c.CardViewObject.transform.rotation = Quaternion.Euler(eulerAngles);
        //else
        //{
        //    // this is a spell: checking for targeted or non-targeted spell
        //    if (c.CardStats.CastType == CardCastType.Field)
        //        card = GameObject.Instantiate(GlobalSettings.Instance.NoTargetSpellCardPrefab, position, Quaternion.Euler(eulerAngles)) as GameObject;
        //    //else if
        //    //else if
        //    //else if
        //    else
        //    {
        //        card = GameObject.Instantiate(GlobalSettings.Instance.TargetedSpellCardPrefab, position, Quaternion.Euler(eulerAngles)) as GameObject;
        //        // pass targeting options to DraggingActions
        //        DragSpellOnTarget dragSpell = card.GetComponentInChildren<DragSpellOnTarget>();
        //        dragSpell.Targets = c.Targets;
        //    }
        //}

        // apply the look of the card based on the info from CardAsset

        return c.CardViewObject;
    }

    // gives player a new card from a given position
    public void GivePlayerACard(ClientSideCard c, bool fast = false)
    {
        var card = c.CardViewObject;

        card = MoveToDeckAtPosition(c, DeckTransform.position, new Vector3(0f, 0f, -179f));
        //else
        //    card = MoveToDeckAtPosition(c, OtherCardDrawSourceTransform.position, new Vector3(0f, 0f, -179f));

        // pass this card to HandVisual class
        AddCard(card);

        //WhereIsTheCardOrCreature w = card.GetComponent<WhereIsTheCardOrCreature>();
        //w.VisualState = VisualStates.Transition;

        // move card to the hand;
        Sequence s = DOTween.Sequence();
        if (!fast)
        {
            // Debug.Log ("Not fast!!!");
            s.Append(card.transform.DOMove(DrawPreviewSpot.position, 1f));
            if (TakeCardsOpenly)
                s.Insert(0f, card.transform.DORotate(Vector3.zero, 1f));
            else
                s.Insert(0f, card.transform.DORotate(new Vector3(0f, 179f, 0f), 1f));
            s.AppendInterval(1f);
            // displace the card so that we can select it in the scene easier.
            s.Append(card.transform.DOLocalMove(slots.Children[0].transform.localPosition, 1f));
        }
        else
        {
            // displace the card so that we can select it in the scene easier.
            s.Append(card.transform.DOLocalMove(slots.Children[0].transform.localPosition, 0.5f));
            if (TakeCardsOpenly)
                s.Insert(0f, card.transform.DORotate(Vector3.zero, 0.5f));
        }

        s.OnComplete(() => PhotonEngine.NextAction());
    }

    // PLAYING SPELLS

    // 2 Overloaded method to show a spell played from hand
    public void PlayASpellFromHand(int CardID)
    {
        GameObject card = IDHolder.GetGameObjectWithID(CardID);
        PlayASpellFromHand(card);
    }

    public void PlayASpellFromHand(GameObject CardVisual)
    {
        Command.CommandExecutionComplete();
        CardVisual.GetComponent<WhereIsTheCardOrCreature>().VisualState = VisualStates.Transition;
        //RemoveCard(CardVisual);

        CardVisual.transform.SetParent(null);

        Sequence s = DOTween.Sequence();
        s.Append(CardVisual.transform.DOMove(PlayPreviewSpot.position, 1f));
        s.Insert(0f, CardVisual.transform.DORotate(Vector3.zero, 1f));
        s.AppendInterval(2f);
        s.OnComplete(() =>
            {
                //Command.CommandExecutionComplete();
                Destroy(CardVisual);
            });
    }


}
