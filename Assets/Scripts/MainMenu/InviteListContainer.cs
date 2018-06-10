using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteListContainer : MonoBehaviour {

    public GameObject InviteListItemPrefab;

    public void Add(int userId, string username, Action<int> OnInvite)
    {
        var invitationListItem = (GameObject)Instantiate(InviteListItemPrefab);
        var item = invitationListItem.GetComponent<InviteListItem>();
        item.Name.text = username;
        item.UserId = userId;
        item.OnClickHandler = OnInvite;


        invitationListItem.transform.SetParent(this.transform);
        invitationListItem.transform.localScale = new Vector3(1, 1, 1);
    }
}
