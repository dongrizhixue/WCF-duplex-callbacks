using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“CallbacksService”。
    public class CallbacksService : ICallbacksService
    {
        /// <summary>
        /// 存储多个客户端的回调
        /// </summary>
        public static List<IMessageCallBack> listIMessageCallBack = new List<IMessageCallBack>();
        /// <summary>
        /// 客户端调用这个方法，将向服务端注册一个回调
        /// </summary>
        public void MonitorServer()
        {
            var callback = OperationContext.Current.GetCallbackChannel<IMessageCallBack>();
            listIMessageCallBack.Add(callback);
        }
        /// <summary>
        /// 客户端调用这个方法，将注销
        /// </summary>
        public void UnMonitorServer()
        {
            //这个注销的方法没有起作用
            var callback = OperationContext.Current.GetCallbackChannel<IMessageCallBack>();
            listIMessageCallBack.Remove(callback);
        }
    }
}
