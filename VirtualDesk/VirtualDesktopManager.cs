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
        private static VDesk _vDesk;

        public static void VirtualDeskThread()
        {
            var threadId = WindowsApi.GetCurrentThreadId();
            DesktopPointer = Create("NewDesktop");
            MainDesktop = WindowsApi.GetThreadDesktop(threadId);
            OpenInterface();

            _vDesk = new VDesk();
            _vDesk.Start();

            while (Running)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }

            _vDesk.Stop();
            _vDesk.Dispose();

            WindowsApi.SwitchDesktop(MainDesktop);
            CloseInterface();

            Close(DesktopPointer);
        }

        public static IntPtr Create(string name)
        {
            return WindowsApi.CreateDesktop("NewDesktop", IntPtr.Zero, IntPtr.Zero, 0, (uint)WindowsApi.AccessRights.GENERIC_ALL, IntPtr.Zero);
        }

        public static bool Close(IntPtr hDesktop)
        {
            return WindowsApi.CloseDesktop(hDesktop);
        }

        public static bool SetThread(IntPtr desktop)
        {
            if (desktop == IntPtr.Zero)
                return false;

            //CloseInterface();
            var ret = WindowsApi.SetThreadDesktop(desktop);
            //Console.WriteLine("Switched: " + ret);

            //OpenInterface();

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

            _form.ForceClose();
            _form = null;
        }
    }
}
