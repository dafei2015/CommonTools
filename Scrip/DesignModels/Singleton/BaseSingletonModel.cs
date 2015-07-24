using UnityEngine;
using System.Collections;

/// <summary>
/// 定义单例模式的基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSingletonModel<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance;

    public static T Instance
    {
        get 
        {
            if(_instance == null)
            {
                GameObject singleton = new GameObject();
                singleton.name = "singleton";
                _instance = singleton.AddComponent<T>();
                
            }
            return _instance;
        }    
    } 

    public void OnApplicationQuit()
    {
        _instance = null;
    }
	
}
