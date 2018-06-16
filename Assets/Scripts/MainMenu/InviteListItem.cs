using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InviteListItem : MonoBehaviour {

    public Text Name;
    public Button InviteButton;
    public int UserId;
    public Action<int> OnClickHandler;

    public void Awake()
    {
        InviteButton.onClick.AddListener(OnInvite);
    }

    public void OnInvite()
    {
        OnClickHandler(UserId);
    }
}
