using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrip.DesignModels.SimpleFactory
{
    /// <summary>
    /// 使用资源管理器的客户端
    /// </summary>
    class ResouceSimpleFactoryClient:MonoBehaviour
    {
        ResouceSimpleFactory _resouceSimpleFactory;
        void Start()
        {
            _resouceSimpleFactory = new ResouceSimpleFactory();
        }

        void OnGUI()
        {
            if(GUILayout.Button("UI管理器"))
            {
                UIResouceManager _UI = _resouceSimpleFactory.CreateManager(ResourceEnum.UIResource) as UIResouceManager;
                _UI.LoadConfig("http.....");
                _UI.LoadAsset("蛮牛");
                _UI.UnLoadResource(false);
            }

            if(GUILayout.Button("Audio管理器"))
            {
                AudioResourceManager _Audio = _resouceSimpleFactory.CreateManager(ResourceEnum.AudioResource) as AudioResourceManager;
                _Audio.LoadConfig("http.....");
                _Audio.LoadAsset("蛮牛");
                _Audio.UnLoadResource(false);
            }
        }
    }
}
