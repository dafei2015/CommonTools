using UnityEngine;
using System.Collections;
using UnityEditor;

public class CheckNullMeshAndAnimator : Editor {
    
    //所有的对象必须是active = true；
    [MenuItem("Tools/Delete Null Mesh And Animation")]
    public static void Remove()
    {
        //获取当前游戏的所有对象
        GameObject[] rootObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        //遍历游戏对象
        foreach(GameObject go in rootObjects)
        {
            //如果发现Render的shader是Diffuse并且颜色是白色，那么将它的shader修改成Mobile/Diffuse
		    if(go!= null && go.transform.parent !=null)
            {
                Renderer render = go.GetComponent<Renderer>();
                if(render != null && render.sharedMaterial!=null && render.sharedMaterial.shader.name == "Diffuse")
                {
                    render.sharedMaterial.shader = Shader.Find("Mobile/Diffuse");
                }
            }

            //删除所有的MeshCollider
            foreach(MeshCollider collider in FindObjectsOfType(typeof(MeshCollider)))
            {
                DestroyImmediate(collider);
            }

            //删除没有用的动画组件
            foreach(Animation animation in FindObjectsOfType(typeof(Animation)))
            {
                if (animation.clip == null)
                    DestroyImmediate(animation);
            }

            //删除没用的Animator 
            foreach(Animator animator in FindObjectsOfType(typeof(Animator)))
            {
                DestroyImmediate(animator);
            }
        }

        //保存
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/Delete All MeshCollider And Animation")]
    public static void RemovaAll()
    {
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if(scene.enabled)
            {
                //打开这个场景
                EditorApplication.OpenScene(scene.path);
                Remove();
            }
            EditorApplication.SaveScene();
        }
        
    }
}
