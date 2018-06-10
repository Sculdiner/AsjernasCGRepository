using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvitationManager : MonoBehaviour {

    public GameObject UIGroup;
    public TextMeshProUGUI InvitationText;
    public Button AcceptInvitationButton;
    public Button DeclineInvitationButton;
    private string inviteSourceId;

    public Action<string> OnGroupAccept;
    public Action<string> OnGroupDecline;

    public void Awake()
    {
        AcceptInvitationButton.onClick.AddListener(OnAcceptPressed);
        DeclineInvitationButton.onClick.AddListener(OnDeclinePressed);
    }

    public void OnAcceptPressed()
    {
        OnGroupAccept(inviteSourceId);
        UIGroup.SetActive(false);
    }
    public void OnDeclinePressed()
    {
        OnGroupAccept(inviteSourceId);
        UIGroup.SetActive(false);
    }

    public void InitializeInvitation(string groupId, string username)
    {
        inviteSourceId = groupId;
        InvitationText.text = username + " invites you to a group";
        UIGroup.SetActive(true);
    }
}
