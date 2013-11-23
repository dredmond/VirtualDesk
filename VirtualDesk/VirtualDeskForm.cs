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

            textBox1.AppendText("Desktop Pointer: " + VirtualDesktopManager.DesktopPointer + "\r\n");
            textBox1.AppendText("Main Desktop Pointer: " + VirtualDesktopManager.MainDesktop + "\r\n");
        }

        void VirtualDeskForm_Disposed(object sender, EventArgs e)
        {

        }

        void VirtualDeskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                VirtualDesktopManager.Running = false;
            }
        }

        void VirtualDeskForm_Move(object sender, EventArgs e)
        {
            Location = new Point(_initialX, _initialY);
        }

        private void desktop2_Click(object sender, EventArgs e)
        {
            if (VirtualDesktopManager.Switch(VirtualDesktopManager.DesktopPointer))
            {
                textBox1.AppendText("Switched to new desktop.\r\n");
            }
        }

        private void desktop1_Click(object sender, EventArgs e)
        {
            if (VirtualDesktopManager.Switch(VirtualDesktopManager.MainDesktop))
            {
                textBox1.AppendText("Switched to main.\r\n");
            }
        }

        public void ForceClose()
        {
            FormClosing -= VirtualDeskForm_FormClosing;
            Close();
        }
    }
}
