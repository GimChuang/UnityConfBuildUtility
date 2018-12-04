using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

public class BuildUtility {

    // Path.GetDirectoryName returns the directory information for the specified path string
    // the string projectPath will be the directory which contains the Assets and ProjectSettings folders
    static string projectPath = Path.GetDirectoryName(Application.dataPath);

    [MenuItem("BuildUtility/Build_001")]
    public static void Build_001()
    {
        ClearConsole();
        
        string appInfoDataPath = GetSourcePath("Files_001/AppInfo/AppInfoData.json");
        AppInfo appInfo = new AppInfo(appInfoDataPath, "MyAwesomeGame001", true);
        PlayerSettings.productName = appInfo.ProductName;
        PlayerSettings.forceSingleInstance = appInfo.bForceSingleInstance;

        // Build
        //string buildName = "Game_001";
        string buildName = "Game_001" + "_" + appInfo.buildDate + "_" + appInfo.buildCount;
        string destinationPath = GenerateBuildPath(buildName);

        CreateDirectoryForBuild(destinationPath);

        string[] scenes = { "Assets/Scene/MyScene_001.unity" };
        BuildPipeline.BuildPlayer(scenes, destinationPath + "/" + buildName + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.ShowBuiltPlayer);

        DirectoryInfo targetDir = new DirectoryInfo(destinationPath + "/" + buildName + "_Data" + "/StreamingAssets");

        // Copy general files
        string sourcePath_general = GetSourcePath("General");
        DirectoryInfo sourceDir_general = new DirectoryInfo(sourcePath_general);
        BuildFileExt.CopyAllTo(sourceDir_general, targetDir);

        // Copy specific files
        string sourcePath = GetSourcePath("Files_001");
        DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);        
        BuildFileExt.CopyAllTo(sourceDir, targetDir);

    }

    
    public static void Build_002(AppInfo _appInfo)
    {
        /*
        ClearConsole();

        string appInfoDataPath = GetSourcePath("Files_002/AppInfo/AppInfoData.json");
        AppInfo appInfo = new AppInfo(appInfoDataPath, "MyAwesomeGame002", false);
        PlayerSettings.productName = appInfo.ProductName;
        PlayerSettings.forceSingleInstance = appInfo.bForceSingleInstance;
        */

        // Build
        //string buildName = "Game_002";
        string buildName = "Game_002" + "_" + _appInfo.buildDate + "_" + _appInfo.buildCount;
        string destinationPath = GenerateBuildPath(buildName);

        CreateDirectoryForBuild(destinationPath);

        string[] scenes = { "Assets/Scene/MyScene_002.unity" };
        BuildPipeline.BuildPlayer(scenes, destinationPath + "/" + buildName + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.ShowBuiltPlayer);

        DirectoryInfo targetDir = new DirectoryInfo(destinationPath + "/" + buildName + "_Data" + "/StreamingAssets");

        // Copy general files
        string sourcePath_general = GetSourcePath("General");
        DirectoryInfo sourceDir_general = new DirectoryInfo(sourcePath_general);
        BuildFileExt.CopyAllTo(sourceDir_general, targetDir);

        // Copy specific files
        string sourcePath = GetSourcePath("Files_002");
        DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
        BuildFileExt.CopyAllTo(sourceDir, targetDir);

    }

    [MenuItem("BuildUtility/Build_002_Load")]
    public static void LoadAppInfo_Build_002()
    {
        ClearConsole();

        string appInfoDataPath = GetSourcePath("Files_002/AppInfo/AppInfoData.json");
        AppInfo appInfo = new AppInfo(appInfoDataPath, "MyAwesomeGame002", false);
        PlayerSettings.productName = appInfo.ProductName;
        PlayerSettings.forceSingleInstance = appInfo.bForceSingleInstance;

        // Open the build tool window
        BuildToolWindow.Instance.Init(appInfo.buildDate + "_" + appInfo.buildCount, appInfo, GetSourcePath("Files_002/AppInfo/Changelog.txt"));

    }

    static void SetPlayerSettings()
    {
        //PlayerSettings.companyName = "Gim";
        //PlayerSettings.productName = "Game";
        //PlayerSettings.runInBackground = true;
        //PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Disabled
        //PlayerSettings.defaultScreenWidth = 1920;
        //PlayerSettings.defaultScreenWidth = 1080;
        //PlayerSettings.SetAspectRatio(AspectRatio.Aspect16by9, true);
        //PlayerSettings.forceSingleInstance = true;
        
    }

    static string GenerateBuildPath(string _buildName)
    {
        return projectPath + "/Builds/" + _buildName;
    }

    static void CreateDirectoryForBuild(string _destinationPath)
    {

        DirectoryInfo buildDir = new DirectoryInfo(_destinationPath);

        // If the directory already exists, delete it first
        if (buildDir.Exists)
        {
            Debug.LogWarning("buildDir already exists. Delete it first!");
            BuildFileExt.MakeFilesWritable(buildDir);

            Directory.Delete(_destinationPath, true);
        }

        // and then create the directory
        buildDir.Create();
        Debug.Log("Created directory for build: " + _destinationPath);

    }
    
    static string GetSourcePath(string _folderPath)
    {        
        return projectPath + "/FilesToInclude/" + _folderPath;
    }

    public static void ClearConsole()
    {
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
