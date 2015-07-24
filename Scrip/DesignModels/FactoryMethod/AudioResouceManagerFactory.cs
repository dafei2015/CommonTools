using System;
using UnityEngine;

namespace Assets.Scrip.DesignModels.FactoryMethod
{
    public class AudioResouceManagerFactory : CreatorResourceFactory
    {
        /// <summary>
        /// 简单方法模式里面的具体产品音频资源管理工厂
        /// </summary>
        /// <returns></returns>
        public override ResourceManager CreateFactory()
        {
            return new AudioResouceManager();
        }
    }
}
