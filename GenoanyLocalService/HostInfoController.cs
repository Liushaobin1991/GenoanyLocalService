using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace GenoanyLocalService
{
    public class HostInfoController : ApiController
    {
        // GET api/values
        public HostInfo Get()
        {

            HostInfo hostInfo = new HostInfo();
            hostInfo.IpAddress = GetLocalIp();
            hostInfo.MacAddress = GetMacAddress();
            hostInfo.MachineName = GetMachineName();
            hostInfo.UserName = GetUserName();
            return hostInfo;

        }
        public string GetLocalIp()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }

        public string GetMachineName()
        {
            return System.Environment.MachineName;
        }

        public string GetUserName()
        {
            return System.Environment.UserName;
        }

        public string GetMacAddress()
        {
            return GetMACByIP(GetLocalIp());
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, byte[] mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        public static string GetMACByIP(string ip)
        {
            try
            {
                byte[] aa = new byte[6];

                Int32 ldest = inet_addr(ip); //目的地的ip
                Int32 len = 6;
                int res = SendARP(ldest, 0, aa, ref len);

                return BitConverter.ToString(aa, 0, 6); ;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
