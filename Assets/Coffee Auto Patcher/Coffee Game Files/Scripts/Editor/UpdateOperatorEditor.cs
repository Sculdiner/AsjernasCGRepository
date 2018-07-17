using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UpdateOperator))]
public class UpdateOperatorEditor : Editor
{

    public UpdateOperator updateOperator;

    public override void OnInspectorGUI()
    {
        updateOperator = (UpdateOperator)target;

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("This scene and Operator is ONLY to be used in your Game. If you are trying to setup your Patcher/Launcher, open the PatcherMainScene located in _Scenes/PatcherMainScene.", MessageType.Error);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        if (DrawDefaultInspector())
        {

        }

        GUI.color = new Color(1, 1, 1, 0.30f);
        GUILayout.Box("", "HorizontalSlider", GUILayout.Height(16));
        GUI.color = Color.white;

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("Full URL for your Server File List (Should be in root directory of your CDN Bucket example s3.patchbucket.com/fileList.txt)", MessageType.Info);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("For Exes include the .exe at the end. Refer to the User Guide if more assistance is needed.", MessageType.Info);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("Upload Full Build Folder to your CDN or use the Coffee One Click Update to upload to S3.", MessageType.Info);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        GUI.color = new Color(1, 1, 1, 0.30f);
        GUILayout.Box("", "HorizontalSlider", GUILayout.Height(16));
        GUI.color = Color.white;
    }


 

}
