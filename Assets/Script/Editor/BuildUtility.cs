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

    static string GenerateBuildPath(string _buildName)
    {
        return projectPath + "/Builds/" + _buildName;
    }

    static string GetIncludePath(string _folderPath)
    {
        return projectPath + "/FilesToInclude/" + _folderPath;
    }

    static string GetAppInfoDataPath(string _folderPath)
    {
        return projectPath + "/FilesToInclude/" + _folderPath + "/AppInfoData.json";
    }

    static string GetChangelogPath(string _folderPath)
    {
        return projectPath + "/FilesToInclude/" + _folderPath + "/Changelog.txt";
    }

    //[MenuItem("BuildUtility/Build_001")]
    /*
    public static void Build_001()
    {
        ClearConsole();
        
        string appInfoDataPath = GetIncludePath("Files_001/AppInfo/AppInfoData.json");
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
        string sourcePath_general = GetIncludePath("General");
        DirectoryInfo sourceDir_general = new DirectoryInfo(sourcePath_general);
        BuildFileExt.CopyAllTo(sourceDir_general, targetDir);

        // Copy specific files
        string sourcePath = GetIncludePath("Files_001");
        DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);        
        BuildFileExt.CopyAllTo(sourceDir, targetDir);

    }
    */

    [MenuItem("BuildUtility/Build_002_Load")]
    public static void LoadAppInfo_Build_002()
    {
        ClearConsole();

        // Set PlayerSettings with AppInfo
        AppInfo.SetUp("MyCompany", "MyAwesomeGame", true, true);   
        PlayerSettings.companyName = AppInfo.companyName;
        PlayerSettings.productName = AppInfo.productName;
        if (AppInfo.bDisplayResolutionDialog)
            PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Enabled;
        else
            PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.HiddenByDefault;
        PlayerSettings.forceSingleInstance = AppInfo.bForceSingleInstance;

        // Load data from json to set the buildDate and buildCount
        string appInfoDataPath = GetIncludePath(AppInfo.productName + "/AppInfoData.json");
        AppInfo.UpdateBuildDateAndCountFromFile(appInfoDataPath);    

        // Open the build utility window
        BuildUtilWindow.Instance.Init("", GetIncludePath(AppInfo.productName + "/Changelog.txt"));

    }

    public static void Build_002()
    {
        //string buildName = "Game_002" + "_" + AppInfo.buildDateAndCount;
        string buildName = AppInfo.productName + "_" + AppInfo.buildDateAndCount;
        string destinationPath = GenerateBuildPath(buildName);

        CreateDirectoryForBuild(destinationPath);

        // Type required scenes here!
        string[] scenes = { "Assets/Scene/MyScene_002.unity" };
        BuildPipeline.BuildPlayer(scenes, destinationPath + "/" + buildName + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.ShowBuiltPlayer);

        // Overwrite the file to update buildCount!
        AppInfo.UpdateAppInfoData(GetAppInfoDataPath(AppInfo.productName));

        // Find target directory to copy files into it
        DirectoryInfo targetDir = new DirectoryInfo(destinationPath + "/" + buildName + "_Data" + "/StreamingAssets");

        // Copy general files
        string sourcePath_general = GetIncludePath("General");
        DirectoryInfo sourceDir_general = new DirectoryInfo(sourcePath_general);
        BuildFileExt.CopyAllTo(sourceDir_general, targetDir);

        // Copy specific files
        string sourcePath = GetIncludePath(AppInfo.productName);
        DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
        BuildFileExt.CopyAllTo(sourceDir, targetDir);
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
    
    

    public static void ClearConsole()
    {
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
