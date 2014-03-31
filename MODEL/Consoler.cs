using log4net;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MODEL
{
    public class Consoler
    {
        [DllImport("Kernel32.dll")]
        static extern Boolean AllocConsole();
        [DllImport("Kernel32.dll")]
        public static extern bool SetConsoleTitle(string strMessage);
        [DllImport("kernel32.dll", EntryPoint = "FreeConsole")]
        private static extern bool FreeConsole();
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        extern static IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        private static readonly ILog logger = LogManager.GetLogger("App.Informer");

        private Consoler()
        {
           
        }

        /// <summary>
        /// Pop up the console window
        /// </summary>
        public static void LoadConsole()
        {
            AllocConsole();
            IntPtr windowHandle = FindWindow(null, Process.GetCurrentProcess().MainModule.FileName);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            const uint SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);
            
        }

        /// <summary>
        /// Close the console window without exiting the application
        /// </summary>
        public static void CloseConsole()
        {
            FreeConsole();
        }

        /// <summary>
        /// Send message to the console window
        /// </summary>
        /// <param name="msg">Message to send</param>
        public static void Output(string msg)
        {
            if (logger.IsInfoEnabled)
            {
                Console.Clear();
                logger.Info(msg);
            }
        }
    }
}
