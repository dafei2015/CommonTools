using UnityEngine;
using System.Collections;

namespace Assets.Scrip.DesignModels.TemplateMethod
{
    /// <summary>
    /// 模板方法里面的具体模板-英雄的动画控制
    /// </summary>
    public class ConcreteHeroAnimationController:AbstractAnimationController
    {
        public override void PlayAnimation()
        {
            Debug.Log("hero_1");
        }

        public override void StopAnimation()
        {
            Debug.Log("hero_2");
        }

        public override void PauseAnimation()
        {
            Debug.Log("hero_3");
        }
    }
}
