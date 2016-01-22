using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFClient
{
    public partial class MainForm : Form, CallbacksServiceReference.ICallbacksServiceCallback, IDisposable
    {
        /// <summary>
        /// 基础服务的客户端类
        /// </summary>
        BaseServiceReference.BaseServiceClient baseServiceClient;
        /// <summary>
        /// 服务器回调的客户端类
        /// </summary>
        CallbacksServiceReference.CallbacksServiceClient callbacksServiceClient;
        public MainForm()
        {
            InitializeComponent();
            baseServiceClient = new BaseServiceReference.BaseServiceClient();   //初始化基础服务客户端类
            callbacksServiceClient = new CallbacksServiceReference.CallbacksServiceClient(new InstanceContext(this));   //初始化服务器回调客户端类
            RegCallBack();
        }

        public void NotifyClientMsg(string msg)
        {
            label4.Text = msg;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            int a = 0, b = 0;
            if (!int.TryParse(txtAdda.Text, out a))
            {
                MessageBox.Show("请填写正确格式的被加数。");
            }
            if (!int.TryParse(txtAddb.Text, out b))
            {
                MessageBox.Show("请填写正确格式的加数。");
            }
            label6.Text = baseServiceClient.Addition(a, b).ToString();
        }

        /// <summary>
        /// 向服务端注册回调事件。
        /// 即监听服务端向客户端发送的命令。
        /// </summary>
        private void RegCallBack()
        {
            try
            {
                callbacksServiceClient.MonitorServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        new void Dispose()
        {
            try
            {
                //callbacksServiceClient.UnMonitorServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
