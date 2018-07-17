using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FileListGenerator))]
public class FileListGeneratorEditor : Editor {

    public static FileListGenerator fileListGenerator;

    public override void OnInspectorGUI()
    {
        fileListGenerator = (FileListGenerator)target;

        if (DrawDefaultInspector())
        {

        }

        GUI.color = new Color(1, 1, 1, 0.25f);
        GUILayout.Box("", "HorizontalSlider", GUILayout.Height(16));
        GUI.color = Color.white;

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("For Build Output Path paste the full directory path for your latest build of your game. For Mac Builds, the output path will be the folder containing both your game.app and patcher.app folders.", MessageType.Info);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("For Full Game Exe include the .exe at the end. Refer to the User Guide if more assistance is needed.", MessageType.Info);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("Upload Full Build Folder to your CDN or use the Coffee One Click Update to upload to S3.", MessageType.Info);
        GUILayout.Space(20);
        GUILayout.EndHorizontal();
        GUI.color = new Color(1, 1, 1, 0.25f);
        GUILayout.Box("", "HorizontalSlider", GUILayout.Height(16));
        GUI.color = Color.white;


        if (GUILayout.Button("Generate File List"))
        {
            fileListGenerator.AttemptFileListGeneration();
        }


    }


  


}
