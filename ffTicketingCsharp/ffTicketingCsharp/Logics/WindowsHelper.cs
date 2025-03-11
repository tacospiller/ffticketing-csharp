using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ffTicketingCsharp
{
    internal static class WindowsHelper
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }


        [DllImport("user32.dll")]
        private static extern void mouse_event(ulong dwFlags, ulong dx, ulong dy, ulong dwData, ulong dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

        public static void Click(Point p)
        {
            var mx = Screen.PrimaryScreen.Bounds.Width;
            var my = Screen.PrimaryScreen.Bounds.Height;
            var dx = (double)p.X / (double)mx * 65535;
            var dy = (double)p.Y / (double)my * 65535;

            mouse_event((ulong)(MouseEventFlags.Move | MouseEventFlags.Absolute), (ulong)dx, (ulong)dy, 0, 0);
            mouse_event((ulong)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), (ulong)dx, (ulong)dy, 0, 0);
            mouse_event((ulong)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), (ulong)dx, (ulong)dy, 0, 0);
        }

    }
}
