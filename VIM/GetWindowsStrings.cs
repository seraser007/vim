using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace VIM
{
    class GetWindowsStrings
    {
        //Original code was given from
        //http://www.martinstoeckli.ch/csharp/csharp.html#windows_text_resources

        /// <summary>
        /// Searches for a text resource in a Windows library.
        /// </summary>
        /// <example>
        ///   btnCancel.Text = GetWindowsStrings.Load("user32.dll", 801, "Cancel");
        ///   btnYes.Text = GetWindowsStrings.Load("user32.dll", 805, "Yes");
        /// </example>
        /// <param name="libraryName">Name of the windows library like "user32.dll"
        /// or "shell32.dll"</param>
        /// <param name="ident">Id of the string resource.</param>
        /// <param name="defaultText">Return this text, if the resource string could
        /// not be found.</param>
        /// <returns>Requested string if the resource was found,
        /// otherwise the <paramref name="defaultText"/></returns>
        public static string Load(string libraryName, uint ident, string defaultText)
        {
            IntPtr libraryHandle = GetModuleHandle(libraryName);
            if (libraryHandle != IntPtr.Zero)
            {
                StringBuilder sb = new StringBuilder(1024);
                int size = LoadString(libraryHandle, ident, sb, 1024);
                if (size > 0)
                    return sb.ToString();
            }
            return defaultText;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);
    }
}
