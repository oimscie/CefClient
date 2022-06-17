using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp
{
    class ObjectRegisterToWeb
    {    
        public void showMessage(string str)
        {
            ProgramStart(ConfigurationManager.AppSettings["ReservesManage"]);
        }
        /// <summary>
        /// 启动外部程序，无限等待其退出
        /// </summary>
        public void ProgramStart(string appName)
        {
            if (File.Exists(appName))
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(appName, "");
                process.StartInfo = startInfo;
                process.Start();
            }
            else {
                MessageBox.Show("未找到外部程序","信息提示");
            }
        }
    }
}
