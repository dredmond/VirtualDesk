using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualDesk_Simulated.Desktop;

namespace VirtualDesk_Simulated
{
    public partial class VirtualDeskForm : Form
    {
        public VirtualDeskForm()
        {
            InitializeComponent();

            Shown += VirtualDeskForm_Shown;
            Move += VirtualDeskForm_Move;
            timer1.Tick += timer1_Tick;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            UpdateSelectedDesktopScreen();
        }

        void VirtualDeskForm_Move(object sender, EventArgs e)
        {
            //MoveToBottomRight();
            timer1.Enabled = true;
        }

        void VirtualDeskForm_Shown(object sender, EventArgs e)
        {
            MoveToBottomRight();
        }

        void MoveToBottomRight()
        {
            var sh = Screen.PrimaryScreen.WorkingArea.Height;
            var sw = Screen.PrimaryScreen.WorkingArea.Width;

            Location = new Point(sw - Width, sh - Height);
        }

        void UpdateSelectedDesktopScreen()
        {
            var snapshot = General.GetSnapshot();
            pictureBox1.Image = snapshot;
            pictureBox2.Image = snapshot;
            pictureBox3.Image = snapshot;
            pictureBox4.Image = snapshot;
        }
    }
}
