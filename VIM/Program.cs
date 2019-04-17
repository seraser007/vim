using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace VIM
{
    static class Extensions
{
    /// <summary>
    /// Get substring of specified number of characters on the right.
    /// </summary>
    public static string Right(this string value, int length)
    {
        if (length<=0)
            return "";

        if (value.Length<length)
            return value;
        else
        {
            return value.Substring(value.Length - length);
        }
    }
}
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        static string _strPath=System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        static string xxxPath = _strPath.Replace("\\", "").Replace(":","").Right(254);
        static Mutex mutex = new Mutex(true, xxxPath);
        
        [STAThread]

        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                mutex.ReleaseMutex();
            }
            else
            {
                // send our Win32 message to make the currently running instance
                // jump on top of all the other windows
                MessageBox.Show("VIQ Manager is already running","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);

                NativeMethods.PostMessage(
                    (IntPtr)NativeMethods.HWND_BROADCAST,
                    NativeMethods.WM_SHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero);
            }
        }
    }
}
