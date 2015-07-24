using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scrip.DesignModels.FactoryMethod
{
    /// <summary>
    /// 工厂方法模式的抽象产品类，由具体的产品类继承实现
    /// </summary>
    public abstract class ResourceManager
    {
        public abstract void LoadConfig(string path);
        public abstract void LoadAsset(string name);
        public abstract void UnLoadResource(bool status);
    }
}
