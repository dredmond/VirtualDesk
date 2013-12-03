using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualDesk_Simulated.Desktop
{
    public static class General
    {
        /*
private void button1_Click(object sender, EventArgs e)
{
    Size sz = Screen.PrimaryScreen.Bounds.Size;
    IntPtr hDesk = GetDesktopWindow();
    IntPtr hSrce = GetWindowDC(hDesk);
    IntPtr hDest = CreateCompatibleDC(hSrce);
    IntPtr hBmp = CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
    IntPtr hOldBmp = SelectObject(hDest, hBmp);
    bool b = BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
    Bitmap bmp = Bitmap.FromHbitmap(hBmp);
    SelectObject(hDest, hOldBmp);
    DeleteObject(hBmp);
    DeleteDC(hDest);
    ReleaseDC(hDesk, hSrce);
    bmp.Save(@"c:\temp\test.png");
    bmp.Dispose();
}*/

        public static Image GetSnapshot()
        {
            var sz = Screen.PrimaryScreen.Bounds.Size;
            var hDesk = WinApi.GetDesktopWindow();
            var hSrc = WinApi.GetWindowDC(hDesk);
            var hDest = WinApi.CreateCompatibleDC(hSrc);
            var hBmp = WinApi.CreateCompatibleBitmap(hSrc, sz.Width, sz.Height);
            var hOldBmp = WinApi.SelectObject(hDest, hBmp);

            WinApi.BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrc, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
            
            var bmp = Image.FromHbitmap(hBmp);
            WinApi.SelectObject(hDest, hOldBmp);
            WinApi.DeleteObject(hBmp);
            WinApi.DeleteDC(hDest);
            WinApi.ReleaseDC(hDesk, hSrc);

            return bmp;
        }
    }
}
