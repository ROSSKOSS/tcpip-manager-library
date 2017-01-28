using Mono.Nat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPIP
{
    class TCPListener
    {
        private INatDevice _device;
        /// <summary>
        /// Current port of the server.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// The Advanced TcpListener with port forwarding, etc.
        /// </summary>
        public TCPListener()
        {
            Port = CommOperator.GetOpenPort();
        }
        /// <summary>
        /// This method turns on UPnP port forwarding.</summary> 
        /// <remarks>If it doesn't work you should enable it in 192.168.0.1 of your current device.</remarks>
        /// 
        public void TurnOnUPnP()
        {
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.DeviceLost += DeviceLost;
            NatUtility.StartDiscovery();
        }
        private void DeviceFound(object sender, DeviceEventArgs args)
        {
            _device = args.Device;
            _device.CreatePortMap(new Mapping(Protocol.Tcp, Port, Port));
        }

        private void DeviceLost(object sender, DeviceEventArgs args)
        {
            _device = args.Device;
        }
    }
}
