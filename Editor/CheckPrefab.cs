using UnityEngine;
using System.Collections;
using UnityEditor;

public class CheckPrefab : Editor
{
	[MenuItem("Tools/Check Prefab Use")]       
    private static void OnSearchForReferences()
    {
        //确保鼠标右键点击的是一个Prefab
        if(Selection.gameObjects.Length !=1)
        {
            Debug.Log("请选择Prefab");
            return;
        }

        //遍历所有游戏场景
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if(scene.enabled)
            {
                //打开场景
                EditorApplication.OpenScene(scene.path);
                //获取场景中的所有对象
                GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
                foreach(GameObject go in gos)
                {
                    //判断GameObject是否为一个Prefab的引用
                    if(PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
                    {
                        Object parentObject = PrefabUtility.GetPrefabParent(go);
                        string path = AssetDatabase.GetAssetPath(parentObject);
                        //判断GameObject的prefab是否和右键选择的prefab是同一路径
                        if(path == AssetDatabase.GetAssetPath(Selection.activeGameObject))
                        {
                            //输出场景名，以及prefab引用路径
                            Debug.Log(scene.path + "  " + GetGameObjectPath(go));
                        }

                    }
                }
            }
        }

    }
    /// <summary>
    /// 获得物体的路径
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    private static string GetGameObjectPath(GameObject go)
    {
        string path = "/" + go.name;
        while(go.transform.parent != null)
        {
            go = go.transform.parent.gameObject;
            path = "/" + go.name + path;
        }
        return path;
        throw new System.NotImplementedException();
    }
 
}
