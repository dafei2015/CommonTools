using System;
using UnityEngine;

namespace Assets.Scrip.DesignModels.FactoryMethod
{
    /// <summary>
    /// 简单方法模式的抽象工厂，由具体的工厂来继承实现
    /// </summary>
    public abstract class CreatorResourceFactory
    {
        public abstract ResourceManager CreateFactory();
    }
}
