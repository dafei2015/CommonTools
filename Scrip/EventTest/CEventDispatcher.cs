using System;
using System.Collections;

namespace Assets.Scrip.EventTest
{
    /// <summary>
    /// 对外使用的消息接口
    /// </summary>
    /// 
    public delegate void CEventListenerDelegate(CBaseEvent evt);
    public class CEventDispatcher
    {
        private Hashtable listeners = new Hashtable();
        static CEventDispatcher instance;
        public static CEventDispatcher GetInstance()
        {
            if(instance == null)
            {
                instance = new CEventDispatcher();
            }
            return instance;
        }

        public void DispatchEvent(CBaseEvent evt)
        {
            CEventListenerDelegate ceventListenerDelegate = this.listeners[evt.Type] as CEventListenerDelegate;
            if(ceventListenerDelegate != null)
            {
                try
                {
                    ceventListenerDelegate(evt);
                }
                catch(Exception ex)
                {
                    throw new Exception(string.Concat(new string[]
                        {
                            "Error Dispatching Event",
                            evt.Type.ToString(),
                            ":",
                            ex.Message,
                            " ",
                            ex.StackTrace
                        }), ex);
                }
            }
        }
        

        /// <summary>
        /// 添加事件的监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="listener"></param>
        public void AddEventListener(CEventType eventType,CEventListenerDelegate listener)
        {
            CEventListenerDelegate ceventListenerDelegate = this.listeners[eventType] as CEventListenerDelegate;
            ceventListenerDelegate = (CEventListenerDelegate)Delegate.Combine(ceventListenerDelegate, listener);
            this.listeners[eventType] = ceventListenerDelegate;
        }

        /// <summary>
        /// 移出事件的监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="listener"></param>
        public void RemoveEventListener(CEventType eventType,CEventListenerDelegate listener)
        {
            CEventListenerDelegate ceventListenerDelegate = this.listeners[eventType] as CEventListenerDelegate;
            if(ceventListenerDelegate != null)
            {
                ceventListenerDelegate = (CEventListenerDelegate)Delegate.Remove(ceventListenerDelegate, listener);
            }
            
            this.listeners[eventType] = ceventListenerDelegate;
        }

        public void RemoveAll()
        {
            this.listeners.Clear();
        }
    }
}
