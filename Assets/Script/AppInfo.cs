using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public static class AppInfo {

    public static string companyName;
    public static string productName;
    public static bool bDisplayResolutionDialog;
    public static bool bForceSingleInstance;

    static string buildDate;
    static int buildCount;
    public static string buildDateAndCount;

    public static string currentDateString;
    public static string currentDateTimeString;

    public static void SetUp(string _companyName, string _productName, bool _bDisplayResolutionDialog, bool _bForceSingleInstance)
    {
        companyName = _companyName;
        productName = _productName;
        bDisplayResolutionDialog = _bDisplayResolutionDialog;
        bForceSingleInstance = _bForceSingleInstance;

        currentDateString = System.DateTime.Now.ToString("yyyy-MM-dd");
        currentDateTimeString = System.DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

    }
 
    public static void UpdateBuildDateAndCountFromFile(string _appInfoDataPath)
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

            // Load data from json to set the buildDate and buildCount
            if (data != null)
            {
                if (data.buildDate != currentDateString)
                {
                    data.buildDate = currentDateString;
                    data.buildCount = 1;
                }
                else
                    data.buildCount++;

                buildDate = data.buildDate;
                buildCount = data.buildCount;
                buildDateAndCount = buildDate + "_" + buildCount.ToString("000");

            }
            else
            {
                Debug.LogError("Something's wrong when reading the AppInfoData json file");
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public static void OverwriteBuildDateAndCountToFile(string _appInfoDataPath)
    {
        try
        {
            AppInfoData newData = new AppInfoData();
            newData.buildDate = buildDate;
            newData.buildCount = buildCount;

            JsonData dataToSave = JsonMapper.ToJson(newData);
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

