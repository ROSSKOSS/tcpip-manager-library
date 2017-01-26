using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;
using System.Net.Sockets;

namespace TCPIP
{
    /// <summary>
    /// The Server-part class. Manages the incoming requests and distributes them to the listeners.
    /// </summary>
    public class CommOperator
    {
        /// <summary>
        /// After the GetIPAsync method will be executed, this property will contain obtained ip.
        /// </summary>
        static string externalIP { get; set; }

        /// <summary>
        /// After the GetLocalIP method will be executed, this property will contain obtained ip.
        /// </summary>
        static string localIP { get; set; }

        /// <summary>
        /// The static method that returns this computer's external ip asynchronously.
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetIPAsync()
        {
            var webClient = new WebClient();
            var result = await webClient.DownloadStringTaskAsync("http://ipinfo.io/ip");
            externalIP = result;
            return result;
        }

        /// <summary>
        /// The static method that returns this computer's local ip.
        /// </summary>
        public static string GetLocalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        /// <summary>
        /// NotImplemented
        /// </summary>
        public static void ConnectClientToServer(string IP, string Port)
        {

        }
    }
}

