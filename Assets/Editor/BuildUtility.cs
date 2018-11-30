using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildUtility {

    // Path.GetDirectoryName returns the directory information for the specified path string
    // the string projectPath will be the directory which contains the Assets and ProjectSettings folders
    static string projectPath = Path.GetDirectoryName(Application.dataPath);

    [MenuItem("BuildTool/Build")]
    static void Build_001()
    {
        // TODO Clear Console Log

        // Build
        string buildName = "Game_001";
        string destinationPath = GenerateBuildPath(buildName);

        CreateDirectoryForBuild(destinationPath);

        string[] scenes = { "Assets/Scene/MyScene.unity" };
        BuildPipeline.BuildPlayer(scenes, 
            destinationPath + "/" + buildName + ".exe", 
            BuildTarget.StandaloneWindows64, 
            BuildOptions.ShowBuiltPlayer);


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
}
