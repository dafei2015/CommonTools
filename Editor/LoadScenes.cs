using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class LoadScenes : Editor {

	[MenuItem("Tools/Load Scene To SceneSetting")]
    static void CheckSceneSetting ()
    {
        List<string> dirs = new List<string>();
        GetDirs(Application.dataPath, ref dirs);
        EditorBuildSettingsScene[] newSettings = new EditorBuildSettingsScene[dirs.Count];
        for(int i= 0;i<newSettings.Length;i++)
        {
            newSettings[i] = new EditorBuildSettingsScene(dirs[i], true);
        }
        EditorBuildSettings.scenes = newSettings;
        EditorApplication.SaveAssets();
    }

    private static void GetDirs(string dirPath, ref List<string> dirs)
    {
        foreach(string path in Directory.GetFiles(dirPath))
        {
            if(Path.GetExtension(path)==".unity")
            {
                dirs.Add(path.Substring(path.IndexOf("Assets\\")));
            }
        }

        if(Directory.GetDirectories(dirPath).Length>0)
        {
            foreach(string path in Directory.GetDirectories(dirPath))
            {
                GetDirs(path,ref dirs);
            }
        }
    }
    
}
