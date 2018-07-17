using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatchOperator))]
public class PatchOperatorEditor : Editor
{

    public PatchOperator patchOperator;

    public override void OnInspectorGUI()
    {
        patchOperator = (PatchOperator)target;

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("This scene and Operator is ONLY to be used in your Patcher. If you are trying to setup your in game Update Scene, copy and use only the files found in the Coffee Game Files folder for the proper UpdateOperator.", MessageType.Error);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("For those on Unity 2017.3+, you must use your patcher in a separate folder than your game. Recommended setup is within a folder next to the game exe.", MessageType.Error);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        if (DrawDefaultInspector())
        {

        }

        CoffeeUtilities.Separator();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("Full URL for your Server File List ending in fileList.txt (Should be in root directory of your CDN Bucket example s3.patchbucket.com/fileList.txt)", MessageType.Info);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("For Full Game Exe include the .exe at the end. Example Game.exe. For Mac Builds put the name of your mac build folder without the .app portion. Refer to the User Guide if more assistance is needed.", MessageType.Info);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();


        CoffeeUtilities.Separator();

    }




}
