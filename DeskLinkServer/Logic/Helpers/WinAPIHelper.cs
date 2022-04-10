using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DeskLinkServer.Logic.Helpers
{
    public static class WinAPIHelper
    {
        #region [Public Methods]

        public static void MoveCursor(short x, short y)
        {
            GetCursorPos(out currentCursorPosition);
            SetCursorPos(currentCursorPosition.X - x, currentCursorPosition.Y - y);
        }

        public static void SendInput(string keyCode)
        {
            if (string.IsNullOrEmpty(keyCode))
                return;
            SendKeys.Send(keyCode);
        }

        public static void MouseEvent(MouseClickEventType eventType)
        {
            switch (eventType)
            {
                case MouseClickEventType.LeftClick:
                    MouseEvent(MouseEventType.LeftDown);
                    MouseEvent(MouseEventType.LeftUp);
                    break;
                case MouseClickEventType.RightClick:
                    MouseEvent(MouseEventType.RightDown);
                    MouseEvent(MouseEventType.RightUp);
                    break;
                case MouseClickEventType.MiddleClick:
                    MouseEvent(MouseEventType.MiddleDown);
                    MouseEvent(MouseEventType.MiddleUp);
                    break;
            }
        }

        public static void MouseEvent(MouseEventType eventType)
        {
            MouseEvent((uint)eventType, 0, 0, 0, 0);
        }

        #endregion

        #region [Mouse Event Enums]

        public enum MouseClickEventType
        {
            LeftClick,
            MiddleClick,
            RightClick
        }

        public enum MouseEventType
        {
            LeftUp = 0x00000004,
            LeftDown = 0x00000002,
            MiddleUp = 0x00000040,
            MiddleDown = 0x00000020,
            RightUp = 0x00000010,
            RightDown = 0x00000008
        }

        #endregion

        #region [Cursor Positioning]

        private struct IntPoint
        {
            public int X;
            public int Y;
        }

        private static IntPoint currentCursorPosition;

        #endregion

        #region [Imports]

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("User32.dll")]
        private static extern bool GetCursorPos(out IntPoint point);

        [DllImport("User32.dll", SetLastError = true)]
        private static extern void MouseEvent(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        #endregion
    }
}
