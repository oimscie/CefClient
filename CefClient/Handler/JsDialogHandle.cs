using System.Windows.Forms;
using CefSharp;

namespace Asm.AutoService
{
    class JsDialogHandler : IJsDialogHandler
    {
        public bool OnBeforeUnloadDialog(IWebBrowser chromiumWebBrowser, IBrowser browser, string messageText, bool isReload, IJsDialogCallback callback)
        {
            throw new System.NotImplementedException();
        }

        public void OnDialogClosed(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public bool OnJSBeforeUnload(IWebBrowser browserControl, IBrowser browser, string message, bool isReload, IJsDialogCallback callback)
        {
            return true;
        }

        public bool OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
        {
            switch (dialogType)
            {
                case CefJsDialogType.Alert:
                    MessageBox.Show(messageText, "提示");
                    suppressMessage = true;
                    return false;
                case CefJsDialogType.Confirm:
                    var dr = MessageBox.Show(messageText, "提示", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        callback.Continue(true, string.Empty);
                        suppressMessage = false;
                        return true;
                    }
                    else
                    {
                        callback.Continue(false, string.Empty);
                        suppressMessage = false;
                        return true;
                    }
                case CefJsDialogType.Prompt:
                    MessageBox.Show("系统不支持prompt形式的提示框", "提示");
                    break;
                default:
                    break;
            }
            return false;
        }

        public void OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
        {

        }
    }
}