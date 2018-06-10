using System;
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

    public Action<int> OnKickedUser;
    public Action OnLeftGroup;
    private int areasUserId;

    public void LoadArea(bool areaIsForCurrentUser, bool isGroupLeader, int userid, string username)
    {
        this.gameObject.SetActive(true);
        UserName.text = username;
        areasUserId = userid;
        if (userid == PhotonEngine.Instance.UserId)
        {
            var prefab = (GameObject)Instantiate(DeckSelectionPrefab);
            DeckListSelectionHelperManager = prefab.GetComponent<DeckSelectionPrefabHelperManager>();
            prefab.transform.SetParent(this.transform);
            prefab.transform.localPosition = new Vector3(0, 0, 0);
            prefab.transform.localScale = new Vector3(1, 1, 1);
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

}
