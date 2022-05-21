using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using DeskLinkServer.Logic.Helpers;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public class TCPDevice
    {
        private TcpClient client;

        private event Action<DataReader> onDataReceived;
        private event Action onDisconnected;

        public TCPDevice(TcpClient client, Action<DataReader> onDataReceived, Action onDisconnected)
        {
            this.client = client;
            this.onDataReceived = onDataReceived;
            this.onDisconnected = onDisconnected;
        }

        public void Communicate()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                DataReader dataReader = new DataReader(stream);
                while (client.GetState() == TcpState.Established)
                    onDataReceived?.Invoke(dataReader);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                stream?.Close();
                onDisconnected?.Invoke();
            }
        }
    }
}
