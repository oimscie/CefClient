using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CefSharp
{
    class KeyboardHandler : IKeyboardHandler
    {
        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            return true;
        }

        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            switch (windowsKeyCode)
            {
                case (int)Keys.F5:
                    if (type == KeyType.KeyUp)
                    {
                        browser.Reload(false);
                    }
                    break;
                case (int)Keys.F6:
                    if (type == KeyType.KeyUp)
                    {
                        browser.Reload(true);
                    }
                    break;
                case (int)Keys.F12:
                    if (type == KeyType.KeyUp)
                    {
                        browser.ShowDevTools();
                    }
                    break;
                case (int)Keys.F11:
                    if (type == KeyType.KeyUp)
                    {
                        // browser.ShowDevTools();
                    }
                    break;
            }
            return false;
        }
    }
}
