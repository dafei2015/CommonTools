using System;
using UnityEngine;

namespace Assets.Scrip.DesignModels.FactoryMethod
{
    /// <summary>
    /// 客户端测试类
    /// </summary>
    class ResouceFactoryMethodClient:MonoBehaviour
    {
        #region 定义具体产品工厂
        CreatorResourceFactory audioFactory;
        CreatorResourceFactory uiFactory;
        #endregion

        #region 定义具体产品
        AudioResouceManager audioManager;
        UIResouceManager uiManager;
        #endregion

        void Start()
        {
            audioFactory = new AudioResouceManagerFactory();
            uiFactory = new UIResouceManagerFactory();
        }

        void OnGUI()
        {
            if(GUILayout.Button("音乐管理器"))
            {
                audioManager = audioFactory.CreateFactory() as AudioResouceManager;
                audioManager.LoadConfig("http....");
                audioManager.LoadAsset("蛮牛");
                audioManager.UnLoadResource(false);
            }
        }
    }
}
