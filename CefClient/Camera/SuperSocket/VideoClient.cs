using CefClient.Camera;
using CefClient.Camera.CHCNETSDK;
using CefClient.Camera.ConnectServer;
using CefClient.CarVideo;
using CefClient.SuperSocket;
using CefSharp;
using CefSharp.CarVideo;
using Jt808Library.JT808PacketBody;
using SuperSocket.ClientEngine;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace CefClient
{
    public class VideoClient
    {
        private int m_lPort = 1;
        private EasyClient<PackageInfo> client;
        private string ip = ConfigurationManager.AppSettings["orderServer"];
   
        public async void ConnectServer()
        {
            client = new EasyClient<PackageInfo>
            {
                ReceiveBufferSize =8192,
              
            };
            client.Initialize(new ReceiveFilter());
            client.Connected += OnClientConnected;
            client.NewPackageReceived += OnPackageReceived;
            client.Error += OnClientError;
            client.Closed += OnClientClosed;
            var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(ip), 8091));
            if (!connected&& StaticResource.CameraVideoIsEnd)
            {
                StaticResource.ShowMessage("连接失败：原因（1）服务器离线");
            }
        }
        public EasyClient<PackageInfo> GetClient()
        {
            return client;
        }
        private void OnPackageReceived(object sender, PackageEventArgs<PackageInfo> e)
        {
            StaticResource.CameraOriginalvideo.Enqueue(e.Package.Data);
        }

        private void OnClientConnected(object sender, EventArgs e)
        {
            new Thread(new Parse().HkPsParse).Start();
            Send(StaticResource.CameraInfo.Concat(new byte[] { 11,22,33,44}).ToArray());
            new Thread(RealDataCallBack).Start();
        }


        public  void RealDataCallBack()
        {
           
            //获取播放句柄 Get the port to play
            if (PlayCtrl.PlayM4_GetPort(ref m_lPort) == false)
            {
                uint nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
              //  LogHelper.WriteLog("1错误码" + nLastErr.ToString());
                // MessageBox.Show("PlayM4_GetPort" + nLastErr.ToString());
                return;
            }
            //设置流播放模式 Set the stream mode: real-time stream mode
            if (PlayCtrl.PlayM4_SetStreamOpenMode(m_lPort, 0) == false)
            {
                uint nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
             //   LogHelper.WriteLog("2错误码" + nLastErr.ToString());
                // MessageBox.Show("PlayM4_SetStreamOpenMode" + nLastErr.ToString());
                return;
            }
            ValueTuple<IntPtr, uint> ValueTuple = new ValueTuple<IntPtr, uint>();
            StaticResource.video.TryDequeue(out ValueTuple);
            //打开码流，送入头数据 Open stream
            if (PlayCtrl.PlayM4_OpenStream(m_lPort, ValueTuple.Item1, ValueTuple.Item2, 1024 * 1024) == false)
            {
                uint nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
              //  LogHelper.WriteLog("3错误码" + nLastErr.ToString());
                // MessageBox.Show("PlayM4_OpenStream" + nLastErr.ToString());
                return;
            }
            //设置显示缓冲区个数 Set the display buffer number
            if (!PlayCtrl.PlayM4_SetDisplayBuf(m_lPort, 15))
            {
                uint iLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
               // LogHelper.WriteLog("4错误码" + iLastErr.ToString());
                // MessageBox.Show("PlayM4_OpenStream" + iLastErr.ToString());
            }
            //设置显示模式 Set the display mode
            if (!PlayCtrl.PlayM4_SetOverlayMode(m_lPort, 0, 0/* COLORREF(0)*/)) //play off screen 
            {
                uint iLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
              //  LogHelper.WriteLog("5错误码" + iLastErr.ToString());
                //MessageBox.Show("PlayM4_OpenStream" + iLastErr.ToString());
            }
            //开始解码 Start to play
            try {
                CameraWindow.Picture.Invoke(new Action(() =>
                {
                    if (PlayCtrl.PlayM4_Play(m_lPort, CameraWindow.Picture.Handle) == false)
                    {
                        uint nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
                        return;
                    }
                    Thread Threads = new Thread(Input)
                    {
                        IsBackground = true
                    };
                    Threads.Start();
                }));
            } catch {
                if (StaticResource.CameraVideoIsEnd)
                {
                    StaticResource.ShowMessage("句柄错误，请关闭后重试");
                }
            }
        }
        public  void Input()
        {
            try
            {
                while (StaticResource.CameraVideoIsEnd&& StaticResource.video.Count<1) {
                    Thread.Sleep(1);
                }
                CameraWindow.Picture.Image = null;
                while (StaticResource.CameraVideoIsEnd)
                {
                    if (StaticResource.video.Count >0)
                    {
                        ValueTuple<IntPtr, uint> ValueTuple = new ValueTuple<IntPtr, uint>();
                        StaticResource.video.TryDequeue(out  ValueTuple);
                        //送入码流数据进行解码 Input the stream data to decode
                        if (!PlayCtrl.PlayM4_InputData(m_lPort, ValueTuple.Item1, ValueTuple.Item2))
                        {
                            uint nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
                          //  LogHelper.WriteLog("错误码" + nLastErr.ToString());
                            //若缓冲区满，则重复送入数据
                            if (nLastErr == 11)
                            {
                                PlayCtrl.PlayM4_InputData(m_lPort, ValueTuple.Item1, ValueTuple.Item2);
                                Thread.Sleep(2);
                            }
                        }
                    }
                    else {
                        Thread.Sleep(2);
                    }
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLog("错误"+e);
            }
        }



        private void OnClientClosed(object sender, EventArgs e)
        {
            if (StaticResource.CameraVideoIsEnd)
            {
                 StaticResource.ShowMessage("获取失败，原因（1）：设备离线（2）：登录错误");
            }
        }

        private void OnClientError(object sender, ErrorEventArgs e)
        {
            if (StaticResource.CameraVideoIsEnd)
            {
                StaticResource.ShowMessage("获取失败，原因（1）：设备离线（2）：登录错误");
            }
        }

        public void Send(byte[] data)
        {
            if (client.IsConnected)
            {
                client.Send(data);
            }
            else
            {
                if (StaticResource.CameraVideoIsEnd)
                {
                    StaticResource.ShowMessage("服务器连接失败..");
                }
            }
        }

        public void Close()
        {
            client.Close();
        }
    }
}
