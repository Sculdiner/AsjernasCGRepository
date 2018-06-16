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
    private int inviteSourceId;

    public Action<int> OnGroupAccept;
    public Action<int> OnGroupDecline;

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
        OnGroupDecline(inviteSourceId);
        UIGroup.SetActive(false);
    }

    public void InitializeInvitation(int groupId, string username)
    {
        UIGroup.SetActive(true);
        inviteSourceId = groupId;
        InvitationText.text = username + " invites you to a group";
    }

    public void CancelInvitation()
    {
        UIGroup.SetActive(false);
        InvitationText.text = "";
    }
}
