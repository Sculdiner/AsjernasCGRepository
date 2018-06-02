using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FriendListMasterModel : MonoBehaviour {

    //current user information
    //friend list
    public FriendListContainer FriendListContainer;
    public RectTransform FriendListWrapper;
    public Button FriendListToggleButton;

    private bool friendListIsDisplayed = false;

    private void Awake()
    {
        FriendListToggleButton.onClick.AddListener(Toggle);
    }

    public void Toggle()
    {
        if (friendListIsDisplayed)
        {
            friendListIsDisplayed = false;
            var obj = FriendListWrapper.gameObject;
            obj.SetActive(false);
        }
        else
        {
            friendListIsDisplayed = true;
            var obj = FriendListWrapper.gameObject;
            obj.SetActive(true);
        }
    }
}
