using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class AppInfo {

    public string ProductName;
    public bool bForceSingleInstance;

    public string buildDate;
    public int buildCount;

    string currentDateString;

    public AppInfo(string _appInfoDataPath, string _productName, bool _bForceInstance)
    {
        
        currentDateString = string.Format("{0:yyMMdd}", System.DateTime.Now);
        //currentDateString = System.DateTime.Now.ToString("YYYY-MM-dd");

        // Load data from json to set the buildDate and buildCount
        AppInfoData data = LoadAppInfoData(_appInfoDataPath);
        if (data != null)
        {
            if (data.buildDate != currentDateString)
            {
                data.buildDate = currentDateString;
                data.buildCount = 1;
            }
            else
                data.buildCount++;

            Debug.Log("AppInfo: " + data.buildDate + "_" + data.buildCount);

            buildDate = data.buildDate;
            buildCount = data.buildCount;

            // Overwrite the file to update buildCount!
            UpdateAppInfoData(_appInfoDataPath, data);
        }
        else
        {
            Debug.LogError("Something's wrong when reading the AppInfoData json file");
        }

        ProductName = _productName;
        bForceSingleInstance = _bForceInstance;
        
    }

    public AppInfoData LoadAppInfoData(string _appInfoDataPath)
    {
        // If the file does not exist, create a new one
        if (!File.Exists(_appInfoDataPath))
        {
            string stringToWrite = "{" + "\"buildDate\":" + "\"" + currentDateString + "\"" + "," + "\"buildCount\":" + "0" + "}";
            using (FileStream fs = new FileStream(_appInfoDataPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(stringToWrite);
                    writer.Close();
                    writer.Dispose();
                }
                fs.Close();
                fs.Dispose();
            }

            Debug.LogWarning("AppInfoData file not exists. Created one.");
        }

        try
        {
            string jsonString = File.ReadAllText(_appInfoDataPath);
            AppInfoData data = JsonUtility.FromJson<AppInfoData>(jsonString);
            Debug.Log("AppInfoData Loaded.");
            return data;
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }

    public void UpdateAppInfoData(string _appInfoDataPath, AppInfoData _appInfoData)
    {
        try
        {
            JsonData dataToSave = JsonMapper.ToJson(_appInfoData);
            File.WriteAllText(_appInfoDataPath, dataToSave.ToString());
            Debug.Log("Overwrote AppInfoData.");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}

[Serializable]
public class AppInfoData
{
    public string buildDate;
    public int buildCount;
}

