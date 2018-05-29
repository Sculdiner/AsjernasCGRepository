using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FriendListViewManager : MonoBehaviour
{
    public FriendListMasterModel FriendListMasterModel;
    public GameObject PREFAB_FriendListItem;

    public void UpdateUserInformation()
    {

    }

    public void AddFriendItem()
    {
        var newFriend = (GameObject)Instantiate(PREFAB_FriendListItem);
        newFriend.transform.SetParent(FriendListMasterModel.FriendListContainer.transform);
    }

    public void RemoveFriendItem()
    {

    }

    private void Start()
    {
        
    }
}
