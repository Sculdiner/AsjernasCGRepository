using AsjernasCG.Common.EventModels.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroupManager : MonoBehaviour
{
    public List<GroupPlayer> Group = new List<GroupPlayer>();
    public void Awake()
    {
        _instance = this;
    }
    public void Start()
    {
        DontDestroyOnLoad(this);
        Application.runInBackground = true;
    }

    public void ClearGroup()
    {
        Group = new List<GroupPlayer>();
        _model = new GroupStatusModel();
    }

    public void NewGroup(int leaderId, string username)
    {
        Group.Add(new GroupPlayer()
        {
            IsGroupLeader=  true,
            UserId =leaderId,
            UserName = username,
        });
    }

    public void AddNonLeaderPlayer(int userId, string username)
    {
        Group.Add(new GroupPlayer()
        {
            IsGroupLeader = false,
            UserId = userId,
            UserName = username,
        });
    }

    public void RemoveNonLeaderPlayers()
    {
        Group.RemoveAll(s => !s.IsGroupLeader);
    }

    private static GroupStatusModel _model = new GroupStatusModel();

    public static void StoreGroupStatus(GroupStatusModel model)
    {
        _model = model;
    }

    public static GroupStatusModel LoadGroupStatus()
    {
        return _model;
    }

    private static GroupManager _instance;
    public static GroupManager Instance
    {
        get { return _instance; }
    }

    public static void InitializeManager()
    {
        var groupManagerObject = GameObject.Find("GroupManager");
        if (groupManagerObject == null)
        {
            groupManagerObject = new GameObject("GroupManager");
            groupManagerObject.AddComponent<GroupManager>();
        }
    }
}
