using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public static class CoffeeUtilities {


    private static GUIStyle _LinkStyle = null;



    public static void Separator()
    {

        GUI.color = new Color(1, 1, 1, 0.30f);
        GUILayout.Box("", "HorizontalSlider", GUILayout.Height(16));
        GUI.color = Color.white;

    }

 


    public static GUIStyle LinkStyle
    {
        get
        {
            if (_LinkStyle == null)
            {
                _LinkStyle = new GUIStyle("Label");
                _LinkStyle.normal.textColor = (EditorGUIUtility.isProSkin ? new Color(0.8f, 0.8f, 1.0f, 1.0f) : Color.blue);
            }
            return _LinkStyle;
        }
    }
}
