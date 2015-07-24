using UnityEngine;
using System.Collections;

namespace Assets.Scrip.DesignModels.TemplateMethod
{

    public class ConcreteEnemyAnimaionController:AbstractAnimationController
    {
        public override void PlayAnimation()
        {
            Debug.Log("Enemy_1");
        }

        public override void StopAnimation()
        {
            Debug.Log("Enemy_2");
        }

        public override void PauseAnimation()
        {
            Debug.Log("Enemy_3");
        }
    }
}
