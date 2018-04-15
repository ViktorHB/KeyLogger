using System.Runtime.InteropServices;

namespace KLConsole
{
    class Program
    {
        private static MainLogic KLlogic;

        static void Main(string[] args)
        {
            //First onCloseEvenr 
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);

            //Second onCloseEvenr
            //signalHandler += HandleConsoleSignal;
            //ConsoleHelper.SetSignalHandler(signalHandler, true);

            KLlogic = new MainLogic();
            KLlogic.LetsGo();
        }

        #region onClose
        /// <summary>
        /// OnClose Callback function
        /// </summary>
        /// <param name = "eventType" ></ param >
        /// < returns ></ returns >
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                KLlogic.ChangeStatus();
                KLlogic.StopSession();
            }
            return false;
        }

        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected

        // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);


        #endregion onClose


        #region second
        //Second onCloseEvenr
        //private static SignalHandler signalHandler;

        //private static void HandleConsoleSignal(ConsoleSignal consoleSignal)
        //{
        //    //TODO
           
        //}

        //internal delegate void SignalHandler(ConsoleSignal consoleSignal);

        //internal enum ConsoleSignal
        //{
        //    CtrlC = 0,
        //    CtrlBreak = 1,
        //    Close = 2,
        //    LogOff = 5,
        //    Shutdown = 6
        //}

        //internal static class ConsoleHelper
        //{
        //    [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
        //    public static extern bool SetSignalHandler(SignalHandler handler, bool add);
        //}

        #endregion second

    }
}
