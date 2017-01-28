using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net.NetworkInformation;

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
        /// This method returns first free port on a current PC.
        /// </summary>
        /// <param name="startPort">Left border of the search. Default: 1024.</param>
        /// <param name="endPort">Right border of the search. Default: 65535.</param>
        /// 
        public static int GetOpenPort(int startPort = 1024, int endPort = 65535)
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();
            List<int> usedPorts = tcpEndPoints.Select(p => p.Port).ToList<int>();
            int unusedPort = 0;

            for (int port = startPort; port < endPort; port++)
            {
                if (!usedPorts.Contains(port))
                {
                    unusedPort = port;
                    break;
                }
               
            }
            return unusedPort;
        }
    }
}

