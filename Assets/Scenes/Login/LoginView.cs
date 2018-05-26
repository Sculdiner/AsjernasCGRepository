using System;
using UnityEngine;
using System.Collections;
using AsjernasCG.Common;
using UnityEngine.UI;
using TMPro;

public class LoginView : View
{
    public string ServerAddress;
    public string ApplicationName;
    public bool loggingIn = false;

    public string UserName;
    public string PassWord;
    public string Email;
    public string GameStateStatus;
    public string LoginUserName;
    public string LoginPassword;
    public bool hasNewMessage;
    public string oldMessageText;
    public string newMessageText;
    public override void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        Controller = new LoginController(this, RoutingOperationCode.Login);
        PhotonEngine.UseExistingOrCreateNewPhotonEngine(ServerAddress, ApplicationName);
        var dp = GameObject.Find("ServerDropdown");
        var dpComp = dp.GetComponent<TMP_Dropdown>();
        dpComp.onValueChanged.AddListener(ChangeServer);
    }



    public void ChangeServer(int server)
    {
        if (server == 0)
        {
            PhotonEngine.ChangeConnectionOrReconnect("localhost:5060", "AsjernasCGServer");
            PhotonEngine.Instance.Controller = Controller as LoginController; 
        }
        else if(server == 1)
        {
            PhotonEngine.ChangeConnectionOrReconnect("104.214.237.49:5060", "AsjernasCGServer");
            PhotonEngine.Instance.Controller = Controller as LoginController;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateStatus != PhotonEngine.GameStateName && !string.IsNullOrEmpty(PhotonEngine.GameStateName))
        {
            GameStateStatus = PhotonEngine.GameStateName;
        }
    }

    void OnGUI()
    {
        UserName = GUI.TextField(new Rect(5, 5, 300, 30), UserName, 64);
        PassWord = GUI.TextField(new Rect(5, 40, 300, 30), PassWord, 64);
        Email = GUI.TextField(new Rect(5, 75, 300, 30), Email, 64);
        if (GUI.Button(new Rect(5, 110, 300, 30), "Register") && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PassWord) && !string.IsNullOrEmpty(Email))
        {
            _controller.SendRegister(UserName, PassWord, Email);
        }

        GUI.Label(new Rect(5, 145, 300, 30), GameStateStatus);

        LoginUserName = GUI.TextField(new Rect(5, 180, 300, 30), LoginUserName, 64);
        LoginPassword = GUI.TextField(new Rect(5, 215, 300, 30), LoginPassword, 64);
        if (GUI.Button(new Rect(5, 250, 300, 30), "Login") && !string.IsNullOrEmpty(LoginUserName) && !string.IsNullOrEmpty(LoginPassword))
        {
            _controller.SendLogin(LoginUserName, LoginPassword);
        }

        if (hasNewMessage)
        {
            GUI.Label(new Rect(5, 285, 300, 30), newMessageText);
            oldMessageText = newMessageText;
            newMessageText = null;
            hasNewMessage = false;
        }
        else
        {
            GUI.Label(new Rect(5, 285, 300, 30), oldMessageText);
        }

    }

    public void MatchmakeRequest()
    {
        _controller.LoadDeckTest(0);
    }

    public override RoutingOperationCode GetRoutingOperationCode()
    {
        return RoutingOperationCode.Login;
    }

    private LoginController _controller;
    public override IViewController Controller { get { return (IViewController)_controller; } protected set { _controller = value as LoginController; } }
}
