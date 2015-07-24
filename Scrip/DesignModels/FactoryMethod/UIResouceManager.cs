using System;
using UnityEngine;
namespace Assets.Scrip.DesignModels.FactoryMethod
{
    /// <summary>
    /// 工厂方法模式中的抽象产品的具体实现产品
    /// </summary>
    class UIResouceManager:ResourceManager
    {
        public override void LoadConfig(string path)
        {
            Debug.Log("加载UI资源配置文件");
        }

        public override void LoadAsset(string name)
        {
            Debug.Log("加载UI文件"); ;
        }

        public override void UnLoadResource(bool status)
        {
            Debug.Log("卸载加载的UI文件");
        }
    }
}
