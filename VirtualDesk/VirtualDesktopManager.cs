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
        public static VDesk AltDesktop;
        public static VDesk MainDesktop;

        public static void VirtualDeskThread()
        {
            MainDesktop = VDesk.GetMainDesktop();
            AltDesktop = new VDesk("NewDesktop");
        }

        public static void Shutdown()
        {
            if (MainDesktop != null)
            {
                MainDesktop.CloseAllWindows();
                AltDesktop = null;
            }

            if (AltDesktop != null)
            {
                AltDesktop.CloseAllWindows();
                AltDesktop = null;
            }
        }
    }
}
