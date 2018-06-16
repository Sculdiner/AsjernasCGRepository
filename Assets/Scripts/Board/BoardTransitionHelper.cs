using AsjernasCG.Common.EventModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BoardTransitionHelper : MonoBehaviour
{
    private static BoardTransitionHelper _instance;
    public static BoardTransitionHelper Instance
    {
        get { return _instance; }
    }
    public void Awake()
    {
        _instance = this;
    }
    public void Start()
    {
        DontDestroyOnLoad(this);
        Application.runInBackground = true;
    }

    public GameInitializeModel GameInitializationModel { get; private set; }

    public void StoreGameInformation(GameInitializeModel gameInitiatializationModel)
    {
        GameInitializationModel = gameInitiatializationModel;
    }

    public static void InitializeInstance()
    {
        var obj = GameObject.Find("BoardTransitionHelper");
        if (obj == null)
        {
            obj = new GameObject("BoardTransitionHelper");
            obj.AddComponent<BoardTransitionHelper>();
        }
    }
}

