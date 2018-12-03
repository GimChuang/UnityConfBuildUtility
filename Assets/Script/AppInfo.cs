using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class AppInfo {

    public string ProductName;
    public bool bForceSingleInstance;

    public int buildDate;
    public int buildCount;

    public AppInfo(string _appInfoDataPath, string _productName, bool _bForceInstance)
    {
        LoadAppInfoData(_appInfoDataPath);

        ProductName = _productName;
        bForceSingleInstance = _bForceInstance;        
    }

    public void LoadAppInfoData(string _path)
    {
        try
        {
            string jsonString = File.ReadAllText(_path);
            AppInfoData appInfoData = JsonUtility.FromJson<AppInfoData>(jsonString);

            Debug.Log("AppInfoData Loaded.");

            buildDate = appInfoData.buildDate;
            buildCount = appInfoData.buildCount;

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void UpdateAppInfoData(string _date, int _count)
    {

    }
}

[Serializable]
public class AppInfoData
{
    public int buildDate;
    public int buildCount;
}

