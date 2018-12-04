using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildToolWindow : EditorWindow {

    public string changeLogString = "";
    public AppInfo appInfo;

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

    public void Init(string _buildInfo, AppInfo _appInfo)
    {
        changeLogString = _buildInfo;
        appInfo = _appInfo;
    }

    void OnGUI()
    {
        GUILayout.Label("Please Enter Changelog Before Build!");
        changeLogString = EditorGUILayout.TextField("Changelog", changeLogString);
        if (GUILayout.Button("OK"))
        {
            Debug.Log("Pressed! " + changeLogString);
            BuildUtility.Build_002(appInfo);
            this.Close();
        }
    }
}
