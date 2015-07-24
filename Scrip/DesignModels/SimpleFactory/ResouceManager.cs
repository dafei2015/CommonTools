using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scrip.DesignModels.SimpleFactory
{
    /// <summary>
    /// 资源管理基类，抽象产品
    /// </summary>
    public abstract class ResouceManager
    {
        public abstract void LoadConfig(string path);
        public abstract void LoadAsset(string name);
        public abstract void UnLoadResource(bool status);
    }
}
