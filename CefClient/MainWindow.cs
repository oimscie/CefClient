using Asm.AutoService;
using CefClient.Camera;
using CefClient.CarVideo;
using CefClient.SuperSocket;
using CefSharp;
using CefSharp.WinForms;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Text;
using System.Threading;
using System.Linq;

namespace CefClient
{
    public partial class MainWindow : Form
    {
        public static TabControl TabControls;
        /// <summary>
        /// 主页地址
        /// </summary>
        private readonly string HOMEURL;
        private Rectangle recChild;
        private string text;
        private SolidBrush brush;
        /// <summary>
        /// 关闭区域框画笔
        /// </summary>
        private Pen pen1;
        /// <summary>
        /// 关闭按钮画笔
        /// </summary>
        private Pen pen2;
        /// <summary>
        /// 关闭区域尺寸
        /// </summary>
        private const int CLOSE_SIZE = 17;
        /// <summary>
        /// 标题文字格式
        /// </summary>
        private readonly StringFormat sf;
        /// <summary>
        /// 视频窗口是否打开
        /// </summary>
        public static bool VideoWindow = false;
        public delegate void Messboxdelegates(string text);
        private static ChromiumWebBrowser temp;
        private OrderSocketClient OrderCoketClient;
        public MainWindow()
        {
            InitializeComponent();
            TabControls = this.tabControl1;
            HOMEURL = "http://" + ConfigurationManager.AppSettings["ServerIp"];       
            this.Load += Form1_Load;
            pen1 = new Pen(Color.Black, 1.8f);
            pen2 = new Pen(Color.Transparent);
            brush = new SolidBrush(Color.White);
            sf = new StringFormat(StringFormatFlags.DirectionRightToLeft)
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeCefSettings();
            ChromiumWebBrowser browser = new ChromiumWebBrowser(string.Empty)
            {
                Dock = DockStyle.Fill
            };
            this.MainPage.Controls.Add(browser);
            browser.Load(HOMEURL);
            browser.TitleChanged += TitleChanged;
            browser.LifeSpanHandler = new LifeSpanHandler();
            browser.KeyboardHandler = new KeyboardHandler();
            browser.DownloadHandler = new DownloadHandler(browser);
            browser.MenuHandler = new ContextMenuHandler();
            browser.JsDialogHandler = new JsDialogHandler();
        }
        /// <summary>
        /// 初始化cef设置
        /// </summary>
        private void InitializeCefSettings()
        {
            CefSettings settings;
            try {
                 settings = new CefSettings();
            } 
            catch{            
                MessageBox.Show("致命错误");
                Application.Exit();
                return;
            }
            // 设置缓存目录，下次账号自动登录
            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            settings.CachePath = System.IO.Path.Combine(appDir, "cef_cache");
            //settings.RemoteDebuggingPort = 8088;
            settings.Locale = "zh-CN"; // 右键菜单默认中文
            settings.AcceptLanguageList = "zh-CN"; // 网站默认中文
            settings.IgnoreCertificateErrors = true;
            settings.LogFile = System.IO.Path.Combine(appDir, "cef_log/" + Guid.NewGuid().ToString() + ".txt");
            //settings.LogSeverity = LogSeverity.Verbose;
            if (!Cef.Initialize(settings))
                throw new Exception("Unable to Initialize Cef");
            SetCookie();
            ThreadPool.QueueUserWorkItem(orderSocketInit);
            ThreadPool.QueueUserWorkItem(heart);
        }

        private void orderSocketInit(object objs) {
            OrderCoketClient = new OrderSocketClient("$login!" + StaticResource.uuid + "!client$");
        }

        private void heart(object objs) {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler((obj, eventArg) => {
                OrderCoketClient.Send(Encoding.UTF8.GetBytes("$heart$"));
            });
            timer.Interval = 5000;
            timer.Enabled = true;
            timer.AutoReset = true;
        }
        //写入客户端唯一识别码
        private void SetCookie()
        {
            var cookieManager = Cef.GetGlobalCookieManager();
            StaticResource.uuid = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
            cookieManager.SetCookie(HOMEURL, new Cookie()
            {
                Domain = ConfigurationManager.AppSettings["ServerIp"],
                Name = "uuid",
                Value = StaticResource.uuid,
                Expires = DateTime.MaxValue
            });
        }
        /// <summary>
        /// 新增Tab页
        /// </summary>
        public static void CreateTabItem(string url)
        {
            if (TabControls.TabCount < 2)
            {
                TabPage tpage = new TabPage
                {
                    Name = "NewPage"
                };
                TabControls.Controls.Add(tpage);
                TabControls.SelectTab(TabControls.TabCount - 1);
                temp = new ChromiumWebBrowser(url)
                {
                    Parent = tpage,
                    Dock = DockStyle.Fill
                };
                temp.TitleChanged += TitleChanged;
                temp.LifeSpanHandler = new LifeSpanHandler();
                temp.KeyboardHandler = new KeyboardHandler();
                temp.JsDialogHandler = new JsDialogHandler();
            }
        }

        /// <summary>
        /// 标题变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TitleChanged(object sender, TitleChangedEventArgs e)
        {
            TabControls.Invoke(new Action(() =>
            {
                TabControls.SelectedTab.Text = e.Title;
            }));
        }
        /// <summary>
        /// 重绘标题文字与关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            text = TabControls.TabPages[e.Index].Text;
            brush.Color = Color.White;
            recChild = TabControls.GetTabRect(e.Index);
            e.Graphics.FillRectangle(brush, recChild);
            brush.Color = Color.Black;
            e.Graphics.DrawString(text, SystemInformation.MenuFont, brush, e.Bounds, sf);
            //再画一个矩形框
            recChild.Offset(recChild.Width - (CLOSE_SIZE + 3), 1);
            recChild.Width = CLOSE_SIZE;
            recChild.Height = CLOSE_SIZE;
            e.Graphics.DrawRectangle(pen2, recChild);
            if (TabControls.TabPages[e.Index].Name == "MainPage") { return; }
            //画Tab选项卡右上方关闭按钮   
            //"\"线
            e.Graphics.DrawLine(pen1, recChild.X + 13, recChild.Y + 13, recChild.X + recChild.Width - 13, recChild.Y + recChild.Height - 13);
            //"/"线
            e.Graphics.DrawLine(pen1, recChild.X + 13, recChild.Y + recChild.Height - 13, recChild.X + recChild.Width - 13, recChild.Y + 13);
        }
        /// <summary>
        /// 点击关闭选项事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (TabControls.SelectedTab.Name != "MainPage")
            {
                if (e.Button == MouseButtons.Left)
                {
                    //计算关闭区域      
                    Rectangle myTabRect = TabControls.GetTabRect(TabControls.SelectedIndex); ;
                    myTabRect.Offset(myTabRect.Width - 0x12, 2);
                    myTabRect.Width = CLOSE_SIZE;
                    myTabRect.Height = CLOSE_SIZE;
                    //如果鼠标在区域内就关闭选项卡    
                    if (e.X > myTabRect.X && e.X < myTabRect.Right && e.Y > myTabRect.Y && e.Y < myTabRect.Bottom == true)
                    {
                        TabControls.SelectedTab.Dispose();
                    }
                }
            }
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (VideoWindow)
            {
                ///关闭车载视频实况窗口
                try
                {
                    LiveWindow.LiveWindows.Invoke(new Action(() => { try { LiveWindow.LiveWindows.Close(); } catch { } }));
                }
                catch
                {
                }
                ///关闭车载录像窗口
                try
                {
                    PlayBack.PlayBacks.Invoke(new Action(() => { try { PlayBack.PlayBacks.Close(); } catch { } }));
                }
                catch
                {
                }
                ///关闭监控窗口
                try
                {
                    Camera.CameraWindow.CameraWindows.Invoke(new Action(() =>
                    {
                        try
                        {
                            if (CameraControl.IsControl)
                            {
                                ///关闭监控云台
                                try
                                {
                                    CameraControl.CamControls.Invoke(new Action(() =>
                                    {
                                        CameraControl.CamControls.Close();
                                    }));
                                }
                                catch { }
                            }
                            Camera.CameraWindow.CameraWindows.Close();
                        }
                        catch
                        {
                        }
                    }));
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// 信息弹出
        /// </summary>
        /// <param name="info"></param>
        public static void MessboxShow(string info)
        {
            MessageBox.Show(info);
        }
    }
}
