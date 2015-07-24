using UnityEngine;
using System;
using UnityEditor;

public class AutoSave : EditorWindow {

    private bool autoSaveScene = true;
    private bool showMessage = true;
    private bool isStarted = false;
    private int intervalScene;
    private DateTime lastSavaTimeScene = DateTime.Now;

    private string projectPath = Application.dataPath;
    private string scenePath;

    [MenuItem("Tools/AutoSave")]
    static void Init()
    {
        AutoSave saveWindow = (AutoSave)EditorWindow.GetWindow(typeof(AutoSave));
        saveWindow.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Info:", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Saving to: ", projectPath);
        EditorGUILayout.LabelField("Saving Scene: ", scenePath);
        GUILayout.Label("Options:", EditorStyles.boldLabel);
        autoSaveScene = EditorGUILayout.BeginToggleGroup("Auto Save", autoSaveScene);
        intervalScene = EditorGUILayout.IntSlider("Interval(minutes)", intervalScene, 1, 10);

        if(isStarted)
        {
            EditorGUILayout.LabelField("Last Save: ", lastSavaTimeScene.ToShortTimeString());
        }
        EditorGUILayout.EndToggleGroup();

        showMessage = EditorGUILayout.BeginToggleGroup("Show Message: ", showMessage);
        EditorGUILayout.EndToggleGroup();
    }

    void Update()
    {
        scenePath = EditorApplication.currentScene;
        if(autoSaveScene)
        {
            if (DateTime.Now.Minute >= (lastSavaTimeScene.Minute + intervalScene) || DateTime.Now.Minute == 59 && DateTime.Now.Second == 59)
                SaveScene();
            else
                isStarted = false;
        }
        
    }

    private void SaveScene()
    {
        EditorApplication.SaveScene(scenePath);
        lastSavaTimeScene = DateTime.Now;
        isStarted = true;
        if(showMessage)
        {
            Debug.Log("AutoSave saved: " + scenePath + " on " + lastSavaTimeScene);
        }
        AutoSave repaintSaveWindow = (AutoSave)EditorWindow.GetWindow(typeof(AutoSave));
        repaintSaveWindow.Repaint();
    }
}
