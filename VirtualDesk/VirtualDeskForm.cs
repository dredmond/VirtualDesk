﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VirtualDesk
{
    public partial class VirtualDeskForm : Form
    {
        private readonly int _initialX;
        private readonly int _initialY;

        public VirtualDeskForm()
        {
            InitializeComponent();
            
            _initialY = Screen.PrimaryScreen.WorkingArea.Height - Height;
            _initialX = Screen.PrimaryScreen.WorkingArea.Width - Width;

            Location = new Point(_initialX, _initialY);
            Move += VirtualDeskForm_Move;
            FormClosing += VirtualDeskForm_FormClosing;

            textBox1.AppendText("Alt Desktop Pointer: " + VirtualDesktopManager.AltDesktop.Name + "\r\n");
            textBox1.AppendText("Main Desktop Pointer: " + VirtualDesktopManager.MainDesktop.Name + "\r\n");
        }

        void VirtualDeskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            VirtualDesktopManager.Shutdown();
        }

        void VirtualDeskForm_Move(object sender, EventArgs e)
        {
            Location = new Point(_initialX, _initialY);
        }

        private void desktop2_Click(object sender, EventArgs e)
        {
            if (VirtualDesktopManager.AltDesktop.Switch())
            {
                textBox1.AppendText("Switched to new desktop.\r\n");
            }
        }

        private void desktop1_Click(object sender, EventArgs e)
        {
            if (VirtualDesktopManager.MainDesktop.Switch())
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
