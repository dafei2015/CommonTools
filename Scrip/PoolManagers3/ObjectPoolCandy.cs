using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolCandy : MonoBehaviour 
{
    public GameObject[] _prefabs;

    Dictionary<string, GameObject> _prefabsPool;
    Dictionary<string, Queue<Object>> _objectPool;

    static ObjectPoolCandy _instance;
    public static ObjectPoolCandy Instance
    {
        get
        {
            if(_instance == null)
            {
                var obj = new GameObject("ObjectPool");
                _instance = obj.AddComponent<ObjectPoolCandy>();
            }

            return _instance;
        }
    }
	// Use this for initialization
	void Awake () 
    {
        _instance = this;
        _objectPool = new Dictionary<string,Queue<Object>>();
        _prefabsPool = new Dictionary<string,GameObject>();

        if(_prefabs != null && _prefabs.Length>0)
        {
            foreach(var prefab in _prefabs)
            {
                _prefabsPool.Add(prefab.name, prefab);
            }
        }
	    
	}
	
    Queue<Object> GetPool(string ObjectID)
    {
        if(!Instance._objectPool.ContainsKey(ObjectID))
        {
            var queue = new Queue<Object>();
            Instance._objectPool.Add(ObjectID, queue);
            return queue;
        }
        else
        {
            return Instance._objectPool[ObjectID];
        }
    }
	

    public static Object Instantiate(string ObjectID)
    {
        Queue<Object> queue = Instance.GetPool(ObjectID);

        Object obj = null;
        if(queue.Count >0)
        {
            obj = queue.Dequeue();
        }
        else if (Instance._prefabsPool.ContainsKey(ObjectID))
        {
            obj = Instantiate(Instance._prefabsPool[ObjectID]);
            queue.Enqueue(obj);
        }

        if(obj && obj is GameObject)
        {
            (obj as GameObject).SetActive(true);
        }

        return obj;
    }

    public static void Destroy(Object Obj, string ObjectID = null)
    {
        if (Obj is GameObject)
        {
            (Obj as GameObject).SetActive(false);
        }

        if (string.IsNullOrEmpty(ObjectID))
        {
            ObjectID = Obj.name;
        }

        Instance.GetPool(ObjectID).Enqueue(Obj);
    }

    public static void Destroy(MonoBehaviour behaviour, string ObjectID = null)
    {
        Destroy(behaviour.gameObject, ObjectID);
    }
}
