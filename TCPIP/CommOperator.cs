using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;

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
        /// The static method that returns this computer's external ip asynchronously.
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetIPAsync()
        {
            try
            {
                var webClient = new WebClient();
                var result = await webClient.DownloadStringTaskAsync("http://ipinfo.io/ip");
                externalIP = result;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

