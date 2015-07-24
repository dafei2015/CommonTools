using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;


namespace Assets.Scrip.FSM
{
    /// <summary>
    /// 对外接口，有限状态机
    /// </summary>
    public class FStateMachine
    {
        //声明一个委托用于状态直接的切换
        public delegate void OnStateChangeDelegate(int fromStateName, int toStateName);
        private List<FSMState> states = new List<FSMState>();
        private volatile int currentStateName; //volatile 用于可以多线程访问，并随时呈现最新值
       // public FiniteStateMachine.OnStateChangeDelegate onStateChange;
        private object locker = new object();

        //增加状态
        public void AddState(object st)
        {
            int stateName = (int)st;
            FSMState fSMState = new FSMState();
            fSMState.SetStateName(stateName);
            this.states.Add(fSMState);
        }

        /// <summary>
        /// 增加所有状态
        /// </summary>
        /// <param name="statesEnumtype"></param>
        public void AddAllStates(Type statesEnumtype)
        {
            IEnumerator enumerator = Enum.GetValues(statesEnumtype).GetEnumerator();
            try
            {
                while(enumerator.MoveNext())
                {
                    object current = enumerator.Current;
                    this.AddState(current);
                }
            }
            finally 
            {

                IDisposable disposable;
                if((disposable = (enumerator as IDisposable))!=null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
