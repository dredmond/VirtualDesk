using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace VirtualDesk
{
    public static class VirtualDesktopManager
    {
        //public static VDesk AltDesktop;
        public static VDesk MainDesktop;
        public static List<VDesk> AlternateDesktops;

        public static void VirtualDeskThread()
        {
            MainDesktop = VDesk.GetMainDesktop();
            AlternateDesktops = new List<VDesk>
            {
                MainDesktop
            };

            // Loop through existing desktops and add them to our list.
            if (VDesk.GetExistingDesktops(GetExistingDesktopsProc))
            {
                Console.WriteLine("Enumeration of Desktops Succeeded.");
            }
            else
            {
                Console.WriteLine("Enumeration of Desktops Failed.");
            }

            AlternateDesktops.Add(new VDesk("Test"));

            
        }

        public static bool GetExistingDesktopsProc(string lpszDesktop, IntPtr lParam)
        {
            var desk = VDesk.OpenExistingDesktop(lpszDesktop);

            if (desk == null)
                return true;

            AlternateDesktops.Add(desk);
            return true;
        }

        public static void Shutdown()
        {
            if (MainDesktop != null)
            {
                MainDesktop.CloseAllWindows();
                MainDesktop = null;
            }

            foreach (var desktop in AlternateDesktops)
            {
                desktop.CloseAllWindows();
            }

            AlternateDesktops.Clear();
        }
    }
}
