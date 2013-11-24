using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualDesk
{
    public class VDesk : IDisposable
    {
        private IntPtr _desktopPointer;
        private Thread _thread;
        private bool _disposed;
        private bool _running;

        public VDesk()
        {
            
        }

        public void Start()
        {
            if (_running)
                return;

            _running = true;
            _thread = new Thread(ThreadProc);
            _thread.Start();
        }

        private void ThreadProc()
        {
            var ret = false;

            while (_running)
            {
                Thread.Sleep(5000);

                ret = VirtualDesktopManager.SetThread(VirtualDesktopManager.DesktopPointer);
                Console.WriteLine("Switched: " + ret + "\r\n");
            }

            ret = VirtualDesktopManager.SetThread(VirtualDesktopManager.MainDesktop);
            Console.WriteLine("Switched Back: " + ret + "\r\n");
        }

        public void Stop()
        {
            if (!_running)
                return;

            _running = false;
            _thread.Join();
            _thread = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) 
                return;
            
            if (disposing)
            {
                _running = false;
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
