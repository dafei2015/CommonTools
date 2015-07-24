using UnityEngine;
using System.Collections;

/// <summary>
/// 定义模板方法的抽象模板
/// </summary>
public abstract class AbstractAnimationController
{
    public abstract void PlayAnimation();
    public abstract void StopAnimation();
    public abstract void PauseAnimation();
}
