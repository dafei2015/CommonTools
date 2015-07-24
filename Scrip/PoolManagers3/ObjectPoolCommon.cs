using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolCommon : MonoBehaviour 
{
    /// <summary>
    /// 所使用的元素Prefab
    /// </summary>
    public GameObject objPrefab;

    /// <summary>
    /// 初始容量
    /// </summary>
    public int InitialCapacity;

    /// <summary>
    /// 初始下标
    /// </summary>
    private int _startCapacityIndex;

    /// <summary>
    /// 可用下标
    /// </summary>
    private List<int> _avaliableIndex;

    /// <summary>
    /// 池中全部元素
    /// </summary>
    private Dictionary<int, GameObject> _totalObjList;

    void Start()
    {
        _avaliableIndex = new List<int>(InitialCapacity);
        _totalObjList = new Dictionary<int, GameObject>(InitialCapacity);
        ExpandPool();
    }

    /// <summary>
    /// 取得一个物体，返回值 1,obj代表，ID是1的物体被取到，ID可以用来归还物体的时候用到
    /// </summary>
    /// <returns></returns>
    public KeyValuePair<int,GameObject> PickObj()
    {
        if (_avaliableIndex.Count == 0)
            ExpandPool();

        int id = _avaliableIndex[0];
        _avaliableIndex.Remove(id);

        _totalObjList[id].SetActive(true);
        return new KeyValuePair<int, GameObject>(id, _totalObjList[id]);
    }

    /// <summary>
    /// 从池中取出元素，在制定时间后回收
    /// </summary>
    /// <param name="existSecond"></param>
    /// <returns></returns>

    public KeyValuePair<int,GameObject> PickObjWithDelayRecyle(float existSecond)
    {
        KeyValuePair<int, GameObject> obj = PickObj();
        StartCoroutine(startRecycleExplosion(obj.Key, existSecond));
        return obj;
    }

    /// <summary>
    /// 回收一个物体
    /// </summary>
    /// <param name="id"></param>
    public void RecyleObj(int id)
    {
        _totalObjList[id].SetActive(false);
        _totalObjList[id].transform.parent = transform;
        _avaliableIndex.Add(id);
    }

    IEnumerator startRecycleExplosion(int id, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        RecyleObj(id);
    }

    /// <summary>
    /// 扩展池
    /// </summary>
    private void ExpandPool()
    {
        int start = _startCapacityIndex;
        int end = _startCapacityIndex + InitialCapacity;

        for (int i = start; i < end ; i ++)
        {
            //加入验证判断，避免在多个请求同时触发扩展池需求

            if (_totalObjList.ContainsKey(i))
                continue;

            GameObject newObj = Instantiate(objPrefab) as GameObject;
            newObj.SetActive(false);
            _avaliableIndex.Add(i);
            _totalObjList.Add(i,newObj);
        }

        _startCapacityIndex = end;
    }
}
