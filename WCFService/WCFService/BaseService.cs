using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“BaseService”。
    public class BaseService : IBaseService
    {
        /// <summary>
        /// 一个简单的加法运算
        /// </summary>
        /// <param name="a">被加数</param>
        /// <param name="b">加数</param>
        /// <returns>运算结果</returns>
        public int Addition(int a, int b)
        {
            return a + b;
        }
    }
}
