using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrip.DesignModels.SimpleFactory
{
    /// <summary>
    /// Audio 资源管理器，抽象产品的具体产品
    /// 继承抽象类后要实现里面的抽象方法
    /// </summary>
    public class AudioResourceManager :ResouceManager
    {
        public override void LoadConfig(string path)
        {
            Debug.Log("加载和音乐相关的配置文件");
        }

        public override void LoadAsset(string name)
        {
            Debug.Log("加载和音乐文件");
        }

        public override void UnLoadResource(bool status)
        {
            Debug.Log("卸载加载的音乐文件");
        }
    }
}
