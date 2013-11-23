using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace VirtualDesk
{
    public class VirtualDesktopManager
    {
        public static IntPtr DesktopPointer = IntPtr.Zero;
        public static IntPtr MainDesktop = IntPtr.Zero;
        private static VirtualDeskForm _form;
        public static bool Running = true;

        /*
         * DESKTOP_CREATEMENU (0x0004L)	Required to create a menu on the desktop.
DESKTOP_CREATEWINDOW (0x0002L)	Required to create a window on the desktop.
DESKTOP_ENUMERATE (0x0040L)	Required for the desktop to be enumerated.
DESKTOP_HOOKCONTROL (0x0008L)	Required to establish any of the window hooks.
DESKTOP_JOURNALPLAYBACK (0x0020L)	Required to perform journal playback on a desktop.
DESKTOP_JOURNALRECORD (0x0010L)	Required to perform journal recording on a desktop.
DESKTOP_READOBJECTS (0x0001L)	Required to read objects on the desktop.
DESKTOP_SWITCHDESKTOP (0x0100L)	Required to activate the desktop using the SwitchDesktop function.
DESKTOP_WRITEOBJECTS (0x0080L)	Required to write objects on the desktop.*/

        [Flags]
        enum AccessRights : uint
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
        private static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags,
            uint dwDesiredAccess, IntPtr lpsa);

        [DllImport("user32.dll")]
        private static extern bool CloseDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        private static extern IntPtr GetThreadDesktop(int dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        private static extern bool SwitchDesktop(IntPtr hDesktop);

        [DllImport("Kernel32.dll")]
        private static extern int GetCurrentThreadId();

        public static void VirtualDeskThread()
        {
            var threadId = GetCurrentThreadId();
            DesktopPointer = Create("NewDesktop");
            MainDesktop = GetThreadDesktop(threadId);
            OpenInterface();

            while (Running)
            {
                Thread.Sleep(1);
            }

            SwitchDesktop(MainDesktop);
            CloseInterface();

            Close(DesktopPointer);
        }

        public static IntPtr Create(string name)
        {
            return CreateDesktop("NewDesktop", IntPtr.Zero, IntPtr.Zero, 0, (uint)AccessRights.GENERIC_ALL, IntPtr.Zero);
        }

        public static bool Close(IntPtr hDesktop)
        {
            return CloseDesktop(hDesktop);
        }

        public static bool SetThread(IntPtr desktop)
        {
            if (desktop == IntPtr.Zero)
                return false;

            CloseInterface();
            var ret = SetThreadDesktop(desktop);
            OpenInterface();

            return ret;
        }

        public static bool Switch(IntPtr desktop)
        {
            if (desktop == IntPtr.Zero)
                return false;

            var ret = true; // SwitchDesktop(desktop);
            return ret && SetThread(desktop);
        }

        public static void OpenInterface()
        {
            if (_form != null)
                return;

            _form = new VirtualDeskForm();
            _form.Show();
        }

        public static void CloseInterface()
        {
            if (_form == null)
                return;

            _form.Close();
        }
    }
}
