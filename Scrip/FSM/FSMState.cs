using System;
using System.Collections.Generic;


namespace Assets.Scrip.FSM
{
    public class FSMState
    {
        private int stateName;
        private Dictionary<int, int> transitions = new Dictionary<int, int>();

        /// <summary>
        /// 设置状态的名字
        /// </summary>
        /// <param name="newStateName"></param>
        public void SetStateName(int newStateName)
        {
            this.stateName = newStateName;
        }

        /// <summary>
        /// 获取状态的名字
        /// </summary>
        /// <returns></returns>
        //public int GetStateName()
        //{
        //    return this.stateName;
        //}

        /// <summary>
        /// 加入状态
        /// </summary>
        /// <param name="transition"></param>
        /// <param name="outputState"></param>
        public void AddTransition(int transition,int outputState)
        {
            this.transitions[transition] = outputState;
        }

        /// <summary>
        /// 使用状态
        /// </summary>
        /// <param name="transition"></param>
        /// <returns></returns>
        public int ApplyTransition(int transition)
        {
            int result = this.stateName;
            if(this.transitions.ContainsKey(transition))
            {
                result = this.transitions[transition];
            }
            return result;
        }
    }
}
