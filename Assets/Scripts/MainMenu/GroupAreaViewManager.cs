using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroupAreaViewManager : MonoBehaviour
{
    public bool IsLeaderArea;
    public int OwnerUserId;

    public GameObject DeckSelectionPrefab;
    public DeckSelectionPrefabHelperManager DeckListSelectionHelperManager;
    public TextMeshProUGUI UserName;
    public TextMeshProUGUI SelectedDeckName;
    public Button KickUserButton;
    public Button LeaveGroupButton;

    public Action<int> OnKickedUser;
    public Action OnLeftGroup;

    public void LoadArea(int leaderId)
    {
        //if (true)
        //{

        //}

        //if (AreaUserId == PhotonEngine.Instance.UserId)
        //{
        //    var prefab = (GameObject)Instantiate(DeckSelectionPrefab);
        //    DeckListSelectionHelperManager = prefab.GetComponent<DeckSelectionPrefabHelperManager>();
        //    prefab.transform.SetParent(this.transform);
        //    prefab.transform.localScale = new Vector3(1, 1, 1);
        //}

        //if (!IsCurrentPlayerArea && !IsLeaderArea)
        //{
        //    KickUserButton.gameObject.SetActive(true);
        //    LeaveGroupButton.gameObject.SetActive(false);
        //    KickUserButton.onClick.AddListener(OnKickedClicked);
        //}
        //else
        //{
        //    LeaveGroupButton.gameObject.SetActive(true);
        //    KickUserButton.onClick.AddListener(OnLeaveGroupClicked);
        //    KickUserButton.gameObject.SetActive(false);
        //}
    }



    public void OnKickedClicked()
    {
        if (OnKickedUser != null)
        {
            OnKickedUser.Invoke(OwnerUserId);
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
