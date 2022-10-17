using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefClient
{
    public class Utils
    {
        private delegate void lableShowDelegate(Label lable, string strshow);

        /// <summary>
        /// 修改界面提示文字
        /// </summary>
        /// <param name="lable"></param>
        /// <param name="strshow"></param>
        public static void ModifyLable(Label lable, string strshow)
        {
            try
            {
                if (lable.InvokeRequired)
                {
                    lable.Invoke(new lableShowDelegate(ModifyLable), new object[] { lable, strshow });
                }
                else
                {
                    lable.Text = strshow;
                }
            }
            catch
            {
            }
        }
    }
}