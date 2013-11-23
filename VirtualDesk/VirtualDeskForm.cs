using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VirtualDesk
{
    public partial class VirtualDeskForm : Form
    {
        private readonly int _initialX;
        private readonly int _initialY;
        private IntPtr _desktopPointer = IntPtr.Zero;
        private IntPtr _mainDesktop = IntPtr.Zero;

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
            DESKTOP_READOBJECTS  = 0x0001,
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

        /*
         * HDESK WINAPI GetThreadDesktop(
              _In_  DWORD dwThreadId
            );
         * 
         * BOOL WINAPI SetThreadDesktop(
  _In_  HDESK hDesktop
);
         * 
         * BOOL WINAPI SwitchDesktop(
  _In_  HDESK hDesktop
);
         */

        public VirtualDeskForm()
        {
            InitializeComponent();
            
            _initialY = Screen.PrimaryScreen.WorkingArea.Height - Height;
            _initialX = Screen.PrimaryScreen.WorkingArea.Width - Width;

            Location = new Point(_initialX, _initialY);
            Move += VirtualDeskForm_Move;
            FormClosing += VirtualDeskForm_FormClosing;
            Disposed += VirtualDeskForm_Disposed;

            _desktopPointer = CreateDesktop("NewDesktop", IntPtr.Zero, IntPtr.Zero, 0, (uint)AccessRights.GENERIC_ALL, IntPtr.Zero);
            _mainDesktop = GetThreadDesktop(GetCurrentThreadId());

            textBox1.AppendText("Desktop Pointer: " + _desktopPointer + "\r\n");
            textBox1.AppendText("Main Desktop Pointer: " + _mainDesktop + "\r\n");
        }

        void VirtualDeskForm_Disposed(object sender, EventArgs e)
        {
            Switch(_mainDesktop);

            if (_desktopPointer == IntPtr.Zero)
                return;

            CloseDesktop(_desktopPointer);
            _desktopPointer = IntPtr.Zero;
        }

        void VirtualDeskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Switch(_mainDesktop);

            if (_desktopPointer == IntPtr.Zero) 
                return;
            
            if (!CloseDesktop(_desktopPointer))
            {
                e.Cancel = true;
            }
        }

        void VirtualDeskForm_Move(object sender, EventArgs e)
        {
            Location = new Point(_initialX, _initialY);
        }

        private void desktop2_Click(object sender, EventArgs e)
        {
            Switch(_desktopPointer);
        }

        private void desktop1_Click(object sender, EventArgs e)
        {
            Switch(_mainDesktop);
        }

        private void Switch(IntPtr desktop)
        {
            if (desktop == IntPtr.Zero)
            {
                textBox1.AppendText("Desktop pointer cannot be null.\r\n");
                return;
            }

            if (SwitchDesktop(desktop))
            {
                textBox1.AppendText("Switch Success!\r\n");

                if (SetThreadDesktop(desktop))
                {
                    textBox1.AppendText("SetThread Success!\r\n");
                }
                else
                {
                    textBox1.AppendText("SetThreadFailed Failed\r\n");
                }
            }
            else
            {
                textBox1.AppendText("Switch Failed\r\n");
            }
        }
    }
}
