﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroupAreaViewManager : MonoBehaviour
{
    public GameObject DeckSelectionPrefab;
    public DeckSelectionPrefabHelperManager DeckListSelectionHelperManager;
    public TextMeshProUGUI UserName;
    public TextMeshProUGUI SelectedDeckName;
    public Button KickUserButton;
    public Button LeaveGroupButton;
    public Button GameInitiationReadyStatusButton;
    public Text GameInitiationReadyStatusButtonText;

    public MainMenuPlayAreaView View { get; private set; }
    public Action<int> OnKickedUser;
    public Action OnLeftGroup;
    public bool PlayerReady { get; private set; }

    public int areasUserId { get; private set; }

    public void LoadArea(bool areaIsForCurrentUser, bool isGroupLeader, int userid, string username, MainMenuPlayAreaView view)
    {
        this.gameObject.SetActive(true);
        UserName.text = username;
        areasUserId = userid;
        View = view;
        if (userid == PhotonEngine.Instance.UserId)
        {
            var prefab = (GameObject)Instantiate(DeckSelectionPrefab);
            DeckListSelectionHelperManager = prefab.GetComponent<DeckSelectionPrefabHelperManager>();
            DeckListSelectionHelperManager.DeckListContainer.OnDeckSelected = ChangeSelectedDeck;
            prefab.transform.SetParent(this.transform);
            prefab.transform.localPosition = new Vector3(0, 0, 0);
            prefab.transform.localScale = new Vector3(1, 1, 1);
            (View.Controller as MainMenuController).SendGetDeckList();
            GameInitiationReadyStatusButton.onClick.AddListener(ReadyStatusButtonToggle);
        }

        if (!isGroupLeader)
        {
            if (!areaIsForCurrentUser)
            {
                KickUserButton.gameObject.SetActive(true);
                LeaveGroupButton.gameObject.SetActive(false);
                KickUserButton.onClick.AddListener(OnKickedClicked);
            }
            else
            {
                LeaveGroupButton.gameObject.SetActive(true);
                KickUserButton.onClick.AddListener(OnLeaveGroupClicked);
                KickUserButton.gameObject.SetActive(false);
            }
        }
    }

    public void ChangeGameInitiationReadyStatus(bool isReady)
    {
        if (isReady)
        {
            ReadyStatusButton_Ready();
        }
        else
        {
            ReadyStatusButton_NotReady();
        }
    }

    public void ChangeSelectedDeck(int deckId, string deckName)
    {
        SelectedDeckName.text = deckName;
        (View.Controller as MainMenuController).SendDeckSelectionChanged(deckId);
    }


    public void OnKickedClicked()
    {
        if (OnKickedUser != null)
        {
            OnKickedUser.Invoke(areasUserId);
        }
    }

    public void OnLeaveGroupClicked()
    {
        if (OnLeftGroup != null)
        {
            OnLeftGroup.Invoke();
        }
    }

    public void ReadyStatusButton_Ready()
    {
        (GameInitiationReadyStatusButton.GetComponent<Image>() as Image).color = new Color(81, 192, 102);
        (GameInitiationReadyStatusButtonText.GetComponent<Text>() as Text).text = "Ready";
    }

    public void ReadyStatusButton_NotReady()
    {
        (GameInitiationReadyStatusButton.GetComponent<Image>() as Image).color = new Color(115, 130, 117);
        (GameInitiationReadyStatusButtonText.GetComponent<Text>() as Text).text = "Not Ready";
    }

    public void ReadyStatusButtonToggle()
    {
        if (areasUserId != PhotonEngine.Instance.UserId)
            return;

        if (PlayerReady)
        {
            PlayerReady = false;
            ReadyStatusButton_NotReady();
            (View.Controller as MainMenuController).SendChangeReadyState(false);
        }
        else
        {
            PlayerReady = true; 
            ReadyStatusButton_Ready();
            (View.Controller as MainMenuController).SendChangeReadyState(true);
        }
    }
}
