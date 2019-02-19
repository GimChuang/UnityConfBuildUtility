using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildUtilWindow : EditorWindow {

    [TextArea]
    public string changeLogString;

    string changelogPath;

    public static BuildUtilWindow Instance
    {
        get { return GetWindow<BuildUtilWindow>("Build Utility"); }
    }
    static BuildUtilWindow _instance;

    void OnEnable()
    {
        _instance = this;
        Debug.LogWarning("BuildUtilWindow OnEnable Called");
    }

    void OnDisable()
    {
        _instance = null;
        Debug.LogWarning("BuildUtilWindow OnDisable Called");
    }

    public void Init(string _changeLogString, string _changelogPath)
    {
        changeLogString = _changeLogString;
        changelogPath = _changelogPath;
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        GUILayout.Label("Please Enter Changelog Before Build!");
        GUILayout.Label(AppInfo.productName, EditorStyles.boldLabel);
        GUILayout.Label(AppInfo.buildDateAndCount, EditorStyles.boldLabel);
        EditorGUILayout.Space();

        changeLogString = EditorGUILayout.TextArea(changeLogString, GUILayout.MinHeight(200));

        if (GUILayout.Button("Build"))
        {
            // If nothing is typed then do nothing
            if (changeLogString == "")
                return;

            BuildFileExt.EditChangelog(changelogPath, changeLogString);
            BuildUtility.m_Build();
            this.Close();
        }
    }
}
