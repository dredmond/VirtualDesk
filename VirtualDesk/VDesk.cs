using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualDesk.Properties;

namespace VirtualDesk
{
    public class VDesk : IDisposable
    {
        private IntPtr _desktopPointer;
        private bool _disposed;
        private VirtualDeskForm _vDeskForm;

        public string Name { get; private set; }

        public static VDesk GetMainDesktop()
        {
            var id = WindowsApi.GetCurrentThreadId();
            var desktopPointer = WindowsApi.GetThreadDesktop(id);

            return new VDesk("MainDesktop", desktopPointer);
        }

        protected VDesk(string name, IntPtr desktopPointer)
        {
            Name = name;
            _desktopPointer = desktopPointer;

            StartThread();
        }

        public VDesk(string name)
        {
            Name = name;

            _desktopPointer = WindowsApi.CreateDesktop(name, IntPtr.Zero, IntPtr.Zero, 0,
                (uint)WindowsApi.AccessRights.GENERIC_ALL, IntPtr.Zero);

            StartThread();
        }

        private void StartThread()
        {
            if (_desktopPointer == IntPtr.Zero)
            {
                throw new Exception("Failed to create desktop.");
            }

            var _thread = new Thread(ThreadProc);
            _thread.Start();
        }

        private void ThreadProc()
        {
            var ret = WindowsApi.SetThreadDesktop(_desktopPointer);
            Console.WriteLine(Resources.MovedThreadToDesktop, Name, ret);

            _vDeskForm = new VirtualDeskForm();
            Application.Run(_vDeskForm);
        }

        public bool Switch()
        {
            return WindowsApi.SwitchDesktop(_desktopPointer);
        }

        public void CloseAllWindows()
        {
            var ret = WindowsApi.EnumDesktopWindows(_desktopPointer, CloseWindowProc, IntPtr.Zero);
            Console.WriteLine(Resources.ClosedAllWindows, Name, ret);
        }

        private bool CloseWindowProc(IntPtr hwnd, IntPtr lParam)
        {
            var ret = WindowsApi.SendMessage(hwnd, WindowsApi.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine(Resources.ClosedWindow, Name, hwnd, ret);
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) 
                return;
            
            if (disposing)
            {
                
            }

            if (_desktopPointer != IntPtr.Zero)
            {
                CloseAllWindows();
                WindowsApi.CloseDesktop(_desktopPointer);
                _desktopPointer = IntPtr.Zero;
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~VDesk()
        {
            Dispose(false);
        }
    }
}
