
using UnityEngine;
using UnityEditor;

public class AboutBox : EditorWindow
{

    static Vector2 _DialogSize = new Vector2(280, 280);
    static string _Copyright = "© " + System.DateTime.Now.Year + " Coffeebns.com. All Rights Reserved.";
    public static Texture2D _CoffeeLogo;
    protected static GUIStyle _SmallTextStyle = null;


    void OnEnable()
    {
        _CoffeeLogo = (Texture2D)Resources.Load("Icons/128x128", typeof(Texture2D));
    }

    public static void Create()
    {

        AboutBox msgBox = (AboutBox)EditorWindow.GetWindow(typeof(AboutBox), true);

        msgBox.titleContent.text = "About Coffee Auto Patcher";

        msgBox.minSize = new Vector2(_DialogSize.x, _DialogSize.y);
        msgBox.maxSize = new Vector2(_DialogSize.x + 1, _DialogSize.y + 1);
        msgBox.position = new Rect(
            (Screen.currentResolution.width / 2) - (_DialogSize.x / 2),
            (Screen.currentResolution.height / 2) - (_DialogSize.y / 2),
            _DialogSize.x,
            _DialogSize.y);
        msgBox.Show();

    }

    void OnGUI()
    {

        if (_CoffeeLogo != null)
            GUI.DrawTexture(new Rect(10, 10, _CoffeeLogo.width, _CoffeeLogo.height), _CoffeeLogo);

        GUILayout.BeginArea(new Rect(20, 155, Screen.width - 40, Screen.height - 40));
        GUI.backgroundColor = Color.clear;

        GUILayout.Label(_Copyright + "\n", SmallTextStyle);
        if (GUILayout.Button("Discord Support Chat", CoffeeUtilities.LinkStyle)) { Application.OpenURL("https://discord.gg/9WYC2sQ"); }
        if (GUILayout.Button("https://coffeebns.com", CoffeeUtilities.LinkStyle)) { Application.OpenURL("https://coffeebns.com"); }
        if (GUILayout.Button("https://twitter.com/CoffeeReclaimed", CoffeeUtilities.LinkStyle)) { Application.OpenURL("https://twitter.com/CoffeeReclaimed"); }
        if (GUILayout.Button("Video Setup Guide", CoffeeUtilities.LinkStyle)) { Application.OpenURL("https://youtu.be/iY6Ke3yzpHs"); }
        GUI.color = Color.gray;
        GUI.backgroundColor = Color.gray;
        GUILayout.EndArea();


        


    }

    public static GUIStyle SmallTextStyle
    {
        get
        {
            if (_SmallTextStyle == null)
            {
                _SmallTextStyle = new GUIStyle("Label");
                _SmallTextStyle.fontSize = 9;
            }
            return _SmallTextStyle;
        }
    }




}
