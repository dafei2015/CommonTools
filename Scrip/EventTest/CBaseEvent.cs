using System;
using System.Collections;

namespace Assets.Scrip.EventTest
{
    /// <summary>
    /// 基类消息事件，只提供简单的数值以及构造方法
    /// </summary>
    public class CBaseEvent
    {
        private Hashtable arguments;
        private CEventType type;
        protected object sender;
        
        
        public CEventType Type
        {
            get { return type; }
            set { type = value; }
        }

        public IDictionary Params
        {
            get { return arguments; }
            set { arguments = value as Hashtable; }
        }

        public object Sender
        {
            get { return this.sender; }
            set { this.sender = value;}
        }

        public override string ToString()
        {
            return this.type + "{" + ((this.sender == null) ? "null" : this.sender.ToString()) + "}";
        }

        public CBaseEvent(CEventType type, object sender, Hashtable arge = null)
        {
            // TODO: Complete member initialization
            this.Type = type;
            this.Params = arge;
            this.Sender = sender;
            if(this.arguments == null)
            {
                this.arguments = new Hashtable();
            }
        }

        public CBaseEvent Clone()
        {
            return new CBaseEvent(this.type, this.sender,this.arguments);
        }

        
    }
}
