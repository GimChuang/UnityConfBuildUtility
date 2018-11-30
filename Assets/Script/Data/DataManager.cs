using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class DataManager : MonoBehaviour {

    public ConfigData LoadConfigData(ref bool _loadSuccess)
    {
        try
        {
            string path = Application.streamingAssetsPath + "\\ConfigData.json";
            string jsonString = File.ReadAllText(path);
            ConfigData configData = JsonUtility.FromJson<ConfigData>(jsonString);

            Debug.Log("Config Data Loaded.");

            _loadSuccess = true;
            return configData;
            
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            _loadSuccess = false;
            return null;
        }
    }

    public GameData LoadGameData(ref bool _loadSuccess)
    {
        try
        {
            string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/GameData.json");

            GameData gameData = JsonMapper.ToObject<GameData>(jsonString);

            Debug.Log("Game Data Loaded.");

            _loadSuccess = true;
            return gameData;
        }
        catch(Exception e)
        {
            Debug.LogError("Failed to Load GameData: " + e.Message);

            _loadSuccess = false;
            return null;
        }
        
    }

    public void SaveGameData(GameData _gameDataToSave, ref bool _saveSuccess)
    {
        try
        {
            JsonData gameDataToSave = JsonMapper.ToJson(_gameDataToSave);
            File.WriteAllText(Application.streamingAssetsPath + "/GameData.json", gameDataToSave.ToString());
            _saveSuccess = true;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            _saveSuccess = false;
        }
    }

}


