using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public interface IResetable
{
    void Reset();
}

/// <summary>
/// 被管理类型自重置的池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T :class,IResetable,new ()
{
    private Stack<T> m_objectStack;

    private Action<T> m_resetAction;//生成一个带有一个参数的委托
    private Action<T> m_oneTimeInitAction;

    /// <summary>
    /// 带有可选参数的构造函数，可选参数必须位于最后
    /// </summary>
    /// <param name="initialBufferSize">对象池的大小</param>
    /// <param name="ResetAction">重置时的方法</param>
    /// <param name="OneTimeInitAction">初始化时的方法</param>
    public ObjectPool(int initialBufferSize,Action<T> ResetAction = null,Action<T> OneTimeInitAction = null)
    {
        m_objectStack = new Stack<T>(initialBufferSize);
        m_resetAction = ResetAction;
        m_oneTimeInitAction = OneTimeInitAction;
    }

    public T New ()
    {
        if(m_objectStack.Count > 0)
        {
            T t = m_objectStack.Pop();

            //自行重置
            t.Reset();

            if(m_resetAction != null)
            {
                m_resetAction(t);
            }
            return t;
        }
        else
        {
            T t = new T();

            if(m_oneTimeInitAction != null)
            {
                m_oneTimeInitAction(t);
            }

            return t;
        }
    }

    public void Store (T obj)
    {
        m_objectStack.Push(obj);
    }
}

/// <summary>
/// 集体重置池
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPoolWithCollectiveReset<T> where T : class, new()
{
    private List<T> m_objectList;
    private int m_nextAvailableIndex = 0;

    private Action<T> m_resetAction;
    private Action<T> m_onetimeInitAction;

    public ObjectPoolWithCollectiveReset(int initialBufferSize, Action<T>
        ResetAction = null, Action<T> OnetimeInitAction = null)
    {
        m_objectList = new List<T>(initialBufferSize);
        m_resetAction = ResetAction;
        m_onetimeInitAction = OnetimeInitAction;
    }

    public T New()
    {
        if (m_nextAvailableIndex < m_objectList.Count)
        {
            // an allocated object is already available; just reset it
            T t = m_objectList[m_nextAvailableIndex];
            m_nextAvailableIndex++;

            if (m_resetAction != null)
                m_resetAction(t);

            return t;
        }
        else
        {
            // no allocated object is available
            T t = new T();
            m_objectList.Add(t);
            m_nextAvailableIndex++;

            if (m_onetimeInitAction != null)
                m_onetimeInitAction(t);

            return t;
        }
    }

    public void ResetAll()
    {
        //重置索引
        m_nextAvailableIndex = 0;
    }
}