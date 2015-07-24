using UnityEngine;
using System.Collections;

namespace Assets.Scrip.DesignModels.TemplateMethod
{
    /// <summary>
    /// 模板方法的测试类
    /// </summary>
    public class TemplateMethodClient : MonoBehaviour
    {
        AbstractAnimationController enemyAnimationController;
        AbstractAnimationController heroAnimationController;

        void Start()
        {
            enemyAnimationController = new ConcreteEnemyAnimaionController();
            heroAnimationController = new ConcreteHeroAnimationController();
        }

        void OnGUI()
        {
            if(GUILayout.Button("Enemy"))
            {
                enemyAnimationController.PlayAnimation();
                enemyAnimationController.StopAnimation();
                enemyAnimationController.PauseAnimation();
            }

            if (GUILayout.Button("Hero"))
            {
                heroAnimationController.PlayAnimation();
                heroAnimationController.StopAnimation();
                heroAnimationController.PauseAnimation();
            }

        }
    }
}
