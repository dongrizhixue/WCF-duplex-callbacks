using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ICallbacksService”。
    [ServiceContract(CallbackContract = typeof(IMessageCallBack))]
    public interface ICallbacksService
    {
        /// <summary>
        /// 注册消息类
        /// </summary>
        [OperationContract]
        void MonitorServer();
        /// <summary>
        /// 注销消息类
        /// </summary>
        [OperationContract]
        void UnMonitorServer();
    }
}
