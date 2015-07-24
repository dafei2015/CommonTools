using UnityEngine;
using UnityEditor;



public class CreateAssetbundlesAll
{
    [MenuItem("Tools/CustomEditor/Create AssetBundles All")]

    static void CreateAssetBundlesAll()
    {
        //没有进行缓存清除 Caching.CleanCache();
        Caching.CleanCache();
        string targetPath = Application.dataPath + "/StreamingAssets/AllAssets.assetbundle";

        Object[] SelectAssets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectAssets)
        {
            Debug.Log("Crate AssetBundles name:" + obj);
        }


        bool flag = false;

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            flag = BuildPipeline.BuildAssetBundle(null, SelectAssets, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iPhone)
        {
            flag = BuildPipeline.BuildAssetBundle(null, SelectAssets, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone);
        }
        else
        {
            //默认的打包只能在电脑端使用
            flag = BuildPipeline.BuildAssetBundle(null, SelectAssets, targetPath, BuildAssetBundleOptions.CollectDependencies);
        }
        if (flag)
        {
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.Log("资源打包失败");
        }
    }

}
