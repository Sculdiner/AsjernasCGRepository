using AsjernasCG.Common.BusinessModels;
using AsjernasCG.Common.EventModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FriendListContainer : MonoBehaviour
{
    private readonly object locker = new object();
    public GameObject PREFAB_FriendListItemOnline;
    public GameObject PREFAB_FriendListItemOffline;
    public Dictionary<int, GameObject> FriendList = new Dictionary<int, GameObject>();
    private List<FriendListItemViewModel> ViewModel = new List<FriendListItemViewModel>();
    public void UpdateFriendItem(FriendListItemViewModel newFriend)
    {
        lock (locker)
        {
            if (FriendList.ContainsKey(newFriend.UserId))
            {
                var friendObject = FriendList[newFriend.UserId];
                DestroyImmediate(friendObject);
                FriendList.Remove(newFriend.UserId);
                ViewModel.RemoveAll(s => s.UserId == newFriend.UserId);
            }
            var prefabItem = newFriend.Status == UserStatusModel.Disconnected ? PREFAB_FriendListItemOffline : PREFAB_FriendListItemOnline;
            var friendPrefab = (GameObject)Instantiate(prefabItem);
            FriendList.Add(newFriend.UserId, friendPrefab);
            ViewModel.Add(newFriend);
            var comp = friendPrefab.GetComponent<FriendListItemDisplayModel>();
            comp.Name.text = newFriend.Username;
            comp.Status.text = newFriend.StatusDescription;
            friendPrefab.transform.SetParent(this.transform);
            friendPrefab.transform.localScale = new Vector3(1, 1, 1);

            //calculate order
            //ViewModel = ViewModel.OrderByDescending(n => n.Status).ThenByDescending(a => a.Username).ToList();
            ViewModel = ViewModel.OrderByDescending(n => n.Status).ThenBy(a => a.Username).ToList();
            for (int i = 0; i < ViewModel.Count; i++)
            {
                (FriendList[ViewModel[i].UserId].transform as RectTransform).SetSiblingIndex(i);
            }

        }
    }
}