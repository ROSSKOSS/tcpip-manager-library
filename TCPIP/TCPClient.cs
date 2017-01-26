using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TCPIP
{
    /// <summary>
    /// The Advanced TcpClient with built-in methods to connect to listeners and transfer data.
    /// </summary>
    class TCPClient
    {
        private TcpClient _client;
        private StreamWriter channelCaster;

        /// <summary>
        /// The IP of the targeted listener.
        /// </summary>
        public string TargetIP { get; set; }

        /// <summary>
        /// The Port of targeted listener.
        /// </summary>
        public int TargetPort { get; set; }

        /// <summary>
        /// The stream of data between client and listener.
        /// </summary>
        public Stream ConnectionChannel { get; set; }

        /// <summary>
        /// The Advanced TcpClient with built-in methods to connect to listeners and transfer data.
        /// </summary>
        /// <param name="targetIP">The IP of the targeted listener.</param>
        /// <param name="targetPort"> The Port of targeted listener.</param>
        public TCPClient(string targetIP, int targetPort)
        {
            TargetIP = targetIP;
            TargetPort = targetPort;
        }
        /// <summary>
        /// This method requests server to approve the connection between self and the client.
        /// </summary>
        /// <param name="clientsName">The name of the client. Eventually is going to be more advanced.</param>
        public void ConnectToSever(string clientsName = "Anonymous client")
        {
            _client = new TcpClient(TargetIP, TargetPort);
            ConnectionChannel = _client.GetStream();
            channelCaster = new StreamWriter(ConnectionChannel);
            channelCaster.WriteLine(clientsName);
            channelCaster.Flush();
        }

        /// <summary>
        /// Sends the data given as a parameter.
        /// </summary>
        /// <param name="data">Arbitary data.</param>
        public void SendData(DataContainer data)
        {
            channelCaster.WriteLine(DataToByteArray(data));
        }

        private byte[] DataToByteArray(DataContainer data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, data);
                return ms.ToArray();
            }

        }
        private object ByteArrayToData(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj as DataContainer;
            }
        }
    }
}
