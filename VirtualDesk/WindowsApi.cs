using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDesk
{
    public static class WindowsApi
    {
        [Flags]
        public enum AccessRights : uint
        {
            DESKTOP_CREATEMENU = 0x0004,
            DESKTOP_CREATEWINDOW = 0x0002,
            DESKTOP_ENUMERATE = 0x0040,
            DESKTOP_HOOKCONTROL = 0x0008,
            DESKTOP_JOURNALPLAYBACK = 0x0020,
            DESKTOP_JOURNALRECORD = 0x0010,
            DESKTOP_READOBJECTS = 0x0001,
            DESKTOP_SWITCHDESKTOP = 0x0100,
            DESKTOP_WRITEOBJECTS = 0x0080,
            GENERIC_ALL = DESKTOP_CREATEMENU | DESKTOP_CREATEWINDOW | DESKTOP_ENUMERATE
                | DESKTOP_HOOKCONTROL | DESKTOP_JOURNALPLAYBACK | DESKTOP_JOURNALRECORD
                | DESKTOP_READOBJECTS | DESKTOP_SWITCHDESKTOP | DESKTOP_WRITEOBJECTS
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags,
            uint dwDesiredAccess, IntPtr lpsa);

        [DllImport("user32.dll")]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(int dwThreadId);

        [DllImport("user32.dll")]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        public static extern bool SwitchDesktop(IntPtr hDesktop);

        [DllImport("Kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("Kernel32.dll")]
        public static extern uint GetLastError();
    }
}
