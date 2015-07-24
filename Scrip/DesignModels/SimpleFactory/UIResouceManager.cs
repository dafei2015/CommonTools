using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrip.DesignModels.SimpleFactory
{
    /// <summary>
    /// UI资源管理器，抽象产品的具体产品
    /// </summary>
    public class UIResouceManager:ResouceManager
    {
        public override void LoadConfig(string path)
        {
            Debug.Log("加载UI的配置文件");
        }

        public override void LoadAsset(string name)
        {
            Debug.Log("加载UI的资源");
        }

        public override void UnLoadResource(bool status)
        {
            Debug.Log("卸载加载的UI的资源");
        }
    }
}
