using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFService
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 提供基础服务的主机
        /// </summary>
        ServiceHost baseService = new ServiceHost(typeof(BaseService));
        /// <summary>
        /// 提供回调客户端服务
        /// </summary>
        ServiceHost callbacksService = new ServiceHost(typeof(CallbacksService));

        public MainForm()
        {
            InitializeComponent();
            BaseHostServiceStart();
            CallBackServiceStart();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            BaseHostServiceStart();    //开启基础服务
            CallBackServiceStart();      //开启服务端回调客户端服务
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            BaseHostServcieClose();   //关闭基础服务
            CallBackServcieClose();     //关闭服务端回调客户端服务
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (CallbacksService.listIMessageCallBack == null || CallbacksService.listIMessageCallBack.Count == 0)
            {
                ShowMsgControlText("没有客户端向服务端注册回调。");
            }
            else
            {
                //本行代码的意思是通知每一个向服务端注册回调的客户端，我发送消息了。
                CallbacksService.listIMessageCallBack.ForEach(IMessageCallback =>
                {
                    IMessageCallback.NotifyClientMsg(txtSendMsg.Text);
                });
            }
        }

        /// <summary>
        /// 启动一个基础服务
        /// </summary>
        private void BaseHostServiceStart()
        {
            try
            {
                baseService = new ServiceHost(typeof(BaseService));
                NetTcpBinding binding = new NetTcpBinding();
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.SendTimeout = new TimeSpan(0, 0, 30, 0);
                binding.TransferMode = TransferMode.Streamed;
                binding.Security.Mode = SecurityMode.None;
                Uri callBackAddress = new Uri(ToolClass.GetAppConfig("BaseAddressTcp"));
                baseService.AddServiceEndpoint(typeof(IBaseService), binding, callBackAddress);
                ServiceMetadataBehavior metadataBehavior;
                metadataBehavior = baseService.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (metadataBehavior == null)
                {
                    metadataBehavior = new ServiceMetadataBehavior();
                    metadataBehavior.HttpGetEnabled = true;
                    metadataBehavior.HttpGetUrl = new Uri(ToolClass.GetAppConfig("BaseAddress"));
                    baseService.Description.Behaviors.Add(metadataBehavior);
                }
                baseService.Open();
                ShowMsgControlText("基础服务启动成功。");
            }
            catch (Exception ex)
            {
                ShowMsgControlText(ex.Message);
                baseService.Abort();
            }
        }

        /// <summary>
        /// 关闭基础服务
        /// </summary>
        private void BaseHostServcieClose()
        {
            try
            {
                baseService.Close();
                ShowMsgControlText("基础服务停止。");
            }
            catch (Exception ex)
            {
                ShowMsgControlText(ex.Message);
                baseService.Abort();
            }
        }
        /// <summary>
        /// 启动服务器回调客户端服务
        /// </summary>
        private void CallBackServiceStart()
        {
            try
            {
                callbacksService = new ServiceHost(typeof(CallbacksService));
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;
                Uri callBackAddress = new Uri(ToolClass.GetAppConfig("CallbacksAddressTcp"));
                callbacksService.AddServiceEndpoint(typeof(ICallbacksService), binding, callBackAddress);
                ServiceMetadataBehavior metadataBehavior;
                metadataBehavior = callbacksService.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (metadataBehavior == null)
                {
                    metadataBehavior = new ServiceMetadataBehavior();
                    metadataBehavior.HttpGetEnabled = true;
                    metadataBehavior.HttpGetUrl = new Uri(ToolClass.GetAppConfig("CallbacksAddress"));
                    callbacksService.Description.Behaviors.Add(metadataBehavior);
                }
                callbacksService.Open();
                ShowMsgControlText("回调客户端服务启动成功。");
            }
            catch (Exception ex)
            {
                ShowMsgControlText(ex.Message);
                callbacksService.Abort();
            }
        }
        /// <summary>
        /// 关闭服务器回调客户端服务
        /// </summary>
        private void CallBackServcieClose()
        {
            try
            {
                callbacksService.Close();
                ShowMsgControlText("回调客户端服务停止。");
            }
            catch (Exception ex)
            {
                ShowMsgControlText(ex.Message);
                callbacksService.Abort();
            }
        }

        /// <summary>
        /// 向界面控件添加内容
        /// </summary>
        /// <param name="msg">在控件上显示的消息</param>
        private void ShowMsgControlText(string msg)
        {
            richTextBox1.AppendText(DateTime.Now.ToString() + " " + msg + "\r\n");
        }
    }
}
