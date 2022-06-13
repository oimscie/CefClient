using CefClient;
using System;

namespace CefSharp
{
    class LifeSpanHandler : ILifeSpanHandler
    {
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
             try
             {
                 if (browserControl.GetBrowserHost().HasDevTools && browser.IsPopup)
                 {
                     return false;
                 }
                 return true;
             }
             catch
             {
                 return true;
             }
          
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = null;
            MainWindow.TabControls.Invoke(new Action<string>(MainWindow.CreateTabItem), targetUrl);
            return true;
        }
    }
}
