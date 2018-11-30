using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    public string url = "https://wwwww";
    public int width = 1920;

    [Space]
    public int fontSize = 60;

    string guiString;
    GUIStyle guiStyle = new GUIStyle();

    [Space]
    public DataManager dataManager;
    ConfigData configData;

    void Start () {

        bool loadConfigSuccess = false;
        configData = dataManager.LoadConfigData(ref loadConfigSuccess);
        if (loadConfigSuccess)
        {
            url = configData.url;
            width = configData.width;
        }

        guiString = url + "\n" + width;
        guiStyle.fontSize = fontSize;
    }
	
	void Update () {
        
	}

    public void SetSettings(string _url, int _width)
    {
        url = _url;
        width = _width;
        guiString = _url + "\n" + _width;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0,0,Screen.width, Screen.height), guiString, guiStyle);
    }
}
