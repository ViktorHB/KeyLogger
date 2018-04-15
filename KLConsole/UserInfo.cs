using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace KLConsole
{
    class UserInfo
    {
        private String _ip;
        private String _mac;
        private String _os;
        private String _userName;
        private String _pcName;
        private String _proccId;
        private String _processorCount;

        #region prop
        public String IP
        {
            get
            {
                if (_ip == null)
                    _ip = GetIP();
                return _ip;
            }
        }
        public String MAC
        {
            get
            {
                if (_mac == null)
                    _mac = GetMACPhysicalAddress();
                return _mac;
            }
        }
        public String OS
        {
            get
            {
                if (_os == null)
                    _os = GetOSVersion();
                return _os;
            }
        }
        public String UserName
        {
            get
            {
                if (_userName == null)
                    _userName = GetUserName();
                return _userName;
            }
        }
        public String PCName
        {
            get
            {
                if (_pcName == null)
                    _pcName = GetPCName();
                return _pcName;
            }
        }
        public String ProcInfo
        {
            get
            {
                if (_proccId == null)
                    _proccId = GetProcessorInfo();
                return _proccId;
            }
        }
        public String ProcCount
        {
            get
            {
                if (_processorCount == null)
                    _processorCount = GetProcessoCoreCount();
                return _processorCount;
            }
        }
        public int ID
        {
            get
            {
                return new ServiceReferenceUser.UsersClient().GetUserId(MAC);
            }
        }

        #endregion prop

        /// <summary>
        /// Get information about user
        /// </summary>
        /// <returns></returns>
        public UserInfo GetInfo()
        {
            return this;
        }

        /// <summary>
        /// Get IP Adress
        /// </summary>
        /// <returns></returns>
        private String GetIP()
        {
            return Dns.GetHostAddresses(Environment.MachineName)[0].ToString();
        }
        private String GetMACPhysicalAddress()
        {
            return (
                    from nic in NetworkInterface.GetAllNetworkInterfaces()
                    where nic.OperationalStatus == OperationalStatus.Up
                    select nic.GetPhysicalAddress().ToString()
                    ).FirstOrDefault();
        }

        /// <summary>
        /// Get User Name
        /// </summary>
        /// <returns></returns>
        private String GetUserName()
        {
            return Environment.UserName;
            //the same
            //var userDomain = Environment.UserDomainName;
            //var pcName = Environment.GetEnvironmentVariable("COMPUTERNAME");
        }

        /// <summary>
        /// Get PC Name
        /// </summary>
        /// <returns></returns>
        private String GetPCName()
        {
            return Environment.MachineName;
        }

        /// <summary>
        /// Get OS Version
        /// </summary>
        /// <returns></returns>
        private String GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }

        /// <summary>
        /// Get PROCESSOR IDENTIFIER
        /// </summary>
        /// <returns></returns>
        private String GetProcessorInfo()
        {
            return Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
        }


        /// <summary>
        /// Get Processoe core count
        /// </summary>
        /// <returns></returns>
        private String GetProcessoCoreCount()
        {
            return Environment.ProcessorCount.ToString();
        }
    }
}
