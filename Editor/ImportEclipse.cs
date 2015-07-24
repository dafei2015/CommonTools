using UnityEngine;
using System.Collections;
using UnityEditor;

public class ImportEclipse : Editor {

	[MenuItem("Tools/Build Google Project")]
    static public void BuildAssetBundles()
    {
        BuildPipeline.BuildPlayer(new string[] { "Assets/Scene/Main.unity" }, Application.dataPath + "/../", BuildTarget.Android, BuildOptions.AcceptExternalModificationsToPlayer);
    }
	
	
}
