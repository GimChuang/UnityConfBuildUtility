using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;
using System;

public static class BuildFileExt {

    public static void CopyTo(string _sourceFilePath, string _targetFilePath)
    {
        Debug.Log("_targetFilePath: " + _targetFilePath);

        // Path.GetDirectoryName returns the directory information for the specified path string
        string targetDir = Path.GetDirectoryName(_targetFilePath);
        Debug.Log("targetDir: " + targetDir);

        if (!File.Exists(_sourceFilePath))
        {
            Debug.LogError("File Not Exists: " + _sourceFilePath);
        }

        if (!Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
            Debug.LogWarning("targetDir Not Exists. Created One!");
        }

        FileInfo targetFile = new FileInfo(_targetFilePath);
        if (targetFile.Exists)
        {
            targetFile.IsReadOnly = false;
            Debug.LogWarning("targetFile Already Exists. Will Overwrite It.");
        }

        File.Copy(_sourceFilePath, _targetFilePath, true);
    }

    public static void CopyAllTo(DirectoryInfo _source, DirectoryInfo _target)
    {
        if (_source.FullName.ToLower() == _target.FullName.ToLower())
        {
            Debug.LogError("The source directoryInfo is the same with the target directoryInfo.");
            return;
        }
            
        if(!Directory.Exists(_target.FullName))
        {
            Directory.CreateDirectory(_target.FullName);
            Debug.LogWarning("Target directory not exists. Created One!");
        }

        foreach(FileInfo fileInfo in _source.GetFiles("*", SearchOption.AllDirectories))
        {
            fileInfo.CopyTo(Path.Combine(_target.FullName, fileInfo.Name), true);
        }

    }

    public static void MakeFilesWritable(DirectoryInfo _dir)
    {
        if (!_dir.Exists)
        {
            Debug.LogError("Directory Not Exists: " + _dir);
            return;
        }          

        FileInfo[] files = _dir.GetFiles("*", SearchOption.AllDirectories);
        foreach (FileInfo file in files)
        {
            file.IsReadOnly = false;
        }
    }

    public static void EditChangelog(string _filePath, string _newChangelog)
    {
        if (!File.Exists(_filePath))
        {
            Debug.LogError("Changelog file not exists: " + _filePath);
        }

        // Read old changelog texts
        StringBuilder builder = new StringBuilder();
        List<string> originalTexts = File.ReadAllLines(_filePath).ToList();

        // Add new changelog texts
        builder.Append(AppInfo.currentDateTimeString);
        builder.AppendLine("  [" + AppInfo.productName + "_" + AppInfo.buildDateAndCount + "]");
        builder.AppendLine(_newChangelog);

        builder.AppendLine("==============================================");

        // Append old changelog texts
        for (int i = 0; i < originalTexts.Count; i++)
        {
            builder.AppendLine(originalTexts[i]);
        }

        // Overwrite the file
        File.WriteAllText(_filePath, builder.ToString());
    }
}
