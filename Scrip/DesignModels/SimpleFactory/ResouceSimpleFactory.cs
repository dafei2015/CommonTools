using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scrip.DesignModels.SimpleFactory
{
    /// <summary>
    /// 简单资源工厂类，工厂类角色，负责创建UI，Audio等管理器的实例
    /// </summary>
    public class ResouceSimpleFactory
    {
        /// <summary>
        /// 方式1，根据type创建相应的管理器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ResouceManager CreateManager(string type)
        {
            if(type == "UI")
            {
                return new UIResouceManager();
            }
            else if(type == "Audio")
            {
                return new AudioResourceManager();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据定义的枚举类型创建资源
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public ResouceManager CreateManager(ResourceEnum re)
        {
            switch (re)
            {
                case ResourceEnum.UIResource:
                    return new UIResouceManager();
                case ResourceEnum.AudioResource:
                    return new AudioResourceManager();
                default:
                    return null;
            }
                
        }

    }
}
