using System;
using UnityEngine;
namespace Assets.Scrip.DesignModels.FactoryMethod
{
    /// <summary>
    /// 工厂方法模式抽象产品的具体产品实现  音频资源管理产品类
    /// </summary>
    class AudioResouceManager : ResourceManager
    {
        public override void LoadConfig(string path)
        {
            Debug.Log("加载音乐相关的配置文件");
        }

        public override void LoadAsset(string name)
        {
            Debug.Log("加载音乐文件");
        }

        public override void UnLoadResource(bool status)
        {
            Debug.Log("卸载加载的音乐文件");
        }
    }
}
