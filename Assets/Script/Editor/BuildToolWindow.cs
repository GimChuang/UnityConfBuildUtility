using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildToolWindow : EditorWindow {

    [TextArea]
    public string changeLogString;

    public AppInfo appInfo;
    string changelogPath;

    public static BuildToolWindow Instance
    {
        get { return GetWindow<BuildToolWindow>("Build Tool"); }
    }
    static BuildToolWindow _instance;

    void OnEnable()
    {
        _instance = this;
        Debug.LogWarning("BuildToolWindow OnEnable Called");
    }

    void OnDisable()
    {
        _instance = null;
        Debug.LogWarning("BuildToolWindow OnDisable Called");
    }

    public void Init(string _changeLogString, AppInfo _appInfo, string _changelogPath)
    {
        changeLogString = _changeLogString;
        appInfo = _appInfo;
        changelogPath = _changelogPath;
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        GUILayout.Label("Please Enter Changelog Before Build!");
        GUILayout.Label(appInfo.buildDate + "_" + appInfo.buildCount, EditorStyles.boldLabel);
        EditorGUILayout.Space();

        changeLogString = EditorGUILayout.TextArea(changeLogString, GUILayout.MinHeight(200));

        if (GUILayout.Button("Build"))
        {
            Debug.Log("Pressed! " + changeLogString);
            BuildFileExt.EditChangelog(changelogPath, appInfo.buildDate + "_" + appInfo.buildCount, changeLogString);
            Debug.LogWarning("Edit Done?");
            BuildUtility.Build_002(appInfo);
            this.Close();
        }
    }
}
