using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InviteListManager : MonoBehaviour {

    private bool IsVisible;
    public Button TogglingButton;
    public GameObject ListWrapper;
    public InviteListContainer Container;  

    //the game objects visible when there are no players in the group
    public GameObject InviteStateUIGroup;

    //the game objects visible when the user is in a party
    public GameObject InGroupStateUIGroup;

    public void Awake()
    {
        TogglingButton.onClick.AddListener(ToggleList);
    }

    public void ToggleList()
    {
        if (IsVisible)
        {
            ListWrapper.gameObject.SetActive(false);
            IsVisible = false;
        }
        else
        {
            ListWrapper.gameObject.SetActive(true);
            IsVisible = true;
        }
    }

    public void ChangeStatusToInGroup()
    {
        InviteStateUIGroup.gameObject.SetActive(false);
        InGroupStateUIGroup.gameObject.SetActive(true);
    }
    public void ChangeStatusToGroupPending()
    {
        InGroupStateUIGroup.gameObject.SetActive(false);
        InviteStateUIGroup.gameObject.SetActive(true);
    }
}
