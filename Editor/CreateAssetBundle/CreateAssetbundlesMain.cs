using UnityEngine;
using UnityEditor;

public class CreateAssetbundlesMain 
{
    [MenuItem("Tools/CustomEditor/Create AssetBunldes Main")]

    static  void CreateAssetBundlesMain()
    {
        //获取在Project视图中选择的所有游戏对象
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        //遍历所有的游戏对象
        foreach (Object obj in SelectedAsset)
        {
            string sourcePath = AssetDatabase.GetAssetPath(obj);

            //建议在本地的，最后将Assetbundle放在StreamingAssets文件夹下，以为移动平台下只能读取这个路径
            //且StreamingAssets是只读路径，不能写入
            //服务器端的话就不需要放在这里，直接用WWW类下载即可

            string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";
           
            bool flag = false;

            if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                flag = BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android);
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iPhone)
            {
                flag = BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone);
            }
            else
            {
                //默认的打包只能在电脑端使用
                flag = BuildPipeline.BuildAssetBundle(obj,null,targetPath,BuildAssetBundleOptions.CollectDependencies);
            }
            if(flag)
            {
                Debug.Log(obj.name + "资源打包成功");
            }
            else
            {
                Debug.Log(obj.name + "资源打包失败");
            }
        }

        AssetDatabase.Refresh();//刷新编辑器
    }
}
