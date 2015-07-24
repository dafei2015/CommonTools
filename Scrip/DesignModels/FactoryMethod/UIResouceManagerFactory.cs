using System;
using UnityEngine;

namespace Assets.Scrip.DesignModels.FactoryMethod
{
    public class UIResouceManagerFactory : CreatorResourceFactory
    {
        public override ResourceManager CreateFactory()
        {
            return new UIResouceManager();
        }
    }
}
